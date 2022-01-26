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
using System.Text;
using System.Numerics;

namespace MyX3DParser.Generated.Model.DataTypes
{
    public static partial class Matrix3d
    {
        public static Matrix4x4 Parse(string value) => Matrix3f.Parse(value);
        public static Matrix4x4 Parse(IEnumerable<string> value) => Matrix3f.Parse(value);

        public static string ToX3DString(Matrix4x4 value) => Matrix4f.ToX3DString(value);
    }
}