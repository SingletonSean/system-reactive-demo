using MVVMEssentials.ViewModels;

namespace StoreDemo.ViewModels
{
    public class GroceryViewModel : ViewModelBase
    {
        public GroceryListViewModel GroceryListViewModel { get; }
        public AddGroceryListItemViewModel AddGroceryListItemViewModel { get; }

        public GroceryViewModel(GroceryListViewModel groceryListViewModel, AddGroceryListItemViewModel addGroceryListItemViewModel)
        {
            GroceryListViewModel = groceryListViewModel;
            AddGroceryListItemViewModel = addGroceryListItemViewModel;
        }
    }
}
