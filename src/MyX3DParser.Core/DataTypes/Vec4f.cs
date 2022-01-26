
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyX3DParser.Generated.Model.AbstractNodes;
using MyX3DParser.Generated.Model.DataTypes;


namespace MyX3DParser.Generated.Model.DataTypes
{
    public partial struct Vec4f
    {

        public static Vec4f operator -(Vec4f a) => new Vec4f(-a.X, -a.Y, -a.Z, -a.W);

        public float Magnitude=> (float)Math.Sqrt(SqrMagnitude);


        public float SqrMagnitude=>(X * X) + (Y * Y) + (Z * Z) + (W * W);
    }
}