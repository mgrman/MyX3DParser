using MyX3DParser.Generated;
using MyX3DParser.Generated.Model.DataTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyX3DParser.Shared
{
    public struct SceneNodeData
    {
        private SceneNodeData(Vec3f translation, Rotation rotation, Rotation scaleOrientation, Vec3f scale, bool isVisible)
        {;
            this.translation = translation;
            this.rotation = rotation;
            this.scaleOrientation = scaleOrientation;
            this.scale = scale;
            IsVisible = isVisible;


            var translationMat = Matrix4f.Translate(translation);
            var rotationMat = Matrix4f.Rotate(rotation);
            var scaleOrientationMat = Matrix4f.Rotate(scaleOrientation);
            var scaleOrientationInverseMat = Matrix4f.Rotate(-scaleOrientation);
            var scaleMat = Matrix4f.Scale(scale);
            Matrix= translationMat *  rotationMat * scaleOrientationMat * scaleMat * scaleOrientationInverseMat ;
        }


        public static readonly SceneNodeData Identity = new SceneNodeData(Vec3f.ConstantValue_0_0_0, MyX3DParser.Generated.Model.DataTypes.Rotation.ConstantValue_0_0_1_0, MyX3DParser.Generated.Model.DataTypes.Rotation.ConstantValue_0_0_1_0, Vec3f.ConstantValue_1_1_1, true);

        public static readonly SceneNodeData Root = Identity;

        public Matrix4f Matrix { get; }

        public bool IsVisible { get; }

        public Vec3f Translation => translation;

        public Rotation Rotation => rotation;

        public Rotation ScaleOrientation => scaleOrientation;

        public Vec3f Scale1 => scale;

        public static readonly SceneNodeData Deactivation = new SceneNodeData(Vec3f.ConstantValue_0_0_0, MyX3DParser.Generated.Model.DataTypes.Rotation.ConstantValue_0_0_1_0, MyX3DParser.Generated.Model.DataTypes.Rotation.ConstantValue_0_0_1_0, Vec3f.ConstantValue_1_1_1, false);

        private readonly Vec3f translation;
        private readonly Rotation rotation;
        private readonly Rotation scaleOrientation;
        private readonly Vec3f scale;

        public static SceneNodeData Translate(Vec3f value)
        {
            return new SceneNodeData(value, MyX3DParser.Generated.Model.DataTypes.Rotation.ConstantValue_0_0_1_0, MyX3DParser.Generated.Model.DataTypes.Rotation.ConstantValue_0_0_1_0, Vec3f.ConstantValue_1_1_1, true);
        }

        public static SceneNodeData Scale(Vec3f value)
        {
            return new SceneNodeData(Vec3f.ConstantValue_0_0_0, MyX3DParser.Generated.Model.DataTypes.Rotation.ConstantValue_0_0_1_0, MyX3DParser.Generated.Model.DataTypes.Rotation.ConstantValue_0_0_1_0, value, true);
        }

        public static SceneNodeData Rotate(Rotation value)
        {
            return new SceneNodeData(Vec3f.ConstantValue_0_0_0, value, MyX3DParser.Generated.Model.DataTypes.Rotation.ConstantValue_0_0_1_0, Vec3f.ConstantValue_1_1_1, true);
        }

        public SceneNodeData CombineWithChild(SceneNodeData value)
        {
            if (value.ScaleOrientation.Angle != 0)
            {
                throw new InvalidOperationException();
            }


            //Matrix = translationMat * rotationMat * scaleOrientationMat * scaleMat * scaleOrientationInverseMat;

            return this.CombineWithTranslation(value.Translation)
                .CombineWithRotation(value.Rotation)
                .CombineWithRotation(value.ScaleOrientation).CombineWithScale(value.Scale1)
                .CombineWithRotation(-value.ScaleOrientation)
                .CombineWithIsVisible(value.IsVisible);
        }
        public SceneNodeData CombineWithTranslation(Vec3f value)
        {
            var newPosition = this.Translation+ this.Matrix.MultiplyVector(value);

            return new SceneNodeData(newPosition, this.Rotation,this.ScaleOrientation,this.Scale1, this.IsVisible);
        }
        public SceneNodeData CombineWithRotation(Rotation value)
        {
            var newRotation = (this.Rotation.ToQuaternion() * value.ToQuaternion()).ToAngleAxis();
            var newScaleOrientation = (this.ScaleOrientation.ToQuaternion() * value.ToQuaternion()).ToAngleAxis();

            return new SceneNodeData(this.Translation, newRotation, newScaleOrientation,this.Scale1,this.IsVisible );
        }
        public SceneNodeData CombineWithScale(Vec3f value)
        {
         //   if(this.ScaleOrientation== Rotation.ConstantValue_0_0_1_0)
           // {
                return new SceneNodeData(this.Translation, this.Rotation, this.ScaleOrientation, this.Scale1* value, this.IsVisible);
            //}

            //var newRotation = (this.rotation.ToQuaternion() * value.rotation.ToQuaternion()).ToAngleAxis();

            //return new SceneNodeData(newPosition, newRotation, this.IsVisible && value.IsVisible);
        }

        public SceneNodeData CombineWithIsVisible(bool value)
        {
            return new SceneNodeData(this.Translation, this.Rotation, this.ScaleOrientation, this.Scale1, this.IsVisible && value);
        }

    }
}
