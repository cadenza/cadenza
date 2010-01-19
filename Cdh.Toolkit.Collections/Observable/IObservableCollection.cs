using System;

namespace Cdh.Toolkit.Collections.Observable
{
    public interface IObservableCollection<T>
    {
        event EventHandler<ObservableCollectionChangedEventArgs<T>> Changed;
    }
}
