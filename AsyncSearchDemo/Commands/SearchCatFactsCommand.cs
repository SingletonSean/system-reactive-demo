using AsyncSearchDemo.Models;
using AsyncSearchDemo.Queries;
using AsyncSearchDemo.ViewModels;
using MVVMEssentials.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace AsyncSearchDemo.Commands
{
    public class SearchCatFactsCommand : CommandBase
    {
        private readonly MainViewModel _viewModel;
        private readonly CatFactsQuery _query;

        private IDisposable _currentSearch;

        public SearchCatFactsCommand(MainViewModel viewModel, CatFactsQuery query)
        {
            _viewModel = viewModel;
            _query = query;
        }

        public override void Execute(object parameter)
        {
            _viewModel.IsLoading = true;

            _currentSearch?.Dispose();
            _currentSearch = Observable
                .FromAsync(() => _query.Execute(_viewModel.Search))
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe((catFacts) =>
                {
                    _viewModel.UpdateCatFacts(catFacts.Select(c => c.Content));
                },
                (error) =>
                {
                    MessageBox.Show("Failed to load cat facts.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                },
                () =>
                {
                    _viewModel.IsLoading = false;
                });
        }
    }
}
