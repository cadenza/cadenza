using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Cdh.Toolkit.Extensions.Reflection
{
    public static class Extensions
    {
        public static IEnumerable<T> GetCustomAttributes<T>(this ICustomAttributeProvider provider, bool inherit)
            where T : Attribute
        {
            return provider.GetCustomAttributes(typeof(T), inherit).Cast<T>();
        }

        public static T GetCustomAttribute<T>(this ICustomAttributeProvider provider, bool inherit)
            where T : Attribute
        {
            return provider.GetCustomAttributes<T>(inherit).FirstOrDefault();
        }
    }
}
