using System;
using System.Collections.Generic;
using System.Reactive.Subjects;

namespace StoreDemo.Stores
{
    public class GroceryListStore
    {
        private readonly Subject<string> _itemAddedSubject;

        private readonly List<string> _items;
        public IEnumerable<string> Items => _items;

        public IObservable<string> ItemAddedObservable => _itemAddedSubject;

        public GroceryListStore()
        {
            _items = new List<string>();

            _itemAddedSubject = new Subject<string>();
        }

        public void AddItem(string description)
        {
            _items.Add(description);
            _itemAddedSubject.OnNext(description);
        }
    }
}
