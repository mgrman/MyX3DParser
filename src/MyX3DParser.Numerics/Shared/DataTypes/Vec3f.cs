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
    public static partial class Vec3f
    {
        public static System.Numerics.Vector3 Parse(string value)
        {
            value.ParseFloats(out var x, out var y, out var z);
            return new System.Numerics.Vector3(x, y,z);
        }
        public static System.Numerics.Vector3 Parse(IEnumerable<string> value)
        {
            value.ParseFloats(out var x, out var y, out var z);
            return new System.Numerics.Vector3(x, y,z);
        }

        public static string ToX3DString(System.Numerics.Vector3 value)
        {
            return ToStringUtils.ToX3DString(value.X, value.Y, value.Z);
        }

        public static bool GreaterOrEqual(System.Numerics.Vector3 a, System.Numerics.Vector3 b) => a.X >= b.X && a.Y >= b.Y && a.Z >= b.Z;
        public static bool Greater(System.Numerics.Vector3 a, System.Numerics.Vector3 b) => a.X > b.X && a.Y > b.Y && a.Z > b.Z;
        public static bool LesserOrEqual(System.Numerics.Vector3 a, System.Numerics.Vector3 b) => a.X <= b.X && a.Y <= b.Y && a.Z <= b.Z;
    }
}