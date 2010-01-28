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

using Cdh.Toolkit.Extensions.ReaderWriterLockSlim;

namespace Cdh.Toolkit.Collections
{
    public class SynchronizedList<T> : SynchronizedCollection<T>, IList<T>
    {
        protected new IList<T> Decorated { get; private set; }

        public SynchronizedList(IList<T> list, EnumerateBehavior behavior, ReaderWriterLockSlim @lock)
            : base(list, behavior, @lock)
        {
            Decorated = list;
        }

        public SynchronizedList(IList<T> list, EnumerateBehavior behavior)
            : base(list, behavior)
        {
            Decorated = list;
        }

        public SynchronizedList(EnumerateBehavior behavior, ReaderWriterLockSlim @lock)
            : base(new List<T>(), behavior, @lock)
        {
            Decorated = (IList<T>)base.Decorated;
        }

        public SynchronizedList(EnumerateBehavior behavior)
            : base(new List<T>(), behavior)
        {
            Decorated = (IList<T>)base.Decorated;
        }

        #region IList<T> Members

        public virtual int IndexOf(T item)
        {
            using (Lock.Read())
                return Decorated.IndexOf(item);
        }

        public virtual void Insert(int index, T item)
        {
            using (Lock.Write())
                Decorated.Insert(index, item);
        }

        public virtual void RemoveAt(int index)
        {
            using (Lock.Write())
                Decorated.RemoveAt(index);
        }

        public virtual T this[int index]
        {
            get
            {
                using (Lock.Read())
                    return Decorated[index];
            }
            set
            {
                using (Lock.Write())
                    Decorated[index] = value;
            }
        }

        #endregion
    }
}
