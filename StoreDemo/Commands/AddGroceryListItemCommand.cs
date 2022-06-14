using MVVMEssentials.Commands;
using StoreDemo.Stores;
using StoreDemo.ViewModels;

namespace StoreDemo.Commands
{
    public class AddGroceryListItemCommand : CommandBase
    {
        private readonly AddGroceryListItemViewModel _viewModel;
        private readonly GroceryListStore _groceryListStore;

        public AddGroceryListItemCommand(AddGroceryListItemViewModel viewModel, GroceryListStore groceryListStore)
        {
            _viewModel = viewModel;
            _groceryListStore = groceryListStore;
        }

        public override void Execute(object parameter)
        {
            _groceryListStore.AddItem(_viewModel.Description);
        }
    }
}
