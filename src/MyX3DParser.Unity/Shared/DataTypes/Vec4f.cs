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
        public static UnityEngine.Vector4 Parse(string value)
        {
            value.ParseFloats(out var x, out var y, out var z, out var w);
            return new UnityEngine.Vector4(x, y,z,w);
        }

        public static UnityEngine.Vector4 Parse(IEnumerable<string> value)
        {
            value.ParseFloats(out var x, out var y, out var z, out var w);
            return new UnityEngine.Vector4(x, y,z,w);
        }
        public static string ToX3DString(UnityEngine.Vector4 value)
        {
            return ToStringUtils.ToX3DString(value.x, value.y, value.z, value.w);
        }
        public static bool GreaterOrEqual(UnityEngine.Vector4 a, UnityEngine.Vector4 b) => a.x >= b.x && a.y >= b.y && a.z >= b.z && a.w >= b.w;
        public static bool LesserOrEqual(UnityEngine.Vector4 a, UnityEngine.Vector4 b) => a.x <= b.x && a.y <= b.y && a.z <= b.z && a.w <= b.w;
    }
}