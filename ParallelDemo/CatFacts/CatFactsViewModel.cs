using MVVMEssentials.ViewModels;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Windows;

namespace ParallelDemo.CatFacts
{
    public class CatFactsViewModel : ViewModelBase
    {
        private readonly DailyCatFactQuery _dailyCatFactQuery;
        private readonly CatFactsQuery _catFactsQuery;

        private string _dailyCatFact;
        public string DailyCatFact
        {
            get
            {
                return _dailyCatFact;
            }
            set
            {
                _dailyCatFact = value;
                OnPropertyChanged(nameof(DailyCatFact));
            }
        }

        public ObservableCollection<string> CatFacts { get; }

        public CatFactsViewModel()
        {
            _dailyCatFactQuery = new DailyCatFactQuery();
            _catFactsQuery = new CatFactsQuery();

            CatFacts = new ObservableCollection<string>();

            CatFactsObservable
                .FromCatFactsAsync(_dailyCatFactQuery, _catFactsQuery)
                .Subscribe((result) =>
                {
                    HandleDailyCatFactResult(result);
                    HandleCatFactsListingResult(result);
                }, 
                (ex) =>
                {
                    MessageBox.Show("Failed to load cats facts. Please try again later.", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                });
        }

        private void HandleCatFactsListingResult(CatFactsForkJoinResult result)
        {
            CatFacts.Clear();

            if (result.CatFacts.Error != null)
            {
                MessageBox.Show("Failed to load cat facts. Please try again later.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                foreach (CatFact catFact in result.CatFacts.Data)
                {
                    CatFacts.Add(catFact.Content);
                }
            }
        }

        private void HandleDailyCatFactResult(CatFactsForkJoinResult result)
        {
            if (result.DailyCatFact.Error != null)
            {
                MessageBox.Show("Failed to load daily cat fact. Please try again later.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                DailyCatFact = result.DailyCatFact.Data.Content;
            }
        }
    }
}
