
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyX3DParser.Generated.Model.AbstractNodes;
using MyX3DParser.Generated.Model.DataTypes;


namespace MyX3DParser.Generated.Model.DataTypes
{
    public partial struct Vec3f
    {

        public static Vec3f operator -(Vec3f a) => new Vec3f(-a.X, -a.Y, -a.Z);
        public static Vec3f operator +(Vec3f a, Vec3f b) => new Vec3f(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        public static Vec3f operator *(Vec3f a, Vec3f b) => new Vec3f(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Vec3f operator -(Vec3f a, Vec3f b) => new Vec3f(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

        public float Magnitude=> (float)Math.Sqrt(SqrMagnitude);


        public float SqrMagnitude=>(X * X) + (Y * Y) + (Z * Z);


        public static Vec3f Interpolate(Vec3f a, Vec3f b, float alpha)
        {
            return new Vec3f(a.X * (1 - alpha) + b.X * alpha, a.Y * (1 - alpha) + b.Y * alpha, a.Z * (1 - alpha) + b.Z * alpha);
        }

        public static Vec3f Cross(Vec3f lhs, Vec3f rhs) => new Vec3f((lhs.Y * rhs.Z) - (lhs.Z * rhs.Y), (lhs.Z * rhs.X) - (lhs.X * rhs.Z), (lhs.X * rhs.Y) - (lhs.Y * rhs.X));
    }
}