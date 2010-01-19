using System;

namespace Cdh.Toolkit.Collections.Observable
{
    public interface IObservableList<T>
    {
        event EventHandler<ObservableListChangedEventArgs<T>> Changed;
    }
}
