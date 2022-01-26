using System;
using System.Collections.Generic;
using System.Linq;
using MyX3DParser.Utils;

namespace MyX3DParser.Model.Builders
{
    internal class NumericalConstraintBuilder
    {
        private readonly IDataTypeBuilder nativeDataType;

        public NumericalConstraintBuilder(decimal? minInclusive, decimal? minExclusive, decimal? maxInclusive, decimal? maxExclusive, IDataTypeBuilder nativeDataType)
        {
            this.nativeDataType = nativeDataType;
            MinInclusive = minInclusive;
            MinExclusive = minExclusive;
            MaxInclusive = maxInclusive;
            MaxExclusive = maxExclusive;
            
            
            // called once to initialize constants
            var aaa=GetConstraints("_");
        }

        private IReadOnlyList<string> GetConstraints(string arg)
        {

            var constraints = new List<string>();
            if (MinInclusive.HasValue)
            {
                constraints.Add(nativeDataType.GreaterOrEqualMethod(arg, nativeDataType.GetSingleValue(MinInclusive.Value.ToString())));
            }

            if (MinExclusive.HasValue)
            {
                constraints.Add(nativeDataType.GreaterMethod(arg, nativeDataType.GetSingleValue(MinExclusive.Value.ToString())));
            }

            if (MaxInclusive.HasValue)
            {
                constraints.Add(nativeDataType.LesserOrEqualMethod(arg, nativeDataType.GetSingleValue(MaxInclusive.Value.ToString())));
            }

            if (MaxExclusive.HasValue)
            {
                constraints.Add(nativeDataType.LesserMethod(arg, nativeDataType.GetSingleValue(MaxExclusive.Value.ToString())));
            }

            return constraints;
        }

        public string ConditionSingle(string arg)=> $"{GetConstraints(arg).StringJoin(" && ")}";
        public string ConditionArray(string arg) => $"value.All(o=>{GetConstraints(arg).Select(o => o.Replace("value", "o")).StringJoin(" && ")})";

        public decimal? MinInclusive { get; }
        public decimal? MinExclusive { get; }
        public decimal? MaxInclusive { get; }
        public decimal? MaxExclusive { get; }

        public string Name
        {
            get
            {
                if (MinExclusive != null && MinInclusive != null)
                {
                    throw new InvalidOperationException();
                }

                if (MaxExclusive != null && MaxInclusive != null)
                {
                    throw new InvalidOperationException();
                }

                if (MinInclusive == 0 && MaxInclusive == byte.MaxValue)
                {
                    return $"byte";
                }

                if (MinInclusive == 0 && MaxInclusive == ushort.MaxValue)
                {
                    return $"ushort";
                }

                if (MinInclusive != null && MaxInclusive != null && (MinInclusive * -1) == MaxInclusive)
                {
                    return $"PlusMinus{ToNumber(MaxInclusive, null)}";
                }

                if (MinExclusive != null && MaxExclusive != null && (MinExclusive * -1) == MaxExclusive)
                {
                    return $"PlusMinus{ToNumber(null, MaxExclusive)}";
                }

                if ((MinInclusive != null || MinExclusive != null) && (MaxInclusive != null || MaxExclusive != null))
                {
                    return $"{ToNumber(MinInclusive, MinExclusive)}To{ToNumber(MaxInclusive, MaxExclusive)}";
                }

                if (MinInclusive != null || MinExclusive != null)
                {
                    return $"From{ToNumber(MinInclusive, MinExclusive)}";
                }

                if (MaxInclusive != null || MaxExclusive != null)
                {
                    return $"To{ToNumber(MaxInclusive, MaxExclusive)}";
                }

                throw new InvalidOperationException();
            }
        }

        private static string? ToNumber(decimal? numInclusive, decimal? numExclusive)
        {
            if (numInclusive != null && numExclusive != null)
            {
                throw new InvalidOperationException();
            }
            else if (numInclusive == null && numExclusive == null)
            {
                return null;
            }
            else if (numInclusive != null)
            {
                return ToNumber(numInclusive);
            }
            else if (numExclusive != null)
            {
                return ToNumber(numExclusive) + "Exc";
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        private static string? ToNumber(decimal? num)
        {
            if (num == null)
            {
                return null;
            }

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

            var intVal = (int) num.Value;
            if (intVal != num.Value)
            {
                throw new InvalidOperationException();
            }

            return Math.Sign(intVal) == -1 ? $"Minus{Math.Abs(intVal)}" : intVal.ToString();
        }
    }
}