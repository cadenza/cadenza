// 
// SynchronizedList.cs
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

namespace Cadenza.Collections {

	public class SynchronizedList<T> : SynchronizedCollection<T>, IList<T> {

		protected new IList<T> Decorated {
			get {return (IList<T>) base.Decorated;}
		}

		public SynchronizedList (IList<T> list, Func<Action> acquireReadLock, Func<Action> acquireWriteLock, EnumerableBehavior defaultBehavior)
			: base (list, acquireReadLock, acquireWriteLock, defaultBehavior)
		{
		}

		public SynchronizedList (IList<T> list, Func<Action> acquireReadLock, Func<Action> acquireWriteLock)
			: base (list, acquireReadLock, acquireWriteLock)
		{
		}

		public SynchronizedList (IList<T> list)
			: base (list)
		{
		}

		public SynchronizedList ()
			: this (new List<T>())
		{
		}

#region IList<T> Members

		public virtual int IndexOf (T item)
		{
			Action release = acquireReadLock ();
			try {
				return Decorated.IndexOf (item);
			}
			finally {
				release ();
			}
		}

		public virtual void Insert (int index, T item)
		{
			Action release = acquireWriteLock ();
			try {
				Decorated.Insert (index, item);
			}
			finally {
				release ();
			}
		}

		public virtual void RemoveAt (int index)
		{
			Action release = acquireWriteLock ();
			try {
				Decorated.RemoveAt (index);
			}
			finally {
				release ();
			}
		}

		public virtual T this [int index] {
			get {
				Action release = acquireReadLock ();
				try {
					return Decorated [index];
				}
				finally {
					release ();
				}
			}
			set {
				Action release = acquireWriteLock ();
				try {
					Decorated [index] = value;
				}
				finally {
					release ();
				}
			}
		}

#endregion
	}
}
