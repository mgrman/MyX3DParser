using Microsoft.VisualBasic.CompilerServices;
using MyX3DParser.Model.Builders;
using MyX3DParser.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MyX3DParser.Model.Builders
{
    [BuilderCategory("DataTypes")]
    internal class BCLTypeBuilder : IDataTypeBuilder
    {
        private Func<string, string> getSingleValue;

        private Func<string, string> getArrayValue;
        private Func<string, string> parseSingleValue;

        private Func<string, string> parseArrayValue;

        private Func<string, string> toX3DString;

        public BCLTypeBuilder(string name)
        {

            Name = name;
            switch (name)
            {
                //case "Time":
                //    CleanSingleTypeName = "DateTime";
                //    getSingleValue = o => $"new {typeof(DateTime).Name}({new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).AddSeconds(double.Parse(o.NullIfEmpty() ?? "0", System.Globalization.CultureInfo.InvariantCulture)).Ticks})";
                //    getArrayValue = o =>
                //    {
                //        var items = (o.NullIfEmpty() ?? string.Empty).Split(new char[] {' ', ','}, StringSplitOptions.RemoveEmptyEntries)
                //            .Select(getSingleValue);
                //        return $"new {typeof(DateTime).Name}[]{{{string.Join(",", items)}}}";
                //    };

                //    parseSingleValue = o => $"new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).AddSeconds(double.Parse({o},System.Globalization.CultureInfo.InvariantCulture))";
                //    parseArrayValue = o => @$"{o}.Split(' ').Where(o=>o.IsNotNullOrEmpty()).Select(temp=>new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).AddSeconds(double.Parse(temp,System.Globalization.CultureInfo.InvariantCulture))).ToList()";

                //    toX3DString = o => $"({o}-new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds.ToString(System.Globalization.CultureInfo.InvariantCulture)";
                //    break;
                case "Double":
                    CleanSingleTypeName = "double";
                    getSingleValue = o => double.TryParse(o, NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var res) ? res.ToString() + "d" : "0d";
                    getArrayValue = o => $"new {typeof(double).Name}[]{{{string.Join(",", (o.NullIfEmpty() ?? string.Empty).Split(new char[] {' ', ','}, StringSplitOptions.RemoveEmptyEntries).Select(getSingleValue))}}}";

                    parseSingleValue = o => $"{o}.IsNullOrEmpty()? 0.0: double.Parse({o})";
                    parseArrayValue = o => @$"{o}.Split(' ').Where(o=>o.IsNotNullOrEmpty()).Select(o=>double.Parse(o,System.Globalization.CultureInfo.InvariantCulture)).ToList()";

                    toX3DString = o => $"{o}.ToString(System.Globalization.CultureInfo.InvariantCulture)";
                    break;
                case "Time":
                case "Float":
                    CleanSingleTypeName = "float";
                    getSingleValue = o => float.TryParse(o, NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var res) ? res.ToString() + "f" : "0f";
                    getArrayValue = o => $"new {typeof(float).Name}[]{{{string.Join(",", (o.NullIfEmpty() ?? string.Empty).Split(new char[] {' ', ','}, StringSplitOptions.RemoveEmptyEntries).Select(getSingleValue))}}}";

                    parseSingleValue = o => $"{o}.IsNullOrEmpty()? 0.0f:float.Parse({o},System.Globalization.CultureInfo.InvariantCulture)";
                    parseArrayValue = o => @$"{o}.Split(' ').Where(o=>o.IsNotNullOrEmpty()).Select(o=>float.Parse(o,System.Globalization.CultureInfo.InvariantCulture)).ToList()";

                    toX3DString = o => $"{o}.ToString(System.Globalization.CultureInfo.InvariantCulture)";
                    break;
                case "Int32":
                    CleanSingleTypeName = "int";
                    getSingleValue = o => int.TryParse(o, NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var res) ? res.ToString() : "0";
                    getArrayValue = o => $"new {typeof(int).Name}[]{{{string.Join(",", (o.NullIfEmpty() ?? string.Empty).Split(new char[] {' ', ','}, StringSplitOptions.RemoveEmptyEntries).Select(getSingleValue))}}}";

                    parseSingleValue = o => $"{o}.IsNullOrEmpty()? 0:int.Parse({o})";
                    parseArrayValue = o => @$"{o}.Split(' ').Where(o=>o.IsNotNullOrEmpty()).Select(int.Parse).ToList()";

                    toX3DString = o => $"{o}.ToString(System.Globalization.CultureInfo.InvariantCulture)";
                    break;
                case "Bool":
                    CleanSingleTypeName = "bool";
                    getSingleValue = o => bool.TryParse(o, out var res) ? (res ? "true" : "false") : "false";
                    getArrayValue = o => $"new {typeof(bool).Name}[]{{{string.Join(",", (o.NullIfEmpty() ?? string.Empty).Split(new char[] {' ', ','}, StringSplitOptions.RemoveEmptyEntries).Select(getSingleValue))}}}";

                    parseSingleValue = o => $"{o}.IsNullOrEmpty()? false:bool.Parse({o})";
                    parseArrayValue = o => @$"{o}.Split(' ').Where(o=>o.IsNotNullOrEmpty()).Select(bool.Parse).ToList()";

                    toX3DString = o => $"{o} ? \"true\":\"false\"";
                    break;
                case "String":
                    CleanSingleTypeName = "string";
                    getSingleValue = o => o.IsNullOrEmpty() ? "string.Empty" : $"\"{o}\"";
                    getArrayValue = o =>
                    {
                        if (o.IsNullOrEmpty())
                        {
                            return "Array.Empty<string>()";
                        }

                        var values = ParseMFStringValue(o);
                        var items = string.Join(", ", values.Select(getSingleValue));
                        return $"new {typeof(string).Name}[]{{{items}}}";
                    };

                    parseSingleValue = o => o;
                    parseArrayValue = o => @$"({o}.StartsWith(""\"""") ? {o}.Split('\""').Select((o, i) => new {{ o, i }}).Where(o => o.i % 2 == 1).Select(o => o.o).ToArray() : {o}.Split(' '))";

                    toX3DString = o => $"{o}";
                    break;
                default:
                    throw new NotImplementedException();
            }

        }

        public string Name { get; }

        public string CleanSingleTypeName { get; }

        public override string ToString()
        {
            return null!;
        }

        public string GetSingleValue(string textValue)
        {
            return getSingleValue(textValue);
        }

        public string GetArrayValue(string textValue)
        {
            return getArrayValue(textValue);
        }

        public static IReadOnlyList<string> ParseMFStringValue(string value)
        {
            if (value.StartsWith("\""))
            {
                return value.Split('\"')
                    .Select((o, i) => new {o, i})
                    .Where(o => o.i % 2 == 1)
                    .Select(o => o.o)
                    .ToArray();
            }
            else
            {
                return value.Split(' ');
            }
        }

        public string ParseMethod(string paramName)
        {
            return parseSingleValue(paramName);
        }

        public string ParseArrayMethod(string paramName)
        {
            return parseArrayValue(paramName);
        }

        public string ToX3DString(string paramName) => toX3DString(paramName);
        public string ToX3DArrayString(string paramName) => $"{paramName}.Select(inner=>{ToX3DString("inner")}).StringJoin(\" \")";
    }
}