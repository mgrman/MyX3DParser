using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyX3DParser.Model.Builders
{
    internal interface IDataTypeBuilder:IFileBuilder
    {
        string ArrayName => $"IReadOnlyList<{Name}>";

        string ParseMethod(string paramName);
        string ParseArrayMethod(string paramName);

        string ToX3DString(string paramName);
        string ToX3DArrayString(string paramName) =>  $"{paramName}.Select(inner=>{ToX3DString("inner")}).StringJoin(\", \")";

        string CleanSingleTypeName { get; }
        string CleanArrayTypeName => $"IReadOnlyList<{CleanSingleTypeName}>";

        string GetSingleValue(string textValue);
        string GetArrayValue(string textValue);
        string GetValue(string textValue, bool isArray) => isArray ? GetArrayValue(textValue) : GetSingleValue(textValue);


        string CompareValueMethod(string paramName, string compareToName) => $"@{paramName} != {(compareToName)}";

        string CompareArrayMethod(string paramName, string compareToName) => $"!@{paramName}.SequenceEqual({(compareToName)})";


        string GreaterMethod(string paramLeft, string paramRight) => $"{paramLeft} > {paramRight}";
        string GreaterOrEqualMethod(string paramLeft, string paramRight) => $"{paramLeft} >= {paramRight}";
        string LesserMethod(string paramLeft, string paramRight) => $"{paramLeft} < {paramRight}";
        string LesserOrEqualMethod(string paramLeft, string paramRight) => $"{paramLeft} <= {paramRight}";
    }
}
