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
    public static partial class Vec4d
    {
        public static System.Numerics.Vector4 Parse(string value) => Vec4d.Parse(value);
        public static System.Numerics.Vector4 Parse(IEnumerable<string> value) => Vec4d.Parse(value);

        public static string ToX3DString(System.Numerics.Vector4 value) => Vec4d.ToX3DString(value);
    }
}