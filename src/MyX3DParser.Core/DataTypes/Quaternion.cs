
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyX3DParser.Generated.Model.AbstractNodes;
using MyX3DParser.Generated.Model.DataTypes;


namespace MyX3DParser.Generated
{

    public struct Quaternion : IEquatable<Quaternion>
    {
        public readonly float X;
        public readonly float Y;
        public readonly float Z;
        public readonly float W;

        public Quaternion(float x, float y, float z, float w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }

        public const float Deg2Rad = 0.01745329f;


        public static readonly Quaternion Identity = new Quaternion(0, 0, 0, 1);

        public static Quaternion Euler(double yaw, double pitch, double roll) // yaw (Z), pitch (Y), roll (X)
        {
            yaw *= Deg2Rad;
            pitch *= Deg2Rad;
            roll *= Deg2Rad;
            // Abbreviations for the various angular functions
            double cy = Math.Cos(yaw * 0.5);
            double sy = Math.Sin(yaw * 0.5);
            double cp = Math.Cos(pitch * 0.5);
            double sp = Math.Sin(pitch * 0.5);
            double cr = Math.Cos(roll * 0.5);
            double sr = Math.Sin(roll * 0.5);

            var w = cr * cp * cy + sr * sp * sy;
            var x = sr * cp * cy - cr * sp * sy;
            var y = cr * sp * cy + sr * cp * sy;
            var z = cr * cp * sy - sr * sp * cy;

            return new Quaternion((float)x, (float)y, (float)z, (float)w);
        }


        public static Vec3f ToEulerAngles(Quaternion q)
        {
            // z (x-axis rotation)
            var sinr_cosp = 2 * (q.W * q.X + q.Y * q.Z);
            var cosr_cosp = 1 - 2 * (q.X * q.X + q.Y * q.Y);
            var roll = (float)Math.Atan2(sinr_cosp, cosr_cosp);

            // y (y-axis rotation)
            var sinp = 2 * (q.W * q.Y - q.Z * q.X);
            float pitch;
            if (Math.Abs(sinp) >= 1)
                pitch = (float)( Math.PI/2 * Math.Sign(sinp)); // use 90 degrees if out of range
            else
                pitch = (float)Math.Asin(sinp);

            // x (z-axis rotation)
            var siny_cosp = 2 * (q.W * q.Z + q.X * q.Y);
            var cosy_cosp = 1 - 2 * (q.Y * q.Y + q.Z * q.Z);
            var yaw = (float)Math.Atan2(siny_cosp, cosy_cosp);

            return new Vec3f(yaw/ Deg2Rad, pitch / Deg2Rad, roll / Deg2Rad);
        }

        public static Quaternion operator *(Quaternion left, Quaternion right)
        {
            float x = left.W * right.X + left.X * right.W + left.Y * right.Z - left.Z * right.Y;
            float y = left.W * right.Y + left.Y * right.W + left.Z * right.X - left.X * right.Z;
            float z = left.W * right.Z + left.Z * right.W + left.X * right.Y - left.Y * right.X;
            float w = left.W * right.W - left.X * right.X - left.Y * right.Y - left.Z * right.Z;
            Quaternion result = new Quaternion(x, y, z, w);
            return result;

        }

        public Rotation AngleAxis => ToAngleAxis();

        public Rotation ToAngleAxis()
        {
            var angle = 2 * Math.Acos(W);
            var x = this.X / Math.Sqrt(1 - this.W * this.W);
            var y = this.Y / Math.Sqrt(1 - this.W * this.W);
            var z = this.Z / Math.Sqrt(1 - this.W * this.W);

            if (Math.Abs(angle) < 0.00001)
            {
                return new Rotation(0, 0, 1, 0);
            }

            return new Rotation((float)x, (float)y, (float)z, (float)angle);
        }

        public static Quaternion Inverse(Quaternion value)
        {
            var x = -value.X;
            var y = -value.Y;
            var z = -value.Z;
            var w = value.W;

            var norm2 = x * x + y * y + z * z + w * w;
            x /= norm2;
            y /= norm2;
            z /= norm2;
            w /= norm2;
            return new Quaternion(x, y, z, w);
        }

        public static bool operator ==(Quaternion a, Quaternion b) => (a.X == b.X) && (a.Y == b.Y) && (a.Z == b.Z) && (a.W == b.W);

        public static bool operator !=(Quaternion a, Quaternion b) => (a.X != b.X) || (a.Y != b.Y) || (a.Z != b.Z) || (a.W != b.W);

        public bool Equals(Quaternion other) => this == other;

        public override bool Equals(object obj)
        {
            if (!(obj is Quaternion))
            {
                return false;
            }

            return this.Equals((Quaternion)obj);
        }

        public override int GetHashCode() => this.X.GetHashCode() ^ (this.Y.GetHashCode() << 2) ^ (this.Z.GetHashCode() >> 2) ^ (this.W.GetHashCode() >> 1);

        public override string ToString() => string.Format("({0} , {1}, {2}, {3})", this.X, this.Y, this.Z, this.W);

        public static Quaternion Slerp(Quaternion from, Quaternion to, double t)
        {
            double cosOmega;
            double scaleFrom, scaleTo;

            // Normalize inputs and stash their lengths
            double lengthFrom = from.Length();
            double lengthTo = to.Length();
            from= from.Scale(1 / lengthFrom);
            to= to.Scale(1 / lengthTo);

            // Calculate cos of omega.
            cosOmega = from.X * to.X + from.Y * to.Y + from.Z * to.Z + from.W * to.W;

            const bool useShortestPath = true;
            if (useShortestPath)
            {
                // If we are taking the shortest path we flip the signs to ensure that
                // cosOmega will be positive.
                if (cosOmega < 0.0)
                {
                    cosOmega = -cosOmega;
                    to = new Quaternion(-to.X, -to.Y, -to.Z, -to.W);
                }
            }
            else
            {
                // If we are not taking the UseShortestPath we clamp cosOmega to
                // -1 to stay in the domain of Math.Acos below.
                if (cosOmega < -1.0)
                {
                    cosOmega = -1.0;
                }
            }

            // Clamp cosOmega to [-1,1] to stay in the domain of Math.Acos below.
            // The logic above has either flipped the sign of cosOmega to ensure it
            // is positive or clamped to -1 aready.  We only need to worry about the
            // upper limit here.
            if (cosOmega > 1.0)
            {
                cosOmega = 1.0;
            }

            // The mainline algorithm doesn't work for extreme
            // cosine values.  For large cosine we have a better
            // fallback hence the asymmetric limits.
            const double maxCosine = 1.0 - 1e-6;
            const double minCosine = 1e-10 - 1.0;

            // Calculate scaling coefficients.
            if (cosOmega > maxCosine)
            {
                // Quaternions are too close - use linear interpolation.
                scaleFrom = 1.0 - t;
                scaleTo = t;
            }
            else if (cosOmega < minCosine)
            {
                // Quaternions are nearly opposite, so we will pretend to 
                // is exactly -from.
                // First assign arbitrary perpendicular to "to".
                to = new Quaternion(-from.Y, from.X, -from.W, from.Z);

                double theta = t * Math.PI;

                scaleFrom = Math.Cos(theta);
                scaleTo = Math.Sin(theta);
            }
            else
            {
                // Standard case - use SLERP interpolation.
                double omega = Math.Acos(cosOmega);
                double sinOmega = Math.Sqrt(1.0 - cosOmega * cosOmega);
                scaleFrom = Math.Sin((1.0 - t) * omega) / sinOmega;
                scaleTo = Math.Sin(t * omega) / sinOmega;
            }

            // We want the magnitude of the output quaternion to be
            // multiplicatively interpolated between the input
            // magnitudes, i.e. lengthOut = lengthFrom * (lengthTo/lengthFrom)^t
            //                            = lengthFrom ^ (1-t) * lengthTo ^ t

            double lengthOut = lengthFrom * Math.Pow(lengthTo / lengthFrom, t);
            scaleFrom *= lengthOut;
            scaleTo *= lengthOut;

            return new Quaternion((float)(scaleFrom * from.X + scaleTo * to.X),
                                  (float)(scaleFrom * from.Y + scaleTo * to.Y),
                                  (float)(scaleFrom * from.Z + scaleTo * to.Z),
                                  (float)(scaleFrom * from.W + scaleTo * to.W));
        }

        private double Length()
        {
            double norm2 = X * X + Y * Y + Z * Z + W * W;
            if (!(norm2 <= Double.MaxValue))
            {
                // Do this the slow way to avoid squaring large
                // numbers since the length of many quaternions is
                // representable even if the squared length isn't.  Of
                // course some lengths aren't representable because
                // the length can be up to twice as big as the largest
                // coefficient.

                double max = Math.Max(Math.Max(Math.Abs(X), Math.Abs(Y)),
                                      Math.Max(Math.Abs(Z), Math.Abs(W)));

                double x = X / max;
                double y = Y / max;
                double z = Z / max;
                double w = W / max;

                double smallLength = Math.Sqrt(x * x + y * y + z * z + w * w);
                // Return length of this smaller vector times the scale we applied originally.
                return smallLength * max;
            }
            return Math.Sqrt(norm2);
        }

        private Quaternion Scale(double scale)
        {
            return new Quaternion((float)(X * scale), (float)(Y * scale), (float)(Z * scale), (float)(W * scale));
        }
    }
}