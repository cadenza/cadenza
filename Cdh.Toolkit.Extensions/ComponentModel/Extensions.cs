using System;
using System.ComponentModel;

namespace Cdh.Toolkit.Extensions.ComponentModel
{
    public static class Extensions
    {
        public static void AutoInvoke(this ISynchronizeInvoke obj, Action method)
        {
            if (obj.InvokeRequired)
                obj.Invoke(method, null);
            else
                method();
        }

        public static object AutoInvoke(this ISynchronizeInvoke obj, Delegate method, params object[] args)
        {
            if (obj.InvokeRequired)
                return obj.Invoke(method, args);
            else
                return method.Method.Invoke(method.Target, args);
        }

        public static AsyncCallback Invoked(this AsyncCallback callback, ISynchronizeInvoke obj)
        {
            return result => obj.AutoInvoke(callback, result);
        }
    }
}
