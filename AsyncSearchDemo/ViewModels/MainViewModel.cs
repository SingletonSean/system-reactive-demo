using AsyncSearchDemo.Commands;
using AsyncSearchDemo.Queries;
using MVVMEssentials.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AsyncSearchDemo.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
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

                SearchCatFactsCommand.Execute(null);
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
