using MVVMEssentials.ViewModels;
using StoreDemo.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDemo.ViewModels
{
    public class GroceryListViewModel : ViewModelBase
    {
        private readonly GroceryListStore _groceryListStore;

        private string _filter = string.Empty;
        public string Filter
        {
            get
            {
                return _filter;
            }
            set
            {
                _filter = value;
                OnPropertyChanged(nameof(Filter));

                UpdateItems();
            }
        }

        private readonly ObservableCollection<string> _items;
        public IEnumerable<string> Items => _items;

        public GroceryListViewModel(GroceryListStore groceryListStore)
        {
            _groceryListStore = groceryListStore;
            _items = new ObservableCollection<string>();

            _groceryListStore.ItemAdded += GroceryListStore_ItemAdded;
        }

        public override void Dispose()
        {
            _groceryListStore.ItemAdded -= GroceryListStore_ItemAdded;

            base.Dispose();
        }

        private void UpdateItems()
        {
            _items.Clear();

            foreach (string item in _groceryListStore.Items.Where(i => i.Contains(Filter, StringComparison.InvariantCultureIgnoreCase)))
            {
                _items.Add(item);
            }
        }

        private void GroceryListStore_ItemAdded(string item)
        {
            UpdateItems();
        }
    }
}
