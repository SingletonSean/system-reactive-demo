using MVVMEssentials.ViewModels;
using PaginationDemo.Utilities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace PaginationDemo.CatFacts
{
    public class CatFactsViewModel : ViewModelBase
    {
        private readonly CatFactsQuery _catFactsQuery;

        public ObservableCollection<string> CatFacts { get; }

        private int _currentPage = 0;
        public int CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));

                OnPropertyChanged(nameof(HasPreviousPage));
                OnPropertyChanged(nameof(HasNextPage));
            }
        }

        private int _currentItemsPerPage = 10;
        public int CurrentItemsPerPage
        {
            get
            {
                return _currentItemsPerPage;
            }
            set
            {
                _currentItemsPerPage = value;
                OnPropertyChanged(nameof(CurrentItemsPerPage));

                CurrentPage = 0;
            }
        }

        private int _totalItems;
        public int TotalItems
        {
            get
            {
                return _totalItems;
            }
            set
            {
                _totalItems = value;
                OnPropertyChanged(nameof(TotalItems));

                OnPropertyChanged(nameof(HasNextPage));
            }
        }

        public double TotalPages => Math.Ceiling((double)TotalItems / CurrentItemsPerPage);

        public bool HasPreviousPage => CurrentPage != 0;
        public bool HasNextPage => CurrentPage + 1 < TotalPages;

        public ICommand PreviousPageCommand 
        { 
            get => new RelayCommand<object>((o) => CurrentPage--); 
        }

        public ICommand NextPageCommand 
        { 
            get => new RelayCommand<object>((o) => CurrentPage++); 
        }

        public ICommand UpdateItemsPerPageCommand
        {
            get => new RelayCommand<int>((itemsPerPage) => CurrentItemsPerPage = itemsPerPage);
        }

        public CatFactsViewModel()
        {
            _catFactsQuery = new CatFactsQuery();
            CatFacts = new ObservableCollection<string>();

            Observable
                .Merge(
                    Observable.Return(new object()),
                    Observable
                        .FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                            h => PropertyChanged += h,
                            h => PropertyChanged -= h)
                        .Where(e => e.EventArgs.PropertyName == nameof(CurrentPage))
                )
                .Select((e) => Observable.FromAsync(() => GetCatFacts()))
                .Switch()
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe((catFactListing) =>
                {
                    IEnumerable<string> catFacts = catFactListing.CatFacts.Select(c => c.Content);

                    TotalItems = catFactListing.Total;
                    UpdateCatFacts(catFacts);
                },
                (error) =>
                {
                    CatFacts.Clear();

                    MessageBox.Show("Failed to load cat facts.", "Error", 
                        MessageBoxButton.OK, MessageBoxImage.Error);
                });
        }

        private async Task<CatFactListing> GetCatFacts()
        {
            int limit = CurrentItemsPerPage;
            int offset = CurrentPage * limit;

            return await _catFactsQuery.Execute(offset, limit);
        }

        private void UpdateCatFacts(IEnumerable<string> catFacts)
        {
            CatFacts.Clear();

            foreach (string catFact in catFacts)
            {
                CatFacts.Add(catFact);
            }
        }
    }
}
