using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Cdh.Toolkit.Extensions.ReaderWriterLockSlim;

namespace Cdh.Toolkit.Collections
{
    public class SynchronizedDictionary<TKey, TValue> :
        SynchronizedCollection<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>
    {
        protected new IDictionary<TKey, TValue> Decorated { get; private set; }

        public SynchronizedDictionary(IDictionary<TKey, TValue> dictionary,
            EnumerateBehavior behavior, ReaderWriterLockSlim @lock)
            : base(dictionary, behavior, @lock)
        {
            Decorated = dictionary;
            Initialize();
        }

        public SynchronizedDictionary(IDictionary<TKey, TValue> dictionary,
            EnumerateBehavior behavior)
            : base(dictionary, behavior)
        {
            Decorated = dictionary;
            Initialize();
        }

        private void Initialize()
        {
            // The base collections are already read-only, but wrapping the
            // SynchronizedCollection in a ReadOnlyCollection prevents a write
            // lock from being acquired prior to throwing the inevitable
            // exception.

            Keys = CreateSynchronizedReadOnlyCollection(Keys);
            Values = CreateSynchronizedReadOnlyCollection(Values);
        }

        private ICollection<T> CreateSynchronizedReadOnlyCollection<T>(ICollection<T> collection)
        {
            return new ReadOnlyCollection<T>(
                new SynchronizedCollection<T>(
                    collection, EnumerateBehavior, Lock));
        }

        #region IDictionary<TKey,TValue> Members

        public void Add(TKey key, TValue value)
        {
            using (Lock.Write())
                Decorated.Add(key, value);
        }

        public bool ContainsKey(TKey key)
        {
            using (Lock.Read())
                return Decorated.ContainsKey(key);
        }

        public ICollection<TKey> Keys { get; private set; }

        public bool Remove(TKey key)
        {
            using (Lock.Write())
                return Decorated.Remove(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            using (Lock.Read())
                return Decorated.TryGetValue(key, out value);
        }

        public ICollection<TValue> Values { get; private set; }

        public TValue this[TKey key]
        {
            get
            {
                using (Lock.Read())
                    return Decorated[key];
            }
            set
            {
                using (Lock.Write())
                    Decorated[key] = value;
            }
        }

        #endregion
    }
}
