using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Cdh.Toolkit.Extensions.Events;
using Cdh.Toolkit.Extensions.ReaderWriterLockSlim;

namespace Cdh.Toolkit.Collections.Observable
{
    public class ObservableHashSet<T> : SynchronizedCollection<T>, IObservableCollection<T>
    {
        protected new HashSet<T> Decorated { get; private set; }

        public ObservableHashSet(HashSet<T> hashSet, EnumerateBehavior behavior, ReaderWriterLockSlim @lock)
            : base(hashSet, behavior, @lock)
        {
            Decorated = hashSet;
        }

        public ObservableHashSet(HashSet<T> hashSet, EnumerateBehavior behavior)
            : base(hashSet, behavior)
        {
            Decorated = hashSet;
        }

        protected void FireAdded(T item)
        {
            Changed.Fire(this, new ObservableCollectionChangedEventArgs<T>(item, ObservableChangeType.Add));
        }

        protected void FireRemoved(T item)
        {
            Changed.Fire(this, new ObservableCollectionChangedEventArgs<T>(item, ObservableChangeType.Remove));
        }

        public override void Add(T item)
        {
            using (Lock.Write())
            {
                if (Decorated.Add(item))
                    FireAdded(item);
            }
        }

        public override void Clear()
        {
            using (Lock.Write())
            {
                foreach (T item in Decorated)
                    FireRemoved(item);

                Decorated.Clear();
            }
        }

        public override bool Remove(T item)
        {
            using (Lock.Write())
            {
                bool success;

                if (success = Decorated.Remove(item))
                    FireRemoved(item);

                return success;
            }
        }

        #region IObservableCollection<T> Members

        public event EventHandler<ObservableCollectionChangedEventArgs<T>> Changed;

        #endregion
    }
}
