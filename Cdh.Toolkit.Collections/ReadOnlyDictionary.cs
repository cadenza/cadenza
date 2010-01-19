using System;
using System.Collections.Generic;

namespace Cdh.Toolkit.Collections
{
    public class ReadOnlyDictionary<TKey, TValue> :
        ReadOnlyCollection<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>
    {
        protected new IDictionary<TKey, TValue> Decorated { get; private set; }

        public ReadOnlyDictionary(IDictionary<TKey, TValue> dictionary)
            : base(dictionary)
        {
            Decorated = dictionary;
        }

        #region IDictionary<TKey,TValue> Members

        void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
        {
            throw new NotSupportedException();
        }

        public virtual bool ContainsKey(TKey key)
        {
            return Decorated.ContainsKey(key);
        }

        public virtual ICollection<TKey> Keys
        {
            get { return Decorated.Keys; }
        }

        bool IDictionary<TKey, TValue>.Remove(TKey key)
        {
            throw new NotSupportedException();
        }

        public virtual bool TryGetValue(TKey key, out TValue value)
        {
            return Decorated.TryGetValue(key, out value);
        }

        public virtual ICollection<TValue> Values
        {
            get { return Decorated.Values; }
        }

        public virtual TValue this[TKey key]
        {
            get { return Decorated[key]; }
            set { throw new NotSupportedException(); }
        }

        #endregion
    }
}
