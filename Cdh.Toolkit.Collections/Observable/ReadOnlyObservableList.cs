using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Cdh.Toolkit.Extensions.Events;

namespace Cdh.Toolkit.Collections.Observable
{
    public class ReadOnlyObservableList<T> : ReadOnlyList<T>, IObservableList<T>
    {
        protected new IObservableList<T> Decorated { get; private set; }

        public ReadOnlyObservableList(IObservableList<T> list)
            : base(list)
        {
            Decorated = list;

            Decorated.Changed += OnDecoratedChanged;
        }

        void OnDecoratedChanged(object sender, ObservableListChangedEventArgs<T> e)
        {
            Changed.Fire(this, e);
            CollectionChanged.Fire(this, e);
        }

        #region IObservableList<T> Members

        public event EventHandler<ObservableListChangedEventArgs<T>> Changed;

        #endregion

        #region IObservableCollection<T> Members

        private event EventHandler<ObservableCollectionChangedEventArgs<T>> CollectionChanged;

        event EventHandler<ObservableCollectionChangedEventArgs<T>> IObservableCollection<T>.Changed
        {
            add { CollectionChanged += value; }
            remove { CollectionChanged -= value; }
        }

        #endregion
    }
}
