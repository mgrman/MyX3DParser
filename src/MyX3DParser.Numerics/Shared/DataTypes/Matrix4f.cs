using System;
using System.Collections.Generic;
using System.Linq;
using MyX3DParser.Utils;
using MyX3DParser.Generated.Model.DataTypes;
using MyX3DParser.Generated.Model.AbstractNodes;
using MyX3DParser.Generated.Model.Nodes;
using MyX3DParser.Generated.Model.Statements;
using MyX3DParser.Generated.Model.Fields;
using MyX3DParser.Generated.Model.Parsing;
using System.Text;
using System.Numerics;

namespace MyX3DParser.Generated.Model.DataTypes
{
    public static partial class Matrix4f
    {
        public static Matrix4x4 Parse(string value)
        {
            return Parse(value.SplitBySpace());
        }
            public static Matrix4x4 Parse(IEnumerable<string> value)
            {
                var enumerator = value.GetEnumerator();
            var matrix = new Matrix4x4();
            if (!enumerator.MoveNext())
            {
                return matrix;
            }
            matrix.M11 = enumerator.Current.ParseInvariantFloat();
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }
            matrix.M21 = enumerator.Current.ParseInvariantFloat();
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }
            matrix.M31 = enumerator.Current.ParseInvariantFloat();
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }
            matrix.M41 = enumerator.Current.ParseInvariantFloat();
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }
            matrix.M12 = enumerator.Current.ParseInvariantFloat();
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }
            matrix.M22 = enumerator.Current.ParseInvariantFloat();
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }
            matrix.M32 = enumerator.Current.ParseInvariantFloat();
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }
            matrix.M42 = enumerator.Current.ParseInvariantFloat();
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }
            matrix.M13 = enumerator.Current.ParseInvariantFloat();
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }
            matrix.M23 = enumerator.Current.ParseInvariantFloat();
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }
            matrix.M33 = enumerator.Current.ParseInvariantFloat();
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }
            matrix.M43 = enumerator.Current.ParseInvariantFloat();
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }
            matrix.M14 = enumerator.Current.ParseInvariantFloat();
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }
            matrix.M24 = enumerator.Current.ParseInvariantFloat();
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }
            matrix.M34 = enumerator.Current.ParseInvariantFloat();
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }
            matrix.M44 = enumerator.Current.ParseInvariantFloat();
            if (enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }
            return matrix;
        }

        public static string ToX3DString(Matrix4x4 value)
        {
            var sb = new StringBuilder();
            sb.Append(value.M11.ToInvariantString());
            sb.Append(' ');
            sb.Append(value.M21.ToInvariantString());
            sb.Append(' ');
            sb.Append(value.M31.ToInvariantString());
            sb.Append(' ');
            sb.Append(value.M41.ToInvariantString());
            sb.Append(' ');
            sb.Append(value.M12.ToInvariantString());
            sb.Append(' ');
            sb.Append(value.M22.ToInvariantString());
            sb.Append(' ');
            sb.Append(value.M32.ToInvariantString());
            sb.Append(' ');
            sb.Append(value.M42.ToInvariantString());
            sb.Append(' ');
            sb.Append(value.M13.ToInvariantString());
            sb.Append(' ');
            sb.Append(value.M23.ToInvariantString());
            sb.Append(' ');
            sb.Append(value.M33.ToInvariantString());
            sb.Append(' ');
            sb.Append(value.M43.ToInvariantString());
            sb.Append(' ');
            sb.Append(value.M14.ToInvariantString());
            sb.Append(' ');
            sb.Append(value.M24.ToInvariantString());
            sb.Append(' ');
            sb.Append(value.M34.ToInvariantString());
            sb.Append(' ');
            sb.Append(value.M44.ToInvariantString());
            return sb.ToString();
        }
    }
}