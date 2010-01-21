using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Cdh.Toolkit.Extensions.Events;
using Cdh.Toolkit.Extensions.ReaderWriterLockSlim;

namespace Cdh.Toolkit.Collections.Observable
{
    public class ObservableCollection<T> : SynchronizedCollection<T>, IObservableCollection<T>
    {
        public ObservableCollection(ICollection<T> collection, EnumerateBehavior behavior, ReaderWriterLockSlim @lock)
            : base(collection, behavior, @lock) { }

        public ObservableCollection(ICollection<T> collection, EnumerateBehavior behavior)
            : base(collection, behavior) { }

        protected virtual void FireAdded(T item)
        {
            var handler = Changed;
            if (handler != null)
                handler(this, new ObservableCollectionChangedEventArgs<T>(item, ObservableChangeType.Add));
        }

        protected virtual void FireRemoved(T item)
        {
            var handler = Changed;
            if (handler != null)
                handler(this, new ObservableCollectionChangedEventArgs<T>(item, ObservableChangeType.Remove));
        }

        public override bool Add(T item)
        {
            using (Lock.Write())
            {
                int originalCount = Decorated.Count;

                Decorated.Add(item);

                if (originalCount != Decorated.Count)
                {
                    FireAdded(item);
                    return true;
                }

                return false;
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
