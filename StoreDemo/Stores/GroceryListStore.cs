using System;
using System.Collections.Generic;

namespace StoreDemo.Stores
{
    public class GroceryListStore
    {
        private readonly List<string> _items;
        public IEnumerable<string> Items => _items;

        public event Action<string> ItemAdded;

        public GroceryListStore()
        {
            _items = new List<string>();
        }

        public void AddItem(string description)
        {
            _items.Add(description);
            ItemAdded?.Invoke(description);
        }
    }
}
