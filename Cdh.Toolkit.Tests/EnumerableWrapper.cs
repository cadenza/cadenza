using System;
using System.Collections;
using System.Collections.Generic;

namespace Cdh.Toolkit.Tests
{
    public class EnumerableWrapper<T> : IEnumerable<T>
    {
        private IEnumerable<T> enumerable;

        public EnumerableWrapper(IEnumerable<T> enumerable)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");

            this.enumerable = enumerable;
        }

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return enumerable.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)enumerable).GetEnumerator();
        }

        #endregion
    }
}
