using MyX3DParser.Generated.Model.DataTypes;
using MyX3DParser.Shared;

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
                position = position.CombineWithChild(SceneNodeData.Deactivation);
                return;
            }


            var translationMat = Shared.SceneNodeData.Translate(translation.Value);

            var centerMat = Shared.SceneNodeData.Translate(center.Value);
            var centerInverseMat = Shared.SceneNodeData.Translate(-center.Value);

            var rotationMat = Shared.SceneNodeData.Rotate(rotation.Value);

            var scaleOrientationMat = Shared.SceneNodeData.Rotate(scaleOrientation.Value);
            var scaleOrientationInverseMat = Shared.SceneNodeData.Rotate(-scaleOrientation.Value);


            var scaleMat = Shared.SceneNodeData.Scale(scale.Value);

            var resultMatrix = position.CombineWithChild( translationMat).CombineWithChild( centerMat).CombineWithChild( rotationMat ).CombineWithChild(scaleOrientationMat).CombineWithChild(scaleMat ).CombineWithChild( scaleOrientationInverseMat).CombineWithChild( centerInverseMat );


            position = resultMatrix;
        }
    }
}