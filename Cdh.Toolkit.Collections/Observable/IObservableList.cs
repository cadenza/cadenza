using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdh.Toolkit.Collections.Observable
{
    public interface IObservableList<T>
    {
        event EventHandler<ObservableListChangedEventArgs<T>> Changed;
    }
}
