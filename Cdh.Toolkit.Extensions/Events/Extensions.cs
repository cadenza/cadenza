using System;

namespace Cdh.Toolkit.Extensions.Events
{
    public static class Extensions
    {
        public static void Fire(this EventHandler handler, object sender, EventArgs args)
        {
            if (handler != null)
                handler(sender, args);
        }

        public static void Fire(this EventHandler handler, object sender)
        {
            handler.Fire(sender, EventArgs.Empty);
        }

        public static void Fire(this EventHandler handler, object sender, Func<EventArgs> argsFactory)
        {
            if (handler != null)
                handler(sender, argsFactory());
        }

        public static void Fire<T>(this EventHandler<T> handler, object sender, T args)
            where T : EventArgs
        {
            if (handler != null)
                handler(sender, args);
        }

        public static void Fire<T>(this EventHandler<T> handler, object sender, Func<T> argsFactory)
            where T : EventArgs
        {
            if (handler != null)
                handler(sender, argsFactory());
        }
    }
}
