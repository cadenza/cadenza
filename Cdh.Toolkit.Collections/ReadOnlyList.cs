using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdh.Toolkit.Collections
{
    public class ReadOnlyList<T> : ReadOnlyCollection<T>, IList<T>
    {
        protected new IList<T> Decorated { get; private set; }

        public ReadOnlyList(IList<T> list)
            : base(list)
        {
            Decorated = list;
        }

        #region IList<T> Members

        public int IndexOf(T item)
        {
            return Decorated.IndexOf(item);
        }

        void IList<T>.Insert(int index, T item)
        {
            throw new NotSupportedException();
        }

        void IList<T>.RemoveAt(int index)
        {
            throw new NotSupportedException();
        }

        public T this[int index]
        {
            get { return Decorated[index]; }
            set { throw new NotSupportedException(); }
        }

        #endregion
    }
}
