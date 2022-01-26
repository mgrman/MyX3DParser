using MyX3DParser.Generated;
using MyX3DParser.Generated.Model.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace MyX3DParser.Tests
{
    public class TransformCombinationsTest
    {
        public const float Deg2Rad = 0.01745329f;

        private ITestOutputHelper output;

        public TransformCombinationsTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Test_Identity()
        {
            var a = new TransformData();
            var b = new TransformData();
            output.WriteLine($"a{Environment.NewLine}{a}");
            output.WriteLine($"b{Environment.NewLine}{b}");

            var result = a * b;

            output.WriteLine($"result{Environment.NewLine}{result}");
        }


        private class TransformData
        {
          public  Vec3f center { get; set; } = new Vec3f(0, 0, 0);
          public  Rotation rotation { get; set; } = new Rotation(0, 0, 1, 0);
          public  Vec3f scale { get; set; } = new Vec3f(1, 1, 1);
          public  Rotation scaleOrientation { get; set; } = new Rotation(0, 0, 1, 0);
          public  Vec3f translation { get; set; } = new Vec3f(0, 0, 0);


            public static TransformData operator *(TransformData a, TransformData b)
            {
                var aMat = a.GetMatrix();
                var bMat = b.GetMatrix();
                var resMat = aMat * bMat;




                var result = new TransformData()
                {
                    translation = resMat.MultiplyPoint(new Vec3f(0, 0, 0))
                };

                return result;
            }

            private Matrix4f GetMatrix()
            {
                var translationMat = Matrix4f.Translate(translation);

                var centerMat = Matrix4f.Translate(center);
                var centerInverseMat = Matrix4f.Translate(-center);

                var rotationMat = Matrix4f.Rotate(rotation);

                var scaleOrientationMat = Matrix4f.Rotate(scaleOrientation);
                var scaleOrientationInverseMat = Matrix4f.Rotate(-scaleOrientation);


                var scaleMat = Matrix4f.Scale(scale);

                return  translationMat * centerMat * rotationMat * scaleOrientationMat * scaleMat * scaleOrientationInverseMat * centerInverseMat;
            }


            public override string ToString()
            {
                return $"translation {translation}" + Environment.NewLine +
                $"rotation {rotation}" + Environment.NewLine +
                $"rotation {scale}" + Environment.NewLine +
                $"rotation {scaleOrientation}" + Environment.NewLine +
                $"rotation {center}" ;
            }
        }
    }
}
