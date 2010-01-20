using System;
using System.Collections.Generic;

namespace Cdh.Toolkit.Collections.Observable
{
    public interface IObservableCollection<T> : ICollection<T>
    {
        event EventHandler<ObservableCollectionChangedEventArgs<T>> Changed;
    }
}
