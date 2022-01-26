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
    public static partial class ColorRGBA
    {
        public static UnityEngine.Color Parse(string value)
        {
            value.ParseFloats(out var r, out var g, out var b, out var a);
            return new UnityEngine.Color(r, g, b, a);
        }

        public static UnityEngine.Color Parse(IEnumerable<string> value)
        {
            value.ParseFloats(out var r, out var g, out var b, out var a);
            return new UnityEngine.Color(r, g, b, a);
        }

        public static string ToX3DString(UnityEngine.Color value)
        {
            return ToStringUtils.ToX3DString(value.r, value.g, value.b, value.a);
        }
    }
}