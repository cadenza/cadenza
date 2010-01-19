using System;
using System.Collections.Generic;

namespace Cdh.Toolkit.Extensions.Enumerable
{
    public static class Extensions
    {
        public static IEnumerable<T> NotNull<T>(this IEnumerable<T?> e)
            where T : struct
        {
            foreach (T? i in e)
                if (i.HasValue)
                    yield return i.Value;
        }
    }
}
