using MyX3DParser.Generated.Model.DataTypes;
using System;
using UnityEngine;
using Color = MyX3DParser.Generated.Model.DataTypes.Color;
using U_Vector3 = UnityEngine.Vector3;
using U_Quaternion = UnityEngine.Quaternion;
using U_Matrix4x4 = UnityEngine.Matrix4x4;
using U_Color = UnityEngine.Color;

namespace MyX3DParser.Shared
{
    public static class Convertors
    {
        public static (float x, float y, float z) ToTuple(this Vector3 value) => (value.x, value.y, value.z);
    }
}
