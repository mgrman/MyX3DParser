using MyX3DParser.Generated.Model.DataTypes;
using MyX3DParser.Shared;
using System.Numerics;

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
                position = new SceneNodeData(position.Matrix, false);
                return;
            }


            var translationMat = Matrix4x4.CreateTranslation(translation.Value);

            var centerMat = Matrix4x4.CreateTranslation(center.Value);
            var centerInverseMat = Matrix4x4.CreateTranslation(-center.Value);

            var rotationMat = Matrix4x4.CreateFromQuaternion(rotation.Value);

            var scaleOrientationMat = Matrix4x4.CreateFromQuaternion(scaleOrientation.Value);
            var scaleOrientationInverseMat = Matrix4x4.CreateFromQuaternion(Quaternion.Inverse(scaleOrientation.Value));


            var scaleMat = Matrix4x4.CreateScale(scale.Value);

            var resultMatrix = centerInverseMat * scaleOrientationInverseMat * scaleMat * scaleOrientationMat * rotationMat * centerMat * translationMat * position.Matrix;

            position = new SceneNodeData(resultMatrix, position.IsVisible);
        }
    }
}