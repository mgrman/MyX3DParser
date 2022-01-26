using Microsoft.VisualBasic.CompilerServices;
using MyX3DParser.Model.Builders;
using MyX3DParser.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyX3DParser.Model.Builders
{
    [BuilderCategory("DataTypes")]
    internal class UnityDataTypeBuilder : BaseProxyDataTypeBuilder
    {
        static IReadOnlyDictionary<string, (string type, int? componentCount)> DataTypesConfigs = new Dictionary<string, (string type, int? componentCount)>()
        {
            {"Color", ("UnityEngine.Color",3)},
            {"ColorRGBA", ("UnityEngine.Color",4)},
            {"Matrix3d", ("UnityEngine.Matrix4x4",9)},
            {"Matrix3f", ("UnityEngine.Matrix4x4",9)},
            {"Matrix4d", ("UnityEngine.Matrix4x4",16)},
            {"Matrix4f", ("UnityEngine.Matrix4x4",16)},
            {"Rotation", ("UnityEngine.Quaternion",4)},
            {"Vec2d", ("UnityEngine.Vector2",2)},
            {"Vec2f", ("UnityEngine.Vector2",2)},
            {"Vec3d", ("UnityEngine.Vector3",3)},
            {"Vec3f", ("UnityEngine.Vector3",3)},
            {"Vec4d", ("UnityEngine.Vector4",4)},
            {"Vec4f", ("UnityEngine.Vector4",4)},
            {"Image", ("UnityEngine.Texture2D",null)}
        };

        public UnityDataTypeBuilder(string name)
            :base(name, DataTypesConfigs[name].type, DataTypesConfigs[name].componentCount)
        {
        }

        public static bool IsSupported(string typeName)
        {
            return DataTypesConfigs.ContainsKey(typeName);
        }

    }
}