using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdh.Toolkit.Collections.Observable
{
    public interface IObservableDictionary<TKey, TValue> :
        IDictionary<TKey, TValue>,
        IObservableCollection<KeyValuePair<TKey, TValue>>
    {
    }
}
