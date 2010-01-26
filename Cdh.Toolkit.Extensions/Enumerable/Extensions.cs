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

        public static void Walk<T>(this IEnumerable<T> e)
        {
            using (IEnumerator<T> walker = e.GetEnumerator())
                while (walker.MoveNext()) ;
        }

        public static void CopyInto<T>(this IEnumerable<T> e, IList<T> list)
        {
            int i = 0;

            foreach (T item in e)
                list[i++] = item;
        }
    }
}
