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
using System.Numerics;

namespace MyX3DParser.Generated.Model.DataTypes
{
    public static partial class Rotation
    {
        public static System.Numerics.Quaternion Parse(string value)
        {
            value.ParseFloats(out var axisX, out var axisY, out var axisZ, out var angle);

            return System.Numerics.Quaternion.CreateFromAxisAngle( new System.Numerics.Vector3(axisX, axisY, axisZ),angle);
        }
        public static System.Numerics.Quaternion Parse(IEnumerable<string> value)
        {
            value.ParseFloats(out var axisX, out var axisY, out var axisZ, out var angle);

            return System.Numerics.Quaternion.CreateFromAxisAngle(new System.Numerics.Vector3(axisX, axisY, axisZ), angle);
        }

        public static string ToX3DString(System.Numerics.Quaternion value)
        {
          var (axis,angle)=  value.ToAngleAxis();
            return ToStringUtils.ToX3DString(axis.X, axis.Y, axis.Z, angle);
        }

        public static (Vector3 axis,float angle) ToAngleAxis(this System.Numerics.Quaternion value)
        {
            var angle = 2 * Math.Acos(value.W);
            var x = value.X / Math.Sqrt(1 - value.W * value.W);
            var y = value.Y / Math.Sqrt(1 - value.W * value.W);
            var z = value.Z / Math.Sqrt(1 - value.W * value.W);

            if (Math.Abs(angle) < 0.00001)
            {
                return (new Vector3(0, 0, 1), 0);
            }

            return (new Vector3((float)x, (float)y, (float)z), (float)angle);
        }
    }
}