using MyX3DParser.Generated.Model.DataTypes;
using System;
using System.Numerics;

namespace MyX3DParser.Shared
{
    public static class Convertors
    {
        public static (float x, float y, float z) ToTuple(this Vector3 value) => (value.X, value.Y, value.Z);
    }
}
