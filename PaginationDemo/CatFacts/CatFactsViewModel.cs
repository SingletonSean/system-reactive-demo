using MVVMEssentials.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PaginationDemo.CatFacts
{
    public class CatFactsViewModel : ViewModelBase
    {
        private readonly CatFactsQuery _catFactsQuery;

        public ObservableCollection<string> CatFacts { get; }

        public CatFactsViewModel()
        {
            _catFactsQuery = new CatFactsQuery();
            CatFacts = new ObservableCollection<string>();

            _ = LoadCatFacts();
        }

        private async Task LoadCatFacts()
        {
            CatFactListing catFactListing = await _catFactsQuery.Execute();

            IEnumerable<string> catFacts = catFactListing.CatFacts.Select(c => c.Content);

            UpdateCatFacts(catFacts);
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
