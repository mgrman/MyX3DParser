using MyX3DParser.Generated;
using MyX3DParser.Generated.Model.DataTypes;
using MyX3DParser.Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace MyX3DParser.Tests
{
    public class SceneNodeDataTests
    {
        private const int Precision = 5;
        private ITestOutputHelper output;

        public SceneNodeDataTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Test_Identity()
        {
            var a = SceneNodeData.Identity;
            var b = SceneNodeData.Identity;

            var result = a.CombineWithChild(b);

            AssertEqual(Vec3f.ConstantValue_0_0_0, result.Translation);
            AssertEqual(Rotation.ConstantValue_0_0_1_0, result.Rotation);
            AssertEqual(Rotation.ConstantValue_0_0_1_0, result.ScaleOrientation);
            AssertEqual(Vec3f.ConstantValue_1_1_1, result.Scale1);
        }

        [Fact]
        public void Test_TranslationThenTranslation()
        {
            var a = SceneNodeData.Translate(new Vec3f(1, 0, 0));
            var b = SceneNodeData.Translate(new Vec3f(0, 1, 0));

            var result = a.CombineWithChild(b);

            AssertEqual(new Vec3f(1, 1, 0), result.Translation);
            AssertEqual(Rotation.ConstantValue_0_0_1_0, result.Rotation);
            AssertEqual(Rotation.ConstantValue_0_0_1_0, result.ScaleOrientation);
            AssertEqual(Vec3f.ConstantValue_1_1_1, result.Scale1);
        }

        [Fact]
        public void Test_ScaleThenTranslation()
        {
            var a = SceneNodeData.Scale(new Vec3f(2, 2, 2));
            var b = SceneNodeData.Translate(new Vec3f(1, 0, 0));

            var result = a.CombineWithChild(b);

            AssertEqual(new Vec3f(2, 0, 0), result.Translation);
            AssertEqual(Rotation.ConstantValue_0_0_1_0, result.Rotation);
            AssertEqual(Rotation.ConstantValue_0_0_1_0, result.ScaleOrientation);
            AssertEqual(new Vec3f(2, 2, 2), result.Scale1);
        }

        [Fact]
        public void Test_RotationThenTranslation()
        {
            var rotation = new Rotation(0, 1, 0, 45f / 180f * (float)Math.PI);
            var a = SceneNodeData.Rotate(rotation);
            var b = SceneNodeData.Translate(new Vec3f(1, 0, 0));

            var result = a.CombineWithChild(b);

            AssertEqual(new Vec3f(0.7071067f, 0, -0.7071068f), result.Translation);
            AssertEqual(rotation, result.Rotation);
            AssertEqual(Rotation.ConstantValue_0_0_1_0, result.ScaleOrientation);
            AssertEqual(Vec3f.ConstantValue_1_1_1, result.Scale1);
        }

        [Fact]
        public void Test_TranslationThenRotation()
        {
            var rotation = new Rotation(0, 1, 0, 45f / 180f * (float)Math.PI);
            var a = SceneNodeData.Translate(new Vec3f(1, 0, 0));
            var b = SceneNodeData.Rotate(rotation);

            var result = a.CombineWithChild(b);

            AssertEqual(new Vec3f(1, 0, 0), result.Translation);
            AssertEqual(rotation, result.Rotation);
            //AssertEqual(Rotation.ConstantValue_0_0_1_0, result.ScaleOrientation); // scale orientation irrelevant when scale is 1 1 1
            AssertEqual(Vec3f.ConstantValue_1_1_1, result.Scale1);
        }

        [Fact]
        public void Test_ScaleThenRotation()
        {
            var rotation = new Rotation(0, 1, 0, 45f / 180f * (float)Math.PI);
            var a = SceneNodeData.Scale(new Vec3f(2, 2, 2));
            var b = SceneNodeData.Rotate(rotation);

            var result = a.CombineWithChild(b);

            AssertEqual(Vec3f.ConstantValue_0_0_0, result.Translation);
            AssertEqual(rotation, result.Rotation);
            AssertEqual(rotation, result.ScaleOrientation);
            AssertEqual(new Vec3f(2, 2, 2), result.Scale1);
        }

        [Fact]
        public void Test_RotationThenRotation()
        {
            var rotation1 = new Rotation(0, 1, 0, 45f / 180f * (float)Math.PI);
            var rotation2 = new Rotation(0, 1, 0, 45f / 180f * (float)Math.PI);
            var a = SceneNodeData.Rotate(rotation1);
            var b = SceneNodeData.Rotate(rotation2);

            var result = a.CombineWithChild(b);

            AssertEqual(Vec3f.ConstantValue_0_0_0, result.Translation);
            AssertEqual(new Rotation(0, 1, 0, 90f / 180f * (float)Math.PI), result.Rotation);
            //AssertEqual(Rotation.ConstantValue_0_0_1_0, result.ScaleOrientation); // scale orientation irrelevant when scale is 1 1 1
            AssertEqual(Vec3f.ConstantValue_1_1_1, result.Scale1);
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


        private void AssertEqual(Rotation expectedValue, Rotation result)
        {
            try
            {
                Assert.Equal(expectedValue.X, result.X, Precision);
                Assert.Equal(expectedValue.Y, result.Y, Precision);
                Assert.Equal(expectedValue.Z, result.Z, Precision);
                Assert.Equal(expectedValue.Angle, result.Angle, Precision);
            }
            catch (EqualException)
            {

                throw new EqualException($"{ToRoundedString(expectedValue)} (rounded from {expectedValue})", $"{ToRoundedString(result)} (rounded from {result})");
            }
        }

        private static string ToRoundedString(Vec3f value)
        {
            return $"{ToRoundedString(value.X)},{ToRoundedString(value.Y)},{ToRoundedString(value.Z)}";
        }

        private static string ToRoundedString(Rotation value)
        {
            return $"{ToRoundedString(value.X)},{ToRoundedString(value.Y)},{ToRoundedString(value.Z)} {ToRoundedString(value.Angle/(float)Math.PI*180f)}°";
        }
        private static string ToRoundedString(float v)
        {
            return Math.Round(v, Precision).ToString(CultureInfo.InvariantCulture);
        }

    }
}
