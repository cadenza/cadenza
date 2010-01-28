// 
// SynchronizedDictionary.cs
//  
// Author:
//       Chris Howie <cdhowie@gmail.com>
// 
// Copyright (c) 2010 Chris Howie
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
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

        public SynchronizedDictionary(EnumerateBehavior behavior, ReaderWriterLockSlim @lock)
            : base(new Dictionary<TKey, TValue>(), behavior, @lock)
        {
            Decorated = (IDictionary<TKey, TValue>)base.Decorated;
            Initialize();
        }

        public SynchronizedDictionary(EnumerateBehavior behavior)
            : base(new Dictionary<TKey, TValue>(), behavior)
        {
            Decorated = (IDictionary<TKey, TValue>)base.Decorated;
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

        public virtual void Add(TKey key, TValue value)
        {
            using (Lock.Write())
                Decorated.Add(key, value);
        }

        public virtual bool ContainsKey(TKey key)
        {
            using (Lock.Read())
                return Decorated.ContainsKey(key);
        }

        public virtual ICollection<TKey> Keys { get; private set; }

        public virtual bool Remove(TKey key)
        {
            using (Lock.Write())
                return Decorated.Remove(key);
        }

        public virtual bool TryGetValue(TKey key, out TValue value)
        {
            using (Lock.Read())
                return Decorated.TryGetValue(key, out value);
        }

        public virtual ICollection<TValue> Values { get; private set; }

        public virtual TValue this[TKey key]
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
