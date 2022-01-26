using MyX3DParser.Generated.Model.DataTypes;
using UnityEngine;

namespace MyX3DParser.Generated.Model.Nodes
{
    public partial class Transform
    {

        partial void Initialize()
        {
            this.@addChildren.OnChange += OnUpdateTransformation;
            this.@center.OnChange += OnUpdateTransformation;
            this.@children.OnChange += OnUpdateTransformation;
            this.@removeChildren.OnChange += OnUpdateTransformation;
            this.@rotation.OnChange += OnUpdateTransformation;
            this.@scale.OnChange += OnUpdateTransformation;
            this.@scaleOrientation.OnChange += OnUpdateTransformation;
            this.@translation.OnChange += OnUpdateTransformation;
        }


        private void OnUpdateTransformation(IX3DField field)
        {
            UpdateMatrices();
        }

        partial void UpdatePositionInner(ref Shared.SceneNodeData position)
        {
            if (scale.Value == Vec3f.ConstantValue_0_0_0)
            {
                position = new Shared.SceneNodeData(position.Matrix, false);
                return;
            }

            var trs = Matrix4x4.TRS(translation.Value + center.Value, rotation.Value * scaleOrientation.Value, scale.Value);
            var trs2 = Matrix4x4.TRS(- center.Value, Quaternion.Inverse(scaleOrientation.Value), Vector3.one);


            var resultMatrix = position.Matrix * trs * trs2;

            position= new Shared.SceneNodeData(resultMatrix, position.IsVisible);
        }
    }
}