
using System;
using System.Collections.Generic;


namespace MyX3DParser.Generated
{
    /// <summary>
    /// https://github.com/x3dom/x3dom/blob/68dd42ab8b9643a7ff1f310c852cb6534d6336e9/src/fields.js
    /// </summary>
    public static partial class Decomposer
    {
        private const float Eps = 0.0001f;

        private static readonly IReadOnlyList<(float x, float y,float z)> Scales = new []
        {
            (-1f, 1f, 1f),
            (1f, -1f, 1f),
            (1f, 1f, -1f)
        };

        /// <summary>
        /// Computes the decomposition of the given 4x4 affine matrix M as
        /// M = T F R SO S SO^t, where T is a translation matrix,
        /// F is +/- I (a reflection), R is a rotation matrix,
        /// SO is a rotation matrix and S is a (nonuniform) scale matrix.
        /// </summary>
        public static ((float x, float y, float z, float w) rotation, (float x, float y, float z) scale, (float x, float y, float z, float w) scaleOrientation) Decompose(this float[,] matrixWithoutTranslation2)
        {

            var (det, _, _) = matrixWithoutTranslation2.PolarDecompose();

            if (det > 0.0f)
            {
                var (_, rotationMatrix, orientedScaleMatrix) = matrixWithoutTranslation2.PolarDecompose();
                var rotation = GetRotation(rotationMatrix);
                var (scaleOrientationMatrix, scale) = orientedScaleMatrix.SpectralDecompose();
                var scaleOrientation = GetRotation(scaleOrientationMatrix);
                return (rotation, scale, scaleOrientation);
            }
            else
            {
                ((float x, float y, float z, float w) rotation, (float x, float y, float z) scale, (float x, float y, float z, float w) scaleOrientation)? res=null;
                foreach (var mirrorScale in Scales)
                {

                    var mirrorMat= new float[,] {
                        {mirrorScale.x,0,0},
                        {0,mirrorScale.y,0},
                        {0,0,mirrorScale.z}
                    };

                    var matrixWithoutTranslation = MatrixMultiply(matrixWithoutTranslation2, mirrorMat);
                   var (det2, rotationMatrix, orientedScaleMatrix) = matrixWithoutTranslation.PolarDecompose();

                    if (det2 < 0.0f)
                    {
                        continue;
                    }


                    var rotation = GetRotation(rotationMatrix);
                    var (scaleOrientationMatrix, scale) = orientedScaleMatrix.SpectralDecompose();
                    var scaleOrientation = GetRotation(scaleOrientationMatrix);

                        scale = (mirrorScale.x * scale.x, mirrorScale.y* scale.y, mirrorScale.z* scale.z);

                        res = (rotation, scale, scaleOrientation);
                    return res.Value;
                }

                throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// Performs a polar decomposition of this matrix A into two matrices
        /// Q and S, so that A = QS.
        /// </summary>
        private static (float determinant, float[,] rotationMatrix, float[,] orientedScaleMatrix) PolarDecompose(this float[,] @this)
        {
            var TOL = 0.000000000001;

            var Mk = @this.GetTransposeCopy();
            var Ek = new float[,]
            {
                {1,0,0 },
                {0,1,0 },
                {0,0,1 }
            };

            var Mk_one = Mk.norm1_3x3();
            var Mk_inf = Mk.normInf_3x3();

            float Ek_one;
            float Mk_det;

            int counter = 1000;
            do
            {
                // compute transpose of adjoint
                var MkAdjT = Mk.adjointT_3x3();

                // Mk_det = det(Mk) -- computed from the adjoint
                Mk_det = Mk[0, 0] * MkAdjT[0, 0] + Mk[0, 1] * MkAdjT[0, 1] + Mk[0, 2] * MkAdjT[0, 2];

                // TODO: should this be a close to zero test ?
                if (Mk_det == 0.0)
                {
                    // x3dom.debug.logWarning( "polarDecompose: Mk_det == 0.0" );
                    break;
                }

                var MkAdjT_one = MkAdjT.norm1_3x3();
                var MkAdjT_inf = MkAdjT.normInf_3x3();

                // compute update factors
                var gamma = (float)Math.Sqrt(Math.Sqrt((MkAdjT_one * MkAdjT_inf) / (Mk_one * Mk_inf)) / Math.Abs(Mk_det));

                var g1 = 0.5f * gamma;
                var g2 = 0.5f / (gamma * Mk_det);

                Ek = Mk;

                Mk = Mk.multiply(g1); // this does:
                Mk = Mk.addScaled(MkAdjT, g2); // Mk = g1 * Mk + g2 * MkAdjT
                Ek = Ek.addScaled(Mk, -1.0f); // Ek -= Mk;

                Ek_one = Ek.norm1_3x3();
                Mk_one = Mk.norm1_3x3();
                Mk_inf = Mk.normInf_3x3();
                counter--;
            } while (counter >= 0 && Ek_one > (Mk_one * TOL));

            var rotationMatrix = Mk.GetTransposeCopy();
            var orientedScaleMatrix = MatrixMultiply(Mk, @this);

            for (var i = 0; i < 3; ++i)
            {
                for (var j = i; j < 3; ++j)
                {
                    orientedScaleMatrix[j, i] = 0.5f * (orientedScaleMatrix[j, i] + orientedScaleMatrix[i, j]);
                    orientedScaleMatrix[i, j] = 0.5f * (orientedScaleMatrix[j, i] + orientedScaleMatrix[i, j]);
                }
            }

            return (Mk_det, rotationMatrix, orientedScaleMatrix);
        }

        /// <summary>
        /// Performs a spectral decomposition of this matrix.
        /// </summary>
        private static (float[,] scaleOrientationMatrix, (float x, float y, float z) scale) SpectralDecompose(this float[,] @this)
        {
            float[,] scaleOrientationMatrix = new float[,]
            {
                {1,0,0 },
                {0,1,0 },
                {0,0,1 }
            };

            var next = new int[] { 1, 2, 0 };
            var maxIterations = 20;
            var diag = new[] { @this[0, 0], @this[1, 1], @this[2, 2] };
            var offDiag = new[] { @this[1, 2], @this[2, 0], @this[0, 1] };

            for (var iter = 0; iter < maxIterations; ++iter)
            {
                var sm = Math.Abs(offDiag[0]) + Math.Abs(offDiag[1]) + Math.Abs(offDiag[2]);

                if (sm == 0)
                {
                    break;
                }

                for (var i = 2; i >= 0; --i)
                {
                    var p = next[i];
                    var q = next[p];

                    var absOffDiag = Math.Abs(offDiag[i]);
                    var g = 100.0 * absOffDiag;

                    if (absOffDiag > 0.0)
                    {
                        var t = 0f;
                        var h = diag[q] - diag[p];
                        var absh = Math.Abs(h);

                        if (absh + g == absh)
                        {
                            t = offDiag[i] / h;
                        }
                        else
                        {
                            var theta = 0.5 * h / offDiag[i];
                            t = 1.0f / ((float)Math.Abs(theta) + (float)Math.Sqrt(theta * theta + 1.0));

                            t = theta < 0.0 ? -t : t;
                        }

                        var c = 1.0f / (float)Math.Sqrt(t * t + 1.0);
                        var s = t * c;

                        var tau = s / (c + 1.0f);
                        var ta = t * offDiag[i];

                        offDiag[i] = 0.0f;

                        diag[p] -= ta;
                        diag[q] += ta;

                        var offDiagq = offDiag[q];

                        offDiag[q] -= s * (offDiag[p] + tau * offDiagq);
                        offDiag[p] += s * (offDiagq - tau * offDiag[p]);

                        for (var j = 2; j >= 0; --j)
                        {
                            var a = scaleOrientationMatrix[j, p];
                            var b = scaleOrientationMatrix[j, q];

                            scaleOrientationMatrix[j, p] = scaleOrientationMatrix[j, p] - s * (b + tau * a);

                            scaleOrientationMatrix[j, q] = scaleOrientationMatrix[j, q] + s * (a - tau * b);
                        }
                    }
                }
            }

            var scale = (diag[0], diag[1], diag[2]);
            return (scaleOrientationMatrix, scale);
        }

        private static (float x, float y, float z, float w) GetRotation(float[,] matrix)
        {
            float s;
            var qt = new float[] { 0, 0, 0 };

            var i = 0;
            var j = 0;
            var k = 0;
            var nxt = new int[] { 1, 2, 0 };

            var tr = matrix[0, 0] + matrix[1, 1] + matrix[2, 2];

            float x;
            float y;
            float z;
            float w;

            if (tr > 0.0)
            {
                s = (float)Math.Sqrt(tr + 1.0);

                w = s * 0.5f;

                s = 0.5f / s;

                x = (matrix[2, 1] - matrix[1, 2]) * s;
                y = (matrix[0, 2] - matrix[2, 0]) * s;
                z = (matrix[1, 0] - matrix[0, 1]) * s;
            }
            else
            {
                if (matrix[1, 1] > matrix[0, 0])
                {
                    i = 1;
                }
                else
                {
                    i = 0;
                }

                if (matrix[2, 2] > matrix[i, i])
                {
                    i = 2;
                }

                j = nxt[i];
                k = nxt[j];

                s = (float)Math.Sqrt(matrix[i, i] - (matrix[j, j] + matrix[k, k]) + 1.0);

                qt[i] = s * 0.5f;
                s = 0.5f / s;

                w = (matrix[k, j] - matrix[j, k]) * s;

                qt[j] = (matrix[j, i] + matrix[i, j]) * s;
                qt[k] = (matrix[k, i] + matrix[i, k]) * s;

                x = qt[0];
                y = qt[1];
                z = qt[2];
            }

            if (w > 1.0 || w < -1.0)
            {
                var errThreshold = 1 + (Eps * 100);

                if (w > errThreshold || w < -errThreshold)
                {
                    // When copying, then everything, incl. the famous OpenSG MatToQuat bug
                    // x3dom.debug.logInfo( "MatToQuat: BUG: |quat[4]| (" + this.w + ") >> 1.0 !" );
                }

                if (w > 1.0)
                {
                    w = 1.0f;
                }
                else
                {
                    w = -1.0f;
                }
            }

            return (x, y, z, w);
        }

        private static float[,] multiply(this float[,] mat, float val)
        {
            var arr = new float[3, 3];

            arr[0, 0] = mat[0, 0] * val;
            arr[0, 1] = mat[0, 1] * val;
            arr[0, 2] = mat[0, 2] * val;
            arr[1, 0] = mat[1, 0] * val;
            arr[1, 1] = mat[1, 1] * val;
            arr[1, 2] = mat[1, 2] * val;
            arr[2, 0] = mat[2, 0] * val;
            arr[2, 1] = mat[2, 1] * val;
            arr[2, 2] = mat[2, 2] * val;
            return arr;
        }

        private static float[,] adjointT_3x3(this float[,] @this)
        {
            var arr = new float[3, 3];

            arr[0, 0] = @this[1, 1] * @this[2, 2] - @this[1, 2] * @this[2, 1];
            arr[0, 1] = @this[1, 2] * @this[2, 0] - @this[1, 0] * @this[2, 2];
            arr[0, 2] = @this[1, 0] * @this[2, 1] - @this[1, 1] * @this[2, 0];
            arr[1, 0] = @this[2, 1] * @this[0, 2] - @this[2, 2] * @this[0, 1];
            arr[1, 1] = @this[2, 2] * @this[0, 0] - @this[2, 0] * @this[0, 2];
            arr[1, 2] = @this[2, 0] * @this[0, 1] - @this[2, 1] * @this[0, 0];
            arr[2, 0] = @this[0, 1] * @this[1, 2] - @this[0, 2] * @this[1, 1];
            arr[2, 1] = @this[0, 2] * @this[1, 0] - @this[0, 0] * @this[1, 2];
            arr[2, 2] = @this[0, 0] * @this[1, 1] - @this[0, 1] * @this[1, 0];
            return arr;
        }

        private static float normInf_3x3(this float[,] @this)
        {
            var max = Math.Abs(@this[0, 0]) + Math.Abs(@this[0, 1]) + Math.Abs(@this[0, 2]);
            var t = 0f;

            if ((t = Math.Abs(@this[1, 0]) + Math.Abs(@this[1, 1]) + Math.Abs(@this[1, 2])) > max)
            {
                max = t;
            }

            if ((t = Math.Abs(@this[2, 0]) + Math.Abs(@this[2, 1]) + Math.Abs(@this[2, 2])) > max)
            {
                max = t;
            }

            return max;
        }

        private static float norm1_3x3(this float[,] @this)
        {
            var max = Math.Abs(@this[0, 0]) + Math.Abs(@this[1, 0]) + Math.Abs(@this[2, 0]);
            var t = 0f;

            if ((t = Math.Abs(@this[0, 1]) + Math.Abs(@this[1, 1]) + Math.Abs(@this[2, 1])) > max)
            {
                max = t;
            }

            if ((t = Math.Abs(@this[0, 2]) + Math.Abs(@this[1, 2]) + Math.Abs(@this[2, 2])) > max)
            {
                max = t;
            }

            return max;
        }

        private static float[,] addScaled(this float[,] @this, float[,] that, float val)
        {
            var arr = new float[3, 3];

            arr[0, 0] = @this[0, 0] + that[0, 0] * val;
            arr[0, 1] = @this[0, 1] + that[0, 1] * val;
            arr[0, 2] = @this[0, 2] + that[0, 2] * val;
            arr[1, 0] = @this[1, 0] + that[1, 0] * val;
            arr[1, 1] = @this[1, 1] + that[1, 1] * val;
            arr[1, 2] = @this[1, 2] + that[1, 2] * val;
            arr[2, 0] = @this[2, 0] + that[2, 0] * val;
            arr[2, 1] = @this[2, 1] + that[2, 1] * val;
            arr[2, 2] = @this[2, 2] + that[2, 2] * val;
            return arr;
        }

        private static float[,] negate(this float[,] mat)
        {
            var arr = new float[3, 3];

            arr[0, 0] = -mat[0, 0];
            arr[0, 1] = -mat[0, 1];
            arr[0, 2] = -mat[0, 2];
            arr[1, 0] = -mat[1, 0];
            arr[1, 1] = -mat[1, 1];
            arr[1, 2] = -mat[1, 2];
            arr[2, 0] = -mat[2, 0];
            arr[2, 1] = -mat[2, 1];
            arr[2, 2] = -mat[2, 2];
            return arr;
        }


        private static float[,] GetTransposeCopy(this float[,] mat)
        {
            var arr = new float[3, 3];
            arr[0, 0] = mat[0, 0];
            arr[0, 1] = mat[1, 0];
            arr[0, 2] = mat[2, 0];
            arr[1, 0] = mat[0, 1];
            arr[1, 1] = mat[1, 1];
            arr[1, 2] = mat[2, 1];
            arr[2, 0] = mat[0, 2];
            arr[2, 1] = mat[1, 2];
            arr[2, 2] = mat[2, 2];
            return arr;
        }

        private static float[,] MatrixMultiply(float[,] lhs, float[,] rhs)
        {
            var arr = new float[3, 3];
            arr[0, 0] = (lhs[0, 0] * rhs[0, 0]) + (lhs[0, 1] * rhs[1, 0]) + (lhs[0, 2] * rhs[2, 0]);
            arr[0, 1] = (lhs[0, 0] * rhs[0, 1]) + (lhs[0, 1] * rhs[1, 1]) + (lhs[0, 2] * rhs[2, 1]);
            arr[0, 2] = (lhs[0, 0] * rhs[0, 2]) + (lhs[0, 1] * rhs[1, 2]) + (lhs[0, 2] * rhs[2, 2]);
            arr[1, 0] = (lhs[1, 0] * rhs[0, 0]) + (lhs[1, 1] * rhs[1, 0]) + (lhs[1, 2] * rhs[2, 0]);
            arr[1, 1] = (lhs[1, 0] * rhs[0, 1]) + (lhs[1, 1] * rhs[1, 1]) + (lhs[1, 2] * rhs[2, 1]);
            arr[1, 2] = (lhs[1, 0] * rhs[0, 2]) + (lhs[1, 1] * rhs[1, 2]) + (lhs[1, 2] * rhs[2, 2]);
            arr[2, 0] = (lhs[2, 0] * rhs[0, 0]) + (lhs[2, 1] * rhs[1, 0]) + (lhs[2, 2] * rhs[2, 0]);
            arr[2, 1] = (lhs[2, 0] * rhs[0, 1]) + (lhs[2, 1] * rhs[1, 1]) + (lhs[2, 2] * rhs[2, 1]);
            arr[2, 2] = (lhs[2, 0] * rhs[0, 2]) + (lhs[2, 1] * rhs[1, 2]) + (lhs[2, 2] * rhs[2, 2]);

            return arr;
        }

    }
}
