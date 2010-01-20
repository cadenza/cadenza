using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Cdh.Toolkit.Extensions.Events;

namespace Cdh.Toolkit.Collections.Observable
{
    public class ReadOnlyObservableCollection<T> : ReadOnlyCollection<T>, IObservableCollection<T>
    {
        protected new IObservableCollection<T> Decorated { get; private set; }

        public ReadOnlyObservableCollection(IObservableCollection<T> collection)
            : base(collection)
        {
            Decorated = collection;

            Decorated.Changed += OnDecoratedChanged;
        }

        protected virtual void OnDecoratedChanged(object sender, ObservableCollectionChangedEventArgs<T> e)
        {
            Changed.Fire(this, e);
        }

        #region IObservableCollection<T> Members

        public event EventHandler<ObservableCollectionChangedEventArgs<T>> Changed;

        #endregion
    }
}
