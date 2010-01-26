using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Cdh.Toolkit.Json
{
    public static class Resolver
    {
        public static bool TryResolve(object root, out object result, params string[] path)
        {
            try
            {
                result = Resolve(root, path);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }

        public static object Resolve(object root, params string[] path)
        {
            foreach (string i in path)
            {
                root = Resolve(root, i);
            }

            return root;
        }

        private static object Resolve(object root, string path)
        {
            if (root == null)
                throw new ArgumentException("path: Path does not exist.");

            if (root is IList)
                return Resolve((IList)root, path);

            if (root is IDictionary)
                return Resolve((IDictionary)root, path);

            throw new ArgumentException("root: Cannot resolve from type " + root.GetType().FullName);
        }

        private static object Resolve(IList root, string path)
        {
            int intKey;
            if (int.TryParse(path, out intKey))
            {
                return root[intKey];
            }

            return null;
        }

        private static object Resolve(IDictionary root, string path)
        {
            return root[path];
        }
    }
}
