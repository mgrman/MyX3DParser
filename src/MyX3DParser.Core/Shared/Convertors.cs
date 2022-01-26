using MyX3DParser.Generated.Model.DataTypes;
using System;

namespace MyX3DParser.Shared
{
    public static class Convertors
    {
        public static (float x, float y, float z) ToTuple(this Vec3f value) => (value.X, value.Y, value.Z);
    }
}
