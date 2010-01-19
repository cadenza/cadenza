using System;
using System.Collections.Generic;
using System.Threading;

using Cdh.Toolkit.Extensions.ReaderWriterLockSlim;

namespace Cdh.Toolkit.Collections
{
    public class SynchronizedList<T> : SynchronizedCollection<T>, IList<T>
    {
        protected new IList<T> Decorated { get; private set; }

        public SynchronizedList(IList<T> list, EnumerateBehavior behavior, ReaderWriterLockSlim @lock)
            : base(list, behavior, @lock)
        {
            Decorated = list;
        }

        public SynchronizedList(IList<T> list, EnumerateBehavior behavior)
            : base(list, behavior)
        {
            Decorated = list;
        }

        #region IList<T> Members

        public virtual int IndexOf(T item)
        {
            using (Lock.Read())
                return Decorated.IndexOf(item);
        }

        public virtual void Insert(int index, T item)
        {
            using (Lock.Write())
                Decorated.Insert(index, item);
        }

        public virtual void RemoveAt(int index)
        {
            using (Lock.Write())
                Decorated.RemoveAt(index);
        }

        public virtual T this[int index]
        {
            get
            {
                using (Lock.Read())
                    return Decorated[index];
            }
            set
            {
                using (Lock.Write())
                    Decorated[index] = value;
            }
        }

        #endregion
    }
}
