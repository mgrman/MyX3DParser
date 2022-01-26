using MyX3DParser.Generated.Model.DataTypes;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MyX3DParser.Shared
{
    public struct SceneNodeData
    {
        public SceneNodeData(Matrix4x4 matrix, bool isVisible)
        {
            Matrix = matrix;
            IsVisible = isVisible;
        }

        public static SceneNodeData Root = new SceneNodeData(Matrix4x4.Identity, true);
        public static SceneNodeData Deactivation = new SceneNodeData(Matrix4x4.Identity, false);


        public Matrix4x4 Matrix { get; }

        public bool IsVisible { get; }

        public SceneNodeData CombineWithChild(SceneNodeData value)
        {
            return new SceneNodeData(this.Matrix * value.Matrix, this.IsVisible && value.IsVisible);
        }
    }
}
