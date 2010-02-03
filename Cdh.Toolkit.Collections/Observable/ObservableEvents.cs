// 
// ObservableEvents.cs
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

namespace Cdh.Toolkit.Collections.Observable
{
    public enum ObservableChangeType
    {
        Add,
        Remove
    }

    public class ObservableCollectionChangedEventArgs<T> : EventArgs
    {
        public ObservableChangeType ChangeType { get; private set; }
        public T Item { get; private set; }

        public ObservableCollectionChangedEventArgs(T item, ObservableChangeType changeType)
        {
            Item = item;
            ChangeType = changeType;
        }
    }

    public class ObservableListChangedEventArgs<T> : ObservableCollectionChangedEventArgs<T>
    {
        public int Index { get; private set; }

        public ObservableListChangedEventArgs(T item, ObservableChangeType changeType, int index)
            : base(item, changeType)
        {
            Index = index;
        }
    }
}
