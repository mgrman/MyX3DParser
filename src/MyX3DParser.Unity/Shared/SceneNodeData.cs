using MyX3DParser.Generated.Model.DataTypes;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MyX3DParser.Shared
{
    public struct SceneNodeData
    {
        public static readonly Matrix4x4 MirrorZ = Matrix4x4.Scale(new Vector3(1, 1, -1));

        public SceneNodeData(Matrix4x4 matrix, bool isVisible)
        {
            Matrix = matrix;
            IsVisible = isVisible;
        }

        public static SceneNodeData Root = new SceneNodeData(MirrorZ, true);
        public static SceneNodeData Deactivation = new SceneNodeData(Matrix4x4.identity, false);

        public Matrix4x4 Matrix { get; }

        public bool IsVisible { get; }


        public SceneNodeData CombineWithChild(SceneNodeData value)
        {
            return new SceneNodeData(this.Matrix * value.Matrix, this.IsVisible && value.IsVisible);
        }
    }
}
