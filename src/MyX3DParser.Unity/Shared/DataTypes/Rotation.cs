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
using UnityEngine;

namespace MyX3DParser.Generated.Model.DataTypes
{
    public static partial class Rotation
    {
        public static UnityEngine.Quaternion Parse(string value)
        {
            value.ParseFloats(out var axisX, out var axisY, out var axisZ, out var angle);

            return Quaternion.AngleAxis(Mathf.Rad2Deg * angle, new Vector3(axisX, axisY, axisZ));
        }
        public static UnityEngine.Quaternion Parse(IEnumerable<string> value)
        {
            value.ParseFloats(out var axisX, out var axisY, out var axisZ, out var angle);

            return Quaternion.AngleAxis(Mathf.Rad2Deg* angle, new Vector3(axisX, axisY, axisZ));
        }

        public static string ToX3DString(UnityEngine.Quaternion value)
        {
            value.ToAngleAxis(out var angle, out var axis);
            return ToStringUtils.ToX3DString(axis.x, axis.y, axis.z, Mathf.Deg2Rad * angle);
        }
    }
}