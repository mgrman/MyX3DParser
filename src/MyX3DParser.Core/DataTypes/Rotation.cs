
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using MyX3DParser.Generated.Model.AbstractNodes;
using MyX3DParser.Generated.Model.DataTypes;


namespace MyX3DParser.Generated.Model.DataTypes
{
    public partial struct Rotation
    {

        public static Rotation operator -(Rotation value)
        {
            return new Rotation(value.X, value.Y, value.Z, -value.Angle);

            //float ls = value.X * value.X + value.Y * value.Y + value.Z * value.Z + value.W * value.W;
            //float invNorm = 1.0f / ls;

            //var X = -value.X * invNorm;
            //var Y = -value.Y * invNorm;
            //var Z = -value.Z * invNorm;
            //var W = value.W * invNorm;

            //return new Rotation(X, Y, Z, W);
        }
        public Quaternion ToQuaternion()
        {
            var s = (float)Math.Sin(Angle / 2);
           var x = X * s;
           var y = Y * s;
           var z = Z * s;
           var w = (float)Math.Cos(Angle / 2);

            return new Quaternion(x, y, z, w);
        }

        public static Rotation Interpolate(Rotation a, Rotation b, float alpha)
        {

            return Quaternion.Slerp(a.ToQuaternion(), b.ToQuaternion(), alpha).ToAngleAxis();
        }

    }
}