using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Cdh.Toolkit.Extensions.Events;

namespace Cdh.Toolkit.Collections.Observable
{
    public class ReadOnlyObservableDictionary<TKey, TValue> :
        ReadOnlyDictionary<TKey, TValue>, IObservableDictionary<TKey, TValue>
    {
        protected new IObservableDictionary<TKey, TValue> Decorated { get; private set; }

        public ReadOnlyObservableDictionary(IObservableDictionary<TKey, TValue> dictionary)
            : base(dictionary)
        {
            Decorated = dictionary;

            Decorated.Changed += OnDecoratedChanged;
        }

        void OnDecoratedChanged(object sender, ObservableCollectionChangedEventArgs<KeyValuePair<TKey, TValue>> e)
        {
            Changed.Fire(this, e);
        }

        #region IObservableCollection<KeyValuePair<TKey,TValue>> Members

        public event EventHandler<ObservableCollectionChangedEventArgs<KeyValuePair<TKey, TValue>>> Changed;

        #endregion
    }
}
