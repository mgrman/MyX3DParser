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
        public static UnityEngine.Vector3 Parse(string value)
        {
            value.ParseFloats(out var x, out var y, out var z);
            return new UnityEngine.Vector3(x, y,z);
        }
        public static UnityEngine.Vector3 Parse(IEnumerable<string> value)
        {
            value.ParseFloats(out var x, out var y, out var z);
            return new UnityEngine.Vector3(x, y,z);
        }

        public static string ToX3DString(UnityEngine.Vector3 value)
        {
            return ToStringUtils.ToX3DString(value.x, value.y, value.z);
        }

        public static bool GreaterOrEqual(UnityEngine.Vector3 a, UnityEngine.Vector3 b) => a.x >= b.x && a.y >= b.y && a.z >= b.z;
        public static bool Greater(UnityEngine.Vector3 a, UnityEngine.Vector3 b) => a.x > b.x && a.y > b.y && a.z > b.z;
        public static bool LesserOrEqual(UnityEngine.Vector3 a, UnityEngine.Vector3 b) => a.x <= b.x && a.y <= b.y && a.z <= b.z;
    }
}