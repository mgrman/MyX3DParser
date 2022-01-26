
using System;
using System.Collections.Generic;
using System.Linq;
using MyX3DParser.Generated;
using MyX3DParser.Generated.Model.DataTypes;


namespace MyX3DParser.Generated
{
    /// <summary>
    /// https://github.com/x3dom/x3dom/blob/68dd42ab8b9643a7ff1f310c852cb6534d6336e9/src/fields.js
    /// </summary>
    public static partial class Decomposer
    {
        public static (Vec3f translation, Quaternion rotation, Vec3f s, Quaternion scaleOrientation) Decompose(this Matrix4f matrix)
        {
            var translation = new Vec3f(matrix.M03, matrix.M13, matrix.M23);
            var matrixWithoutTranslation = matrix.ToMatrix3x3Array();

            var data = matrixWithoutTranslation.Decompose();

            return (translation,data.rotation.ToQuaternion(), data.scale.ToVec3f(), data.scaleOrientation.ToQuaternion());
        }

        private static float[,] ToMatrix3x3Array(this Matrix4f mat)
        {
            var arr = new float[3, 3];
            arr[0, 0] = mat[0, 0];
            arr[0, 1] = mat[0, 1];
            arr[0, 2] = mat[0, 2];
            arr[1, 0] = mat[1, 0];
            arr[1, 1] = mat[1, 1];
            arr[1, 2] = mat[1, 2];
            arr[2, 0] = mat[2, 0];
            arr[2, 1] = mat[2, 1];
            arr[2, 2] = mat[2, 2];
            return arr;
        }

        private static Quaternion ToQuaternion(this (float x, float y, float z, float w) val) => new Quaternion(val.x, val.y, val.z, val.w);
        private static Vec3f ToVec3f(this (float x, float y, float z) val) => new Vec3f(val.x, val.y, val.z);

    }
}
