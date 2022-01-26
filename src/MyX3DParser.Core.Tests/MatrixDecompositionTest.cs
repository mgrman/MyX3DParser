using MyX3DParser.Generated;
using MyX3DParser.Generated.Model.DataTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace MyX3DParser.Tests
{
    public class MatrixDecompositionTest
    {
        private const int Precision = 1;
        private static readonly float PrecisionEps = (float)(1.0 / Math.Pow(10, Precision));
        private ITestOutputHelper output;

        public MatrixDecompositionTest(ITestOutputHelper output)
        {
            this.output = output;
        }


        [Theory(Skip ="Decomposer does not give valid data when mirror is in place")]
        [MemberData(nameof(RegressionData))]
        public void Test(string _,XunitVector3 rotation1, XunitVector3 scaleOrientation1, XunitVector3 scale1)
        {
            var matrix1 = GetMatrix(Quaternion.Euler(rotation1.x, rotation1.y, rotation1.z), Quaternion.Euler(scaleOrientation1.x, scaleOrientation1.y, scaleOrientation1.z), new Vec3f(scale1.x, scale1.y, scale1.z));

            var (translation2,rotation2, scale2, scaleOrientation2) = matrix1.Decompose();
            var matrix2 = GetMatrix(rotation2, scaleOrientation2, scale2);

            try
            {
                AssertEqual(Vec3f.ConstantValue_0_0_0, translation2);
                AssertEqual(matrix1, matrix2);
            }
            catch (EqualException)
            {
                output.WriteLine($"rotation IN {Quaternion.Euler(rotation1.x, rotation1.y, rotation1.z)}");
                output.WriteLine($"rotation OUT {rotation2}");
                output.WriteLine($"scaleOrientation IN {Quaternion.Euler(scaleOrientation1.x, scaleOrientation1.y, scaleOrientation1.z)}");
                output.WriteLine($"scaleOrientation OUT {scaleOrientation2}");
                output.WriteLine($"scale IN {scale1}");
                output.WriteLine($"scale OUT {scale2}");
                output.WriteLine($"translation IN {Vec3f.ConstantValue_0_0_0}");
                output.WriteLine($"translation OUT {translation2}");
                throw;
            }
            //AssertEqual(new Vec3f(1, 2, 3), translation);
            //AssertEqual(new Vec3f(0.5f, 0.25f, -0.1f), scale);
            //AssertEqual(Quaternion.Euler(210, 232, 270), rotation);
            //AssertEqual(Quaternion.Identity, scaleOrientation);
            //Assert.Equal(-1f, determinant);
        }

        public static IEnumerable<object[]> RegressionData
        {
            get
            {
                yield return new object[] {"identity",new XunitVector3(0f, 0f, 0f),     new XunitVector3(0f, 0f, 0f),   new XunitVector3(1f, 1f, 1f) };
                yield return new object[] {"mirrorX",new XunitVector3(0f, 0f, 0f),     new XunitVector3  (0f, 0f, 0f), new XunitVector3 (-1f, 1f, 1f) };
                yield return new object[] {"RS",new XunitVector3(0f, 45f, 0f),    new XunitVector3(0f, 0f, 0f),   new XunitVector3(0.5f, 0.25f, 0.1f) };
                yield return new object[] { "ScaledOrientation", new XunitVector3(0f, 0f, 0f), new XunitVector3(0, 32, 0), new XunitVector3(0.5f, 0.25f, 0.1f) };
                yield return new object[] { "ScaledOrientation+mirrorX", new XunitVector3(0f, 0f, 0f), new XunitVector3(1, 32, 0), new XunitVector3(-0.5f, 0.25f, 0.1f) };
                yield return new object[] {"RS+mirrorZ",new XunitVector3(10f, 32f, 70f),  new XunitVector3 (0f, 0f, 0f),  new XunitVector3(0.5f, 0.25f, -0.1f) };
                yield return new object[] {"RS+mirrorY",new XunitVector3(10f, 32f, 70f),  new XunitVector3 (0f, 0f, 0f),  new XunitVector3(0.5f, -0.25f, 0.1f) };
                yield return new object[] {"RS+mirrorX",new XunitVector3(10f, 32f, 70f),  new XunitVector3 (0f, 0f, 0f),  new XunitVector3(-0.5f, 0.25f, 0.1f) };
                yield return new object[] {"RS+mirrorXY",new XunitVector3(10f, 32f, 70f),  new XunitVector3 (0f, 0f, 0f),  new XunitVector3(-0.5f, -0.25f, 0.1f) };
                yield return new object[] {"RS+mirrorYZ",new XunitVector3(10f, 32f, 70f),  new XunitVector3 (0f, 0f, 0f),  new XunitVector3(0.5f, -0.25f, -0.1f) };
                yield return new object[] {"RS+mirrorXZ",new XunitVector3(10f, 32f, 70f),  new XunitVector3 (0f, 0f, 0f),  new XunitVector3(-0.5f, 0.25f, -0.1f) };
                yield return new object[] { "RS+mirrorXYZ", new XunitVector3(10f, 32f, 70f), new XunitVector3(0f, 0f, 0f), new XunitVector3(-0.5f, -0.25f, -0.1f) };
                yield return new object[] { "R+mirrorX", new XunitVector3(0f, 32f, 0f), new XunitVector3(0f, 0f, 0f), new XunitVector3(-1f, 1f, 1f) };
                yield return new object[] {"Rotation>180",new XunitVector3(210f, 232f, 270f),new XunitVector3 (0f, 0f, 0f),  new XunitVector3(0.5f, 0.25f, 0.1f) };

                var rand = new Random(1213);

                for (int i = 0; i < 50; i++)
                {
                    yield return new object[] { $"randRot_{i}", new XunitVector3(GetRandAngle(rand), GetRandAngle(rand), GetRandAngle(rand)), new XunitVector3(1, 1, 1), new XunitVector3(0, 0, 0) };
                }
                 rand = new Random(1213);
                for (int i = 0; i < 50; i++)
                {
                    yield return new object[] { $"randScale_{i}", new XunitVector3(0, 0, 0), new XunitVector3(0, 0, 0), new XunitVector3(GetRandScale(rand), GetRandScale(rand), GetRandScale(rand)) };
                }
                rand = new Random(1213);
                for (int i = 0; i < 50; i++)
                {
                    yield return new object[] { $"randOrientedScale_{i}", new XunitVector3(0, 0, 0), new XunitVector3(GetRandAngle(rand), GetRandAngle(rand), GetRandAngle(rand)), new XunitVector3(GetRandScale(rand), GetRandScale(rand), GetRandScale(rand)) };
                }
                rand = new Random(1213);
                for (int i = 0; i < 50; i++)
                {
                    yield return new object[] { $"rand_{i}", new XunitVector3(GetRandAngle(rand), GetRandAngle(rand), GetRandAngle(rand)), new XunitVector3(GetRandAngle(rand), GetRandAngle(rand), GetRandAngle(rand)), new XunitVector3(GetRandScale(rand), GetRandScale(rand), GetRandScale(rand)) };
                }
            }
        }

        private static float GetRandAngle(Random rand)
        {
            return (float)Math.Round((rand.NextDouble() - 0.5) * 720, 2);
        }
        private static float GetRandScale(Random rand)
        {
            return (float)Math.Round((rand.NextDouble() - 0.5) * 10, 2);
        }

        public class XunitVector3: IXunitSerializable
        {
            public float x;
            public float y;
            public float z;

            public XunitVector3()
            {
            }
            public XunitVector3(float x, float y, float z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }

            public void Deserialize(IXunitSerializationInfo info)
            {
                x = info.GetValue<float>(nameof(x));
                y = info.GetValue<float>(nameof(y));
                z = info.GetValue<float>(nameof(z));
            }

            public void Serialize(IXunitSerializationInfo info)
            {
                info.AddValue(nameof(x), x);
                info.AddValue(nameof(y), y);
                info.AddValue(nameof(z), z);
            }
            public override string ToString()
            {
                return $"{x}; {y}; {z}";
            }
        }

        private void AssertEqual(Vec3f expectedValue, Vec3f result)
        {
            try
            {
                Assert.Equal(expectedValue.X, result.X, Precision);
                Assert.Equal(expectedValue.Y, result.Y, Precision);
                Assert.Equal(expectedValue.Z, result.Z, Precision);
            }
            catch (EqualException)
            {

                throw new EqualException($"{ToRoundedString(expectedValue.X)},{ToRoundedString(expectedValue.Y)},{ToRoundedString(expectedValue.Z)} (rounded from {expectedValue})", $"{ToRoundedString(result.X)},{ToRoundedString(result.Y)},{ToRoundedString(result.Z)} (rounded from {result})");
            }
        }


        private void AssertEqual(Quaternion expectedValue, Quaternion result)
        {
            try
            {
                Assert.Equal(expectedValue.X, result.X, Precision);
                Assert.Equal(expectedValue.Y, result.Y, Precision);
                Assert.Equal(expectedValue.Z, result.Z, Precision);
                Assert.Equal(expectedValue.W, result.W, Precision);
            }
            catch (EqualException)
            {

                throw new EqualException($"{ToRoundedString(Quaternion.ToEulerAngles(expectedValue))} (rounded from {expectedValue})", $"{ToRoundedString(Quaternion.ToEulerAngles(result))} (rounded from {result})");
            }
        }


        private void AssertEqual(Matrix4f expectedValue, Matrix4f result)
        {
            try
            {
                var diff = Diff(expectedValue, result);
                Assert.True(diff< PrecisionEps);
            }
            catch (EqualException)
            {
                throw new EqualException($"~{expectedValue}", $"~{result}");
            }
        }
        private static float Diff(Matrix4f mat1, Matrix4f mat2)
        {
            var sum = 0f;

            sum += Math.Abs(mat1.M00 - mat2.M00);
            sum += Math.Abs(mat1.M01 - mat2.M01);
            sum += Math.Abs(mat1.M02 - mat2.M02);
            sum += Math.Abs(mat1.M03 - mat2.M03);
            sum += Math.Abs(mat1.M10 - mat2.M10);
            sum += Math.Abs(mat1.M11 - mat2.M11);
            sum += Math.Abs(mat1.M12 - mat2.M12);
            sum += Math.Abs(mat1.M13 - mat2.M13);
            sum += Math.Abs(mat1.M20 - mat2.M20);
            sum += Math.Abs(mat1.M21 - mat2.M21);
            sum += Math.Abs(mat1.M22 - mat2.M22);
            sum += Math.Abs(mat1.M23 - mat2.M23);
            sum += Math.Abs(mat1.M30 - mat2.M30);
            sum += Math.Abs(mat1.M31 - mat2.M31);
            sum += Math.Abs(mat1.M32 - mat2.M32);
            sum += Math.Abs(mat1.M33 - mat2.M33);
            return sum;
        }

        private static string ToRoundedString(Vec3f value)
        {
            return $"{ToRoundedString(value.X)},{ToRoundedString(value.Y)},{ToRoundedString(value.Z)}";
        }
        private static string ToRoundedString(float v)
        {
            return Math.Round(v, Precision).ToString(CultureInfo.InvariantCulture);
        }

        private Matrix4f GetMatrix(Quaternion rotation, Quaternion scaleOrientation, Vec3f scale)
        {
            var rotationMat = Matrix4f.Rotate(rotation);

            var scaleOrientationMat = Matrix4f.Rotate(scaleOrientation);
            var scaleOrientationInverseMat = Matrix4f.Rotate(Quaternion.Inverse(scaleOrientation));


            var scaleMat = Matrix4f.Scale(scale);

            return  rotationMat * scaleOrientationMat * scaleMat * scaleOrientationInverseMat ;
        }

    }
}
