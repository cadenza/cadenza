using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdh.Toolkit.Collections.Observable
{
    public interface IObservableCollection<T>
    {
        event EventHandler<ObservableCollectionChangedEventArgs<T>> Changed;
    }
}
