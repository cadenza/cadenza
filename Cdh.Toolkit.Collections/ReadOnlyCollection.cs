using System;
using System.Collections;
using System.Collections.Generic;

namespace Cdh.Toolkit.Collections
{
    public class ReadOnlyCollection<T> : ICollection<T>
    {
        protected ICollection<T> Decorated { get; private set; }

        public ReadOnlyCollection(ICollection<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            Decorated = collection;
        }

        #region ICollection<T> Members

        void ICollection<T>.Add(T item)
        {
            throw new NotSupportedException();
        }

        void ICollection<T>.Clear()
        {
            throw new NotSupportedException();
        }

        public virtual bool Contains(T item)
        {
            return Decorated.Contains(item);
        }

        public virtual void CopyTo(T[] array, int arrayIndex)
        {
            Decorated.CopyTo(array, arrayIndex);
        }

        public virtual int Count
        {
            get { return Decorated.Count; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        bool ICollection<T>.Remove(T item)
        {
            throw new NotSupportedException();
        }

        #endregion

        #region IEnumerable<T> Members

        public virtual IEnumerator<T> GetEnumerator()
        {
            return Decorated.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Decorated).GetEnumerator();
        }

        #endregion
    }
}
