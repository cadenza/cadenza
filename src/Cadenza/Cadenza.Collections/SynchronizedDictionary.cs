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
using System.Collections.ObjectModel;
using System.Threading;

namespace Cadenza.Collections {

	public class SynchronizedDictionary<TKey, TValue> :
		SynchronizedCollection<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>
	{
		protected new IDictionary<TKey, TValue> Decorated {
			get {return (IDictionary<TKey, TValue>) base.Decorated;}
		}

		public SynchronizedDictionary (IDictionary<TKey, TValue> dictionary,
				EnumerableBehavior behavior, ReaderWriterLockSlim @lock)
			: base (dictionary, behavior, @lock)
		{
			Initialize ();
		}

		public SynchronizedDictionary (IDictionary<TKey, TValue> dictionary,
				EnumerableBehavior behavior)
			: base (dictionary, behavior)
		{
			Initialize ();
		}

		public SynchronizedDictionary (EnumerableBehavior behavior, ReaderWriterLockSlim @lock)
			: base (new Dictionary<TKey, TValue>(), behavior, @lock)
		{
			Initialize ();
		}

		public SynchronizedDictionary(EnumerableBehavior behavior)
			: base (new Dictionary<TKey, TValue>(), behavior)
		{
			Initialize ();
		}

		private void Initialize ()
		{
			// The base collections are already read-only, but wrapping the
			// SynchronizedCollection in a ReadOnlyCollection prevents a write
			// lock from being acquired prior to throwing the inevitable
			// exception.

			// Keys = CreateSynchronizedReadOnlyCollection(Keys);
			// Values = CreateSynchronizedReadOnlyCollection(Values);
		}

#region IDictionary<TKey,TValue> Members

		public virtual void Add (TKey key, TValue value)
		{
			using (Lock.Write ())
				Decorated.Add (key, value);
		}

		public virtual bool ContainsKey (TKey key)
		{
			using (Lock.Read ())
				return Decorated.ContainsKey (key);
		}

		public virtual ICollection<TKey> Keys { get; private set; }

		public virtual bool Remove (TKey key)
		{
			using (Lock.Write ())
				return Decorated.Remove (key);
		}

		public virtual bool TryGetValue (TKey key, out TValue value)
		{
			using (Lock.Read ())
				return Decorated.TryGetValue (key, out value);
		}

		public virtual ICollection<TValue> Values { get; private set; }

		public virtual TValue this [TKey key] {
			get {
				using (Lock.Read())
					return Decorated[key];
			}
			set {
				using (Lock.Write())
					Decorated[key] = value;
			}
		}

#endregion
	}
}
