using StoreDemo.Stores;
using StoreDemo.ViewModels;
using System.Windows;

namespace StoreDemo
{
    public partial class App : Application
    {
        private readonly GroceryListStore _groceryListStore;

        public App()
        {
            _groceryListStore = new GroceryListStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow()
            {
                DataContext = new GroceryViewModel(
                    new GroceryListViewModel(_groceryListStore),
                    new AddGroceryListItemViewModel(_groceryListStore))
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
