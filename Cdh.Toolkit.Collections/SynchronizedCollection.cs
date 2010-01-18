using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using Cdh.Toolkit.Extensions.ReaderWriterLockSlim;

namespace Cdh.Toolkit.Collections
{
    public class SynchronizedCollection<T> : ICollection<T>
    {
        public ReaderWriterLockSlim Lock { get; private set; }
        public EnumerateBehavior EnumerateBehavior { get; private set; }

        protected ICollection<T> Decorated { get; private set; }

        public SynchronizedCollection(ICollection<T> collection,
            EnumerateBehavior behavior, ReaderWriterLockSlim @lock)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            if (@lock == null)
                throw new ArgumentNullException("lock");

            Decorated = collection;
            EnumerateBehavior = behavior;
            Lock = @lock;
        }

        public SynchronizedCollection(ICollection<T> collection,
            EnumerateBehavior behavior) :
            this(collection, behavior, new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion))
        {
        }

        #region ICollection<T> Members

        public void Add(T item)
        {
            using (Lock.Write())
                Decorated.Add(item);
        }

        public void Clear()
        {
            using (Lock.Write())
                Decorated.Clear();
        }

        public bool Contains(T item)
        {
            using (Lock.Read())
                return Decorated.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            using (Lock.Read())
                Decorated.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get
            {
                using (Lock.Read())
                    return Decorated.Count;
            }
        }

        public bool IsReadOnly
        {
            get { return Decorated.IsReadOnly; }
        }

        public bool Remove(T item)
        {
            using (Lock.Write())
                return Decorated.Remove(item);
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            if (EnumerateBehavior == EnumerateBehavior.Copy)
                using (Lock.Read())
                    return Decorated.Where(i => true).ToList().GetEnumerator();

            return CreateGenericEnumerator();
        }

        private IEnumerator<T> CreateGenericEnumerator()
        {
            using (Lock.Read())
                foreach (T i in Decorated)
                    yield return i;
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            if (EnumerateBehavior == EnumerateBehavior.Copy)
                using (Lock.Read())
                    Decorated.Cast<object>().ToList().GetEnumerator();

            return CreateEnumerator();
        }

        private IEnumerator CreateEnumerator()
        {
            using (Lock.Read())
                foreach (object i in (IEnumerable)Decorated)
                    yield return i;
        }

        #endregion
    }
}
