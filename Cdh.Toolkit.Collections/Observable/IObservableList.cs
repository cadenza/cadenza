using System;
using System.Collections.Generic;

namespace Cdh.Toolkit.Collections.Observable
{
    public interface IObservableList<T> : IObservableCollection<T>, IList<T>
    {
        new event EventHandler<ObservableListChangedEventArgs<T>> Changed;
    }
}
