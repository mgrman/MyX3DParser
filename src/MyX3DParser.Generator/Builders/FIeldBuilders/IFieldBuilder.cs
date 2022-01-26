using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyX3DParser.Model.Builders
{
    internal interface IFieldBuilder : IFileBuilder
    {
        string X3DFieldName { get; }
        bool IsArray { get; }

        string GetValue(string textValue);

        string BuilderDataType { get; }
    }

    internal interface INodeFieldBuilder : IFieldBuilder
    {
        string GetX3DNodeValue(string paramName);
        string GetValidateNodeMethod(string paramName);

        // string AcceptableNodeTypes { get; }
        // IReadOnlyList<string> AcceptableNodeTypesSplit => AcceptableNodeTypes.Split("|");

        IReadOnlyList<INodeTypeBuilder> DataTypes { get; }
    }

    internal interface IStringFieldBuilder : IFieldBuilder
    {

        IDataTypeBuilder DataType { get; }

        // IFieldConstraint? Constraint { get; }
        // string ToX3DString(string paramName);
        //
        // string ParseMethod(string arg);
        
        string CompareMethod(string paramName, string compareToName) => IsArray ? DataType.CompareArrayMethod(paramName, compareToName) : DataType.CompareValueMethod(paramName, compareToName);
    }
}