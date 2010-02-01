// 
// SynchronizedCollection.cs
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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Cadenza.Collections {

	public class SynchronizedCollection<T> : ICollection<T> {

		protected Func<Action> acquireReadLock, acquireWriteLock;

		public EnumerableBehavior DefaultEnumerableBehavior { get; private set; }

		protected ICollection<T> Decorated { get; private set; }

		public SynchronizedCollection (ICollection<T> collection,
				Func<Action> acquireReadLock, Func<Action> acquireWriteLock,
				EnumerableBehavior defaultBehavior)
		{
			if (collection == null)
				throw new ArgumentNullException ("collection");
			if (acquireReadLock == null)
				throw new ArgumentNullException ("acquireReadLock");
			if (acquireWriteLock == null)
				throw new ArgumentNullException ("acquireWriteLock");

			Decorated = collection;
			DefaultEnumerableBehavior = defaultBehavior;
			this.acquireReadLock  = acquireReadLock;
			this.acquireWriteLock = acquireWriteLock;
		}

		public SynchronizedCollection (ICollection<T> collection, Func<Action> acquireReadLock, Func<Action> acquireWriteLock)
			: this (collection, acquireReadLock, acquireWriteLock, EnumerableBehavior.Copy)
		{
		}

		public SynchronizedCollection (ICollection<T> collection)
		{
			if (collection == null)
				throw new ArgumentNullException ("collection");
			Decorated = collection;
			DefaultEnumerableBehavior = EnumerableBehavior.Copy;

			object _lock = new object ();
			acquireReadLock = acquireWriteLock = _lock.CreateMonitorLockAccessor ();
		}

		public virtual bool Add (T item)
		{
			Action release = acquireReadLock ();
			try {
				// This is kind of a hack, but it ensures that with e.g.
				// HashSet<T> this method will return true/false properly,
				// saving me from having to write HashSet-specific wrappers.

				int initialCount = Decorated.Count;

				Decorated.Add (item);

				return initialCount != Decorated.Count;
			}
			finally {
				release ();
			}
		}

#region ICollection<T> Members

		void ICollection<T>.Add (T item)
		{
			Add (item);
		}

		public virtual void Clear ()
		{
			Action release = acquireWriteLock ();
			try {
				Decorated.Clear ();
			}
			finally {
				release ();
			}
		}

		public virtual bool Contains (T item)
		{
			Action release = acquireReadLock ();
			try {
				return Decorated.Contains (item);
			}
			finally {
				release ();
			}
		}

		public virtual void CopyTo (T[] array, int arrayIndex)
		{
			Action release = acquireReadLock ();
			try {
				Decorated.CopyTo (array, arrayIndex);
			}
			finally {
				release ();
			}
		}

		public virtual int Count {
			get {
				Action release = acquireReadLock ();
				try {
					return Decorated.Count;
				}
				finally {
					release ();
				}
			}
		}

		public virtual bool IsReadOnly {
			get {
				Action release = acquireReadLock ();
				try {
					return Decorated.IsReadOnly;
				}
				finally {
					release ();
				}
			}
		}

		public virtual bool Remove (T item)
		{
			Action release = acquireWriteLock ();
			try {
				return Decorated.Remove (item);
			}
			finally {
				release ();
			}
		}

#endregion

#region IEnumerable<T> Members

		public IEnumerator<T> GetEnumerator ()
		{
			return GetEnumerator (DefaultEnumerableBehavior);
		}

		public virtual IEnumerator<T> GetEnumerator (EnumerableBehavior behavior)
		{
			switch (behavior) {
				case EnumerableBehavior.Copy: {
					Action release = acquireReadLock ();
					try {
						return Decorated.Where (i => true).ToList ().GetEnumerator ();
					}
					finally {
						release ();
					}
				}
				case EnumerableBehavior.Lock:
					return CreateLockEnumerator ();
			}
			throw new ArgumentException ("Unsupported EnumerableBehavior value: " + behavior + ".", "behavior");
		}

		private IEnumerator<T> CreateLockEnumerator ()
		{
			Action release = acquireReadLock ();
			try {
				foreach (T i in Decorated)
					yield return i;
			}
			finally {
				release ();
			}
		}

#endregion

#region IEnumerable Members

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return GetEnumerator ();
		}

#endregion
	}
}
