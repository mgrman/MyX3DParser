using System;
using System.Diagnostics.CodeAnalysis;

namespace MyX3DParser.Utils
{
    public static class NullUtils
    {
        [return: NotNull]
        public static T ThrowIfNull<T>(this T? value)
        {
            if (value == null)
            {
                throw new InvalidOperationException();
            }

            return value;
        }
    }
}