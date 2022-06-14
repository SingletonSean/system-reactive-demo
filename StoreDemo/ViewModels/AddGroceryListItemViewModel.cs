using MVVMEssentials.ViewModels;
using StoreDemo.Commands;
using StoreDemo.Stores;
using System.Windows.Input;

namespace StoreDemo.ViewModels
{
    public class AddGroceryListItemViewModel : ViewModelBase
    {
        private string _description = string.Empty;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public ICommand SubmitCommand { get; set; }

        public AddGroceryListItemViewModel(GroceryListStore groceryListStore)
        {
            SubmitCommand = new AddGroceryListItemCommand(this, groceryListStore);
        }
    }
}
