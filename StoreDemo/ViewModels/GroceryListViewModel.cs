using MVVMEssentials.ViewModels;
using StoreDemo.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDemo.ViewModels
{
    public class GroceryListViewModel : ViewModelBase
    {
        private readonly GroceryListStore _groceryListStore;
        private readonly IDisposable _groceryListItemsChangedSubscription;

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
            }
        }

        private readonly ObservableCollection<string> _items;
        public IEnumerable<string> Items => _items;

        public GroceryListViewModel(GroceryListStore groceryListStore)
        {
            _groceryListStore = groceryListStore;
            _items = new ObservableCollection<string>();

            _groceryListItemsChangedSubscription = Observable
                .Merge<object>(
                    _groceryListStore.ItemAddedObservable, 
                    Observable
                        .FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                            h => PropertyChanged += h,
                            h => PropertyChanged -= h)
                        .Where(e => e.EventArgs.PropertyName == nameof(Filter)))
                .Select(e => _groceryListStore.Items)
                .Select(items => items.Where(i => i.Contains(Filter, StringComparison.InvariantCultureIgnoreCase)))
                .Subscribe((items) =>
                {
                    _items.Clear();

                    foreach (string item in items)
                    {
                        _items.Add(item);
                    }
                });
        }

        public override void Dispose()
        {
            _groceryListItemsChangedSubscription.Dispose();

            base.Dispose();
        }
    }
}
