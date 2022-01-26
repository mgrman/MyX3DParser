using System;
using System.Collections.Generic;
using System.Text;

namespace MyX3DParser.Utils
{
    internal static class ToStringUtils
    {

        public static string? ToNumberString(this decimal? num)
        {
            if (num == null)
            {
                return null;
            }
            return num.Value.ToNumberString();
        }

        public static string ToNumberString(this decimal num)
        {
                if (num == 1.5708m || num == 1.570796m)
            {
                return "HalfPI";
            }

            if (num == 3.1416m)
            {
                return "PI";
            }

            if (num == 6.2832m)
            {
                return "2PI";
            }

            if (num == -1.5708m || num == -1.570796m)
            {
                return "MinusHalfPI";
            }

            if (num == -3.1416m)
            {
                return "MinusPI";
            }

            if (num == -6.2832m)
            {
                return "Minus2PI";
            }

            if (num == 0.8m)
            {
                return "ZeroPointEight";
            }

            if (num == -9.8m)
            {
                return "MinusNinePointEight";
            }

            if (num == 0.02m)
            {
                return "ZeroPointZeroTwo";
            }

            var intVal = (int)num;
            if (intVal != num)
            {
                throw new InvalidOperationException();
            }

            return Math.Sign(intVal) == -1 ? $"Minus{Math.Abs(intVal)}" : intVal.ToString();
        }


        public static string ToX3DString(float val1, float val2)
        {

            return $"{val1.ToInvariantString()} {val2.ToInvariantString()}";
        }

        public static string ToX3DString(float val1, float val2, float val3)
        {

            return $"{val1.ToInvariantString()} {val2.ToInvariantString()} {val3.ToInvariantString()}";
        }

        public static string ToX3DString(float val1, float val2, float val3, float val4)
        {

            return $"{val1.ToInvariantString()} {val2.ToInvariantString()} {val3.ToInvariantString()} {val4.ToInvariantString()}";
        }
    }
}
