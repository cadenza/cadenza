using System;
using System.Reflection.Emit;

namespace Cdh.Toolkit.Extensions.Reflection.Emit
{
    public static class Extensions
    {
        public static void EmitTypeof(this ILGenerator il, Type type)
        {
            il.Emit(OpCodes.Ldtoken, type);
            il.Emit(OpCodes.Call, typeof(Type).GetMethod("GetTypeFromHandle"));
        }
    }
}
