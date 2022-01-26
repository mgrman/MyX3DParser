
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyX3DParser.Generated.Model.AbstractNodes;
using MyX3DParser.Generated.Model.DataTypes;


namespace MyX3DParser.Generated.Model.DataTypes
{
    public partial struct Matrix4f
    {
        public static readonly Matrix4f Zero = new Matrix4f(0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f);

        public static readonly Matrix4f Identity = new Matrix4f(1f, 0.0f, 0.0f, 0.0f, 0.0f, 1f, 0.0f, 0.0f, 0.0f, 0.0f, 1f, 0.0f, 0.0f, 0.0f, 0.0f, 1f);

        public float this[int x, int y]
        {
            get
            {
                if (x == 0 && y == 0) return M00;
                if (x == 0 && y == 1) return M01;
                if (x == 0 && y == 2) return M02;
                if (x == 0 && y == 3) return M03;
                if (x == 1 && y == 0) return M10;
                if (x == 1 && y == 1) return M11;
                if (x == 1 && y == 2) return M12;
                if (x == 1 && y == 3) return M13;
                if (x == 2 && y == 0) return M20;
                if (x == 2 && y == 1) return M21;
                if (x == 2 && y == 2) return M22;
                if (x == 2 && y == 3) return M23;
                if (x == 3 && y == 0) return M30;
                if (x == 3 && y == 1) return M31;
                if (x == 3 && y == 2) return M32;
                if (x == 3 && y == 3) return M33;
                throw new IndexOutOfRangeException();
            }
        }

        public Vec4f GetColumn(int i)
        {
            switch (i)
            {
                case 0:
                    return new Vec4f(this.M00, this.M10, this.M20, this.M30);

                case 1:
                    return new Vec4f(this.M01, this.M11, this.M21, this.M31);

                case 2:
                    return new Vec4f(this.M02, this.M12, this.M22, this.M32);

                case 3:
                    return new Vec4f(this.M03, this.M13, this.M23, this.M33);

                default:
                    throw new IndexOutOfRangeException();
            }
        }

        public double Determinant => ((((((((((((this.M03 * this.M12 * this.M21 * this.M30) - (this.M02 * this.M13 * this.M21 * this.M30) - (this.M03 * this.M11 * this.M22 * this.M30)) + (this.M01 * this.M13 * this.M22 * this.M30) + (this.M02 * this.M11 * this.M23 * this.M30)) - (this.M01 * this.M12 * this.M23 * this.M30) - (this.M03 * this.M12 * this.M20 * this.M31)) + (this.M02 * this.M13 * this.M20 * this.M31) + (this.M03 * this.M10 * this.M22 * this.M31)) - (this.M00 * this.M13 * this.M22 * this.M31) - (this.M02 * this.M10 * this.M23 * this.M31)) + (this.M00 * this.M12 * this.M23 * this.M31) + (this.M03 * this.M11 * this.M20 * this.M32)) - (this.M01 * this.M13 * this.M20 * this.M32) - (this.M03 * this.M10 * this.M21 * this.M32)) + (this.M00 * this.M13 * this.M21 * this.M32) + (this.M01 * this.M10 * this.M23 * this.M32)) - (this.M00 * this.M11 * this.M23 * this.M32) - (this.M02 * this.M11 * this.M20 * this.M33)) + (this.M01 * this.M12 * this.M20 * this.M33) + (this.M02 * this.M10 * this.M21 * this.M33)) - (this.M00 * this.M12 * this.M21 * this.M33) - (this.M01 * this.M10 * this.M22 * this.M33)) + (this.M00 * this.M11 * this.M22 * this.M33);

        public Matrix4f Inverse => Matrix4X4FInvertor.Invert(this);

        public static Matrix4f operator *(Matrix4f lhs, Matrix4f rhs)
        {
            var m00 = (lhs.M00 * rhs.M00) + (lhs.M01 * rhs.M10) + (lhs.M02 * rhs.M20) + (lhs.M03 * rhs.M30);
            var m01 = (lhs.M00 * rhs.M01) + (lhs.M01 * rhs.M11) + (lhs.M02 * rhs.M21) + (lhs.M03 * rhs.M31);
            var m02 = (lhs.M00 * rhs.M02) + (lhs.M01 * rhs.M12) + (lhs.M02 * rhs.M22) + (lhs.M03 * rhs.M32);
            var m03 = (lhs.M00 * rhs.M03) + (lhs.M01 * rhs.M13) + (lhs.M02 * rhs.M23) + (lhs.M03 * rhs.M33);
            var m10 = (lhs.M10 * rhs.M00) + (lhs.M11 * rhs.M10) + (lhs.M12 * rhs.M20) + (lhs.M13 * rhs.M30);
            var m11 = (lhs.M10 * rhs.M01) + (lhs.M11 * rhs.M11) + (lhs.M12 * rhs.M21) + (lhs.M13 * rhs.M31);
            var m12 = (lhs.M10 * rhs.M02) + (lhs.M11 * rhs.M12) + (lhs.M12 * rhs.M22) + (lhs.M13 * rhs.M32);
            var m13 = (lhs.M10 * rhs.M03) + (lhs.M11 * rhs.M13) + (lhs.M12 * rhs.M23) + (lhs.M13 * rhs.M33);
            var m20 = (lhs.M20 * rhs.M00) + (lhs.M21 * rhs.M10) + (lhs.M22 * rhs.M20) + (lhs.M23 * rhs.M30);
            var m21 = (lhs.M20 * rhs.M01) + (lhs.M21 * rhs.M11) + (lhs.M22 * rhs.M21) + (lhs.M23 * rhs.M31);
            var m22 = (lhs.M20 * rhs.M02) + (lhs.M21 * rhs.M12) + (lhs.M22 * rhs.M22) + (lhs.M23 * rhs.M32);
            var m23 = (lhs.M20 * rhs.M03) + (lhs.M21 * rhs.M13) + (lhs.M22 * rhs.M23) + (lhs.M23 * rhs.M33);
            var m30 = (lhs.M30 * rhs.M00) + (lhs.M31 * rhs.M10) + (lhs.M32 * rhs.M20) + (lhs.M33 * rhs.M30);
            var m31 = (lhs.M30 * rhs.M01) + (lhs.M31 * rhs.M11) + (lhs.M32 * rhs.M21) + (lhs.M33 * rhs.M31);
            var m32 = (lhs.M30 * rhs.M02) + (lhs.M31 * rhs.M12) + (lhs.M32 * rhs.M22) + (lhs.M33 * rhs.M32);
            var m33 = (lhs.M30 * rhs.M03) + (lhs.M31 * rhs.M13) + (lhs.M32 * rhs.M23) + (lhs.M33 * rhs.M33);
            var result= new Matrix4f(m00, m01, m02, m03, m10, m11, m12, m13, m20, m21, m22, m23, m30, m31, m32, m33);



            return result;
        }

        public static Vec4f operator *(Matrix4f lhs, Vec4f v)
        {
            var x = (float)((lhs.M00 * (double)v.X) + (lhs.M01 * (double)v.Y) + (lhs.M02 * (double)v.Z) + (lhs.M03 * (double)v.W));
            var y = (float)((lhs.M10 * (double)v.X) + (lhs.M11 * (double)v.Y) + (lhs.M12 * (double)v.Z) + (lhs.M13 * (double)v.W));
            var z = (float)((lhs.M20 * (double)v.X) + (lhs.M21 * (double)v.Y) + (lhs.M22 * (double)v.Z) + (lhs.M23 * (double)v.W));
            var w = (float)((lhs.M30 * (double)v.X) + (lhs.M31 * (double)v.Y) + (lhs.M32 * (double)v.Z) + (lhs.M33 * (double)v.W));
            return new Vec4f(x, y, z, w);
        }

        public static Matrix4f Scale(Vec3f v) => new Matrix4f(v.X, 0.0f, 0.0f, 0.0f, 0.0f, v.Y, 0.0f, 0.0f, 0.0f, 0.0f, v.Z, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f);

        public static Matrix4f Translate(Vec3f v) => new Matrix4f(1.0f, 0.0f, 0.0f, v.X, 0.0f, 1.0f, 0.0f, v.Y, 0.0f, 0.0f, 1.0f, v.Z, 0.0f, 0.0f, 0.0f, 1.0f);

        public static Matrix4f Rotate(Rotation q)
        {
            var qX = q.X * (float)Math.Sin(q.Angle / 2);
            var qY = q.Y * (float)Math.Sin(q.Angle / 2);
            var qZ = q.Z * (float)Math.Sin(q.Angle / 2);
            var qW = (float)Math.Cos(q.Angle / 2);

            return Rotate(new Quaternion(qX, qY, qZ, qW));
        }

            public static Matrix4f Rotate(Quaternion q)
            {
            var qX = q.X;
             var qY = q.Y ;
             var qZ = q.Z ;
             var qW = q.W ;

                var num = qX * 2f;
            var num2 = qY * 2f;
            var num3 = qZ * 2f;
            var num4 = qX * num;
            var num5 = qY * num2;
            var num6 = qZ * num3;
            var num7 = qX * num2;
            var num8 = qX * num3;
            var num9 = qY * num3;
            var num10 = qW * num;
            var num11 = qW * num2;
            var num12 = qW * num3;
            var m00 = 1f - (num5 + num6);
            var m10 = num7 + num12;
            var m20 = num8 - num11;
            var m30 = 0f;
            var m01 = num7 - num12;
            var m11 = 1f - (num4 + num6);
            var m21 = num9 + num10;
            var m31 = 0f;
            var m02 = num8 + num11;
            var m12 = num9 - num10;
            var m22 = 1f - (num4 + num5);
            var m32 = 0f;
            var m03 = 0f;
            var m13 = 0f;
            var m23 = 0f;
            var m33 = 1f;
            return new Matrix4f(m00, m01, m02, m03, m10, m11, m12, m13, m20, m21, m22, m23, m30, m31, m32, m33);
        }

        private static class Matrix4X4FInvertor
        {
            public static Matrix4f Invert( Matrix4f mat4X4)
            {
                var matArrays = MatrixCreate(4, 4);
                matArrays[0][0] = mat4X4.M00;
                matArrays[1][0] = mat4X4.M10;
                matArrays[2][0] = mat4X4.M20;
                matArrays[3][0] = mat4X4.M30;
                matArrays[0][1] = mat4X4.M01;
                matArrays[1][1] = mat4X4.M11;
                matArrays[2][1] = mat4X4.M21;
                matArrays[3][1] = mat4X4.M31;
                matArrays[0][2] = mat4X4.M02;
                matArrays[1][2] = mat4X4.M12;
                matArrays[2][2] = mat4X4.M22;
                matArrays[3][2] = mat4X4.M32;
                matArrays[0][3] = mat4X4.M03;
                matArrays[1][3] = mat4X4.M13;
                matArrays[2][3] = mat4X4.M23;
                matArrays[3][3] = mat4X4.M33;

                var matInvertedArrays = MatrixInverse(matArrays);

                return new Matrix4f((float)matInvertedArrays[0][0], (float)matInvertedArrays[0][1], (float)matInvertedArrays[0][2], (float)matInvertedArrays[0][3], (float)matInvertedArrays[1][0], (float)matInvertedArrays[1][1], (float)matInvertedArrays[1][2], (float)matInvertedArrays[1][3], (float)matInvertedArrays[2][0], (float)matInvertedArrays[2][1], (float)matInvertedArrays[2][2], (float)matInvertedArrays[2][3], (float)matInvertedArrays[3][0], (float)matInvertedArrays[3][1], (float)matInvertedArrays[3][2], (float)matInvertedArrays[3][3]);
            }

            private static double[][] MatrixCreate(int rows, int cols)
            {
                var result = new double[rows][];
                for (var i = 0; i < rows; ++i)
                {
                    result[i] = new double[cols];
                }

                return result;
            }

            private static double[][] MatrixInverse(double[][] matrix)
            {
                // assumes determinant is not 0
                // that is, the matrix does have an inverse
                var n = matrix.Length;
                var result = MatrixCreate(n, n); // make a copy of matrix
                for (var i = 0; i < n; ++i)
                {
                    for (var j = 0; j < n; ++j)
                    {
                        result[i][j] = matrix[i][j];
                    }
                }

                double[][] lum; // combined lower & upper
                int[] perm;
                MatrixDecompose(matrix, out lum, out perm);

                var b = new double[n];
                for (var i = 0; i < n; ++i)
                {
                    for (var j = 0; j < n; ++j)
                    {
                        if (i == perm[j])
                        {
                            b[j] = 1.0;
                        }
                        else
                        {
                            b[j] = 0.0;
                        }
                    }

                    var x = Helper(lum, b);
                    for (var j = 0; j < n; ++j)
                    {
                        result[j][i] = x[j];
                    }
                }

                return result;
            } // MatrixInverse

            private static int MatrixDecompose(double[][] m, out double[][] lum, out int[] perm)
            {
                // Crout's LU decomposition for matrix determinant and inverse
                // stores combined lower & upper in lum[][]
                // stores row permuations into perm[]
                // returns +1 or -1 according to even or odd number of row permutations
                // lower gets dummy 1.0s on diagonal (0.0s above)
                // upper gets lum values on diagonal (0.0s below)

                var toggle = +1; // even (+1) or odd (-1) row permutatuions
                var n = m.Length;

                // make a copy of m[][] into result lu[][]
                lum = MatrixCreate(n, n);
                for (var i = 0; i < n; ++i)
                {
                    for (var j = 0; j < n; ++j)
                    {
                        lum[i][j] = m[i][j];
                    }
                }

                // make perm[]
                perm = new int[n];
                for (var i = 0; i < n; ++i)
                {
                    perm[i] = i;
                }

                for (var j = 0; j < (n - 1); ++j) // process by column. note n-1
                {
                    var max = Math.Abs(lum[j][j]);
                    var piv = j;

                    for (var i = j + 1; i < n; ++i) // find pivot index
                    {
                        var xij = Math.Abs(lum[i][j]);
                        if (xij > max)
                        {
                            max = xij;
                            piv = i;
                        }
                    } // i

                    if (piv != j)
                    {
                        var tmp = lum[piv]; // swap rows j, piv
                        lum[piv] = lum[j];
                        lum[j] = tmp;

                        var t = perm[piv]; // swap perm elements
                        perm[piv] = perm[j];
                        perm[j] = t;

                        toggle = -toggle;
                    }

                    var xjj = lum[j][j];
                    if (xjj != 0.0)
                    {
                        for (var i = j + 1; i < n; ++i)
                        {
                            var xij = lum[i][j] / xjj;
                            lum[i][j] = xij;
                            for (var k = j + 1; k < n; ++k)
                            {
                                lum[i][k] -= xij * lum[j][k];
                            }
                        }
                    }
                } // j

                return toggle;
            }

            private static double[] Helper(double[][] luMatrix, double[] b) // helper
            {
                var n = luMatrix.Length;
                var x = new double[n];
                b.CopyTo(x, 0);

                for (var i = 1; i < n; ++i)
                {
                    var sum = x[i];
                    for (var j = 0; j < i; ++j)
                    {
                        sum -= luMatrix[i][j] * x[j];
                    }

                    x[i] = sum;
                }

                x[n - 1] /= luMatrix[n - 1][n - 1];
                for (var i = n - 2; i >= 0; --i)
                {
                    var sum = x[i];
                    for (var j = i + 1; j < n; ++j)
                    {
                        sum -= luMatrix[i][j] * x[j];
                    }

                    x[i] = sum / luMatrix[i][i];
                }

                return x;
            } // Helper
        }

        public Vec3f MultiplyPoint(Vec3f v)
        {
            var x = (float)((this.M00 * (double)v.X) + (this.M01 * (double)v.Y) + (this.M02 * (double)v.Z)) + this.M03;
            var y = (float)((this.M10 * (double)v.X) + (this.M11 * (double)v.Y) + (this.M12 * (double)v.Z)) + this.M13;
            var z = (float)((this.M20 * (double)v.X) + (this.M21 * (double)v.Y) + (this.M22 * (double)v.Z)) + this.M23;
            var num = 1f / ((float)((this.M30 * (double)v.X) + (this.M31 * (double)v.Y) + (this.M32 * (double)v.Z)) + this.M33);
            x *= num;
            y *= num;
            z *= num;
            return new Vec3f(x, y, z);
        }

        public Vec3f MultiplyVector(Vec3f v)
        {
            var x = (float)((this.M00 * (double)v.X) + (this.M01 * (double)v.Y) + (this.M02 * (double)v.Z));
            var y = (float)((this.M10 * (double)v.X) + (this.M11 * (double)v.Y) + (this.M12 * (double)v.Z));
            var z = (float)((this.M20 * (double)v.X) + (this.M21 * (double)v.Y) + (this.M22 * (double)v.Z));
            return new Vec3f(x, y, z);
        }

    }
}