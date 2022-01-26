using System;
using System.Collections.Generic;
using System.Linq;
using MyX3DParser.Utils;
using MyX3DParser.Generated.Model.DataTypes;
using MyX3DParser.Generated.Model.AbstractNodes;
using MyX3DParser.Generated.Model.Nodes;
using MyX3DParser.Generated.Model.Statements;
using MyX3DParser.Generated.Model.Fields;
using MyX3DParser.Generated.Model.Parsing;

namespace MyX3DParser.Generated.Model.DataTypes
{
    public static partial class Vec4f
    {
        public static System.Numerics.Vector4 Parse(string value)
        {
            value.ParseFloats(out var x, out var y, out var z, out var w);
            return new System.Numerics.Vector4(x, y,z,w);
        }

        public static System.Numerics.Vector4 Parse(IEnumerable<string> value)
        {
            value.ParseFloats(out var x, out var y, out var z, out var w);
            return new System.Numerics.Vector4(x, y,z,w);
        }
        public static string ToX3DString(System.Numerics.Vector4 value)
        {
            return ToStringUtils.ToX3DString(value.X, value.Y, value.Z, value.W);
        }
        public static bool GreaterOrEqual(System.Numerics.Vector4 a, System.Numerics.Vector4 b) => a.X >= b.X && a.Y >= b.Y && a.Z >= b.Z && a.W >= b.W;
        public static bool LesserOrEqual(System.Numerics.Vector4 a, System.Numerics.Vector4 b) => a.X <= b.X && a.Y <= b.Y && a.Z <= b.Z && a.W <= b.W;
    }
}