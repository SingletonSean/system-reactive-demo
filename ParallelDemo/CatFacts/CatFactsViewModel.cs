using MVVMEssentials.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

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
        }
    }
}
