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
