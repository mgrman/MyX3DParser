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
    internal class NumericsDataTypeBuilder : BaseProxyDataTypeBuilder
    {
        static IReadOnlyDictionary<string, (string type, int? componentCount)> DataTypesConfigs = new Dictionary<string, (string type, int? componentCount)>()
        {
            {"Matrix3d", ("System.Numerics.Matrix4x4",9)},
            {"Matrix3f", ("System.Numerics.Matrix4x4",9)},
            {"Matrix4d", ("System.Numerics.Matrix4x4",16)},
            {"Matrix4f", ("System.Numerics.Matrix4x4",16)},
            {"Rotation", ("System.Numerics.Quaternion",4)},
            {"Vec2d", ("System.Numerics.Vector2",2)},
            {"Vec2f", ("System.Numerics.Vector2",2)},
            {"Vec3d", ("System.Numerics.Vector3",3)},
            {"Vec3f", ("System.Numerics.Vector3",3)},
            {"Vec4d", ("System.Numerics.Vector4",4)},
            {"Vec4f", ("System.Numerics.Vector4",4)},
        };

        public NumericsDataTypeBuilder(string name)
            : base(name, DataTypesConfigs[name].type, DataTypesConfigs[name].componentCount)
        {

        }

        public static bool IsSupported(string typeName)
        {
            return DataTypesConfigs.ContainsKey(typeName);
        }

    }
}