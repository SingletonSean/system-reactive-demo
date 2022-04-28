using AsyncSearchDemo.Commands;
using AsyncSearchDemo.Queries;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Windows.Input;

namespace AsyncSearchDemo.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IDisposable _disposeSearchObservable;

        private readonly ObservableCollection<string> _catFacts;
        public IEnumerable<string> CatFacts => _catFacts;

        private string _search = string.Empty;
        public string Search
        {
            get
            {
                return _search;
            }
            set
            {
                _search = value;
                OnPropertyChanged(nameof(Search));
            }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        public ICommand SearchCatFactsCommand { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainViewModel()
        {
            _catFacts = new ObservableCollection<string>();
            SearchCatFactsCommand = new SearchCatFactsCommand(this, new CatFactsQuery());

            _disposeSearchObservable = Observable
                .FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                    h => PropertyChanged += h,
                    h => PropertyChanged -= h)
                .Where(e => e.EventArgs.PropertyName == nameof(Search))
                .Throttle(TimeSpan.FromSeconds(1))
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe((e) =>
                {
                    SearchCatFactsCommand.Execute(null);
                });
        }

        public static MainViewModel LoadViewModel()
        {
            MainViewModel viewModel = new MainViewModel();

            viewModel.SearchCatFactsCommand.Execute(null);

            return viewModel;
        }

        public void UpdateCatFacts(IEnumerable<string> catFacts)
        {
            _catFacts.Clear();

            foreach (string catFact in catFacts)
            {
                _catFacts.Add(catFact);
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
