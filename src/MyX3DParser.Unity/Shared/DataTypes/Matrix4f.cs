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
using UnityEngine;

namespace MyX3DParser.Generated.Model.DataTypes
{
    public static partial class Matrix4f
    {
        public static UnityEngine.Matrix4x4 Parse(string value)
        {
            return Parse(value.SplitBySpace());
        }
            public static UnityEngine.Matrix4x4 Parse(IEnumerable<string> value)
            {
                var enumerator = value.GetEnumerator();
            var matrix = new Matrix4x4();
            if (!enumerator.MoveNext())
            {
                return matrix;
            }
            matrix.m00 = enumerator.Current.ParseInvariantFloat();
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }
            matrix.m01 = enumerator.Current.ParseInvariantFloat();
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }
            matrix.m02 = enumerator.Current.ParseInvariantFloat();
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }
            matrix.m03 = enumerator.Current.ParseInvariantFloat();
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }
            matrix.m10 = enumerator.Current.ParseInvariantFloat();
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }
            matrix.m11 = enumerator.Current.ParseInvariantFloat();
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }
            matrix.m12 = enumerator.Current.ParseInvariantFloat();
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }
            matrix.m13 = enumerator.Current.ParseInvariantFloat();
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }
            matrix.m20 = enumerator.Current.ParseInvariantFloat();
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }
            matrix.m21 = enumerator.Current.ParseInvariantFloat();
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }
            matrix.m22 = enumerator.Current.ParseInvariantFloat();
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }
            matrix.m23 = enumerator.Current.ParseInvariantFloat();
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }
            matrix.m30 = enumerator.Current.ParseInvariantFloat();
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }
            matrix.m31 = enumerator.Current.ParseInvariantFloat();
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }
            matrix.m32 = enumerator.Current.ParseInvariantFloat();
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }
            matrix.m33 = enumerator.Current.ParseInvariantFloat();
            if (enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }
            return matrix;
        }

        public static string ToX3DString(UnityEngine.Matrix4x4 value)
        {
            var sb = new StringBuilder();
            sb.Append(value.m00.ToInvariantString());
            sb.Append(' ');
            sb.Append(value.m01.ToInvariantString());
            sb.Append(' ');
            sb.Append(value.m02.ToInvariantString());
            sb.Append(' ');
            sb.Append(value.m03.ToInvariantString());
            sb.Append(' ');
            sb.Append(value.m10.ToInvariantString());
            sb.Append(' ');
            sb.Append(value.m11.ToInvariantString());
            sb.Append(' ');
            sb.Append(value.m12.ToInvariantString());
            sb.Append(' ');
            sb.Append(value.m13.ToInvariantString());
            sb.Append(' ');
            sb.Append(value.m20.ToInvariantString());
            sb.Append(' ');
            sb.Append(value.m21.ToInvariantString());
            sb.Append(' ');
            sb.Append(value.m22.ToInvariantString());
            sb.Append(' ');
            sb.Append(value.m23.ToInvariantString());
            sb.Append(' ');
            sb.Append(value.m30.ToInvariantString());
            sb.Append(' ');
            sb.Append(value.m31.ToInvariantString());
            sb.Append(' ');
            sb.Append(value.m32.ToInvariantString());
            sb.Append(' ');
            sb.Append(value.m33.ToInvariantString());
            return sb.ToString();
        }
    }
}