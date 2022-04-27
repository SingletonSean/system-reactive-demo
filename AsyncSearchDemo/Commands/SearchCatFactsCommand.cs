using AsyncSearchDemo.Models;
using AsyncSearchDemo.Queries;
using AsyncSearchDemo.ViewModels;
using MVVMEssentials.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AsyncSearchDemo.Commands
{
    public class SearchCatFactsCommand : AsyncCommandBase
    {
        private readonly MainViewModel _viewModel;
        private readonly CatFactsQuery _query;

        public SearchCatFactsCommand(MainViewModel viewModel, CatFactsQuery query)
        {
            _viewModel = viewModel;
            _query = query;
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoading = true;

            try
            {
                IEnumerable<CatFact> catFacts = await _query.Execute(_viewModel.Search);
                _viewModel.UpdateCatFacts(catFacts.Select(c => c.Content));
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to load cat facts.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            _viewModel.IsLoading = false;
        }
    }
}
