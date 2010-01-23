using System;
using System.Collections.Generic;

namespace Cdh.Toolkit.Extensions.Collections
{
    public static class Extensions
    {
        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
        {
            TValue value;
            dict.TryGetValue(key, out value);
            return value;
        }

        public static TValue GetOrValue<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue fallback)
        {
            TValue value;

            return dict.TryGetValue(key, out value) ? value : fallback;
        }
    }
}
