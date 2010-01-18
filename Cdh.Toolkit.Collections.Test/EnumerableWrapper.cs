using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Cdh.Toolkit.Collections.Test
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
