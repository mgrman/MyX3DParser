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
    public static partial class Vec2f
    {
        public static UnityEngine.Vector2 Parse(string value)
        {
            value.ParseFloats(out var x, out var y);
            return new UnityEngine.Vector2(x,y);
        }
        public static UnityEngine.Vector2 Parse(IEnumerable<string> value)
        {
            value.ParseFloats(out var x, out var y);
            return new UnityEngine.Vector2(x,y);
        }

        public static string ToX3DString(UnityEngine.Vector2 value)
        {
            return ToStringUtils.ToX3DString(value.x, value.y);
        }
        public static bool GreaterOrEqual(UnityEngine.Vector2 a, UnityEngine.Vector2 b) => a.x >= b.x && a.y >= b.y;
        public static bool Greater(UnityEngine.Vector2 a, UnityEngine.Vector2 b) => a.x > b.x && a.y > b.y;
    }
}