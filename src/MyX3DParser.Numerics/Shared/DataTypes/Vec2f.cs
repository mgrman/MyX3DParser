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
        public static System.Numerics.Vector2 Parse(string value)
        {
            value.ParseFloats(out var x, out var y);
            return new System.Numerics.Vector2(x,y);
        }
        public static System.Numerics.Vector2 Parse(IEnumerable<string> value)
        {
            value.ParseFloats(out var x, out var y);
            return new System.Numerics.Vector2(x,y);
        }

        public static string ToX3DString(System.Numerics.Vector2 value)
        {
            return ToStringUtils.ToX3DString(value.X, value.Y);
        }
        public static bool GreaterOrEqual(System.Numerics.Vector2 a, System.Numerics.Vector2 b) => a.X >= b.X && a.Y >= b.Y;
        public static bool Greater(System.Numerics.Vector2 a, System.Numerics.Vector2 b) => a.X > b.X && a.Y > b.Y;
    }
}