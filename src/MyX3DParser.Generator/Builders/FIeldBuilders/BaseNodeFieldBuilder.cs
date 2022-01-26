using MyX3DParser.Model.Builders;
using MyX3DParser.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyX3DParser.Model.Builders
{
    [BuilderCategory("Fields")]
    internal abstract class BaseNodeFieldBuilder : INodeFieldBuilder
    {
        public string Name => (DataTypes.Count==1 && DataTypes[0].Name=="X3DNode")? $"{X3DFieldName}": $"{X3DFieldName}_{DataTypes.Select(o => o.CleanName).StringJoin("_")}";

        
        public string CleanName => Name;

        string IFieldBuilder.BuilderDataType => IsArray ? "List<X3DNode>" : "X3DNode?";
        public string GetX3DNodeValue(string paramName) => paramName;

        public string GetValidateNodeMethod(string paramName)
        {
            return "(" + DataTypes.Select(o => $"{paramName} is {o.Name}")
                .StringJoin(" || ") + ")";
        }

        public string AcceptableNodeTypes =>
            DataTypes.Select(o => o.Name)
                .StringJoin("|");

        public IReadOnlyList<INodeTypeBuilder> DataTypes { get; }

        public abstract string X3DFieldName { get; }

        public abstract bool IsArray { get; }

        public string GetValue(string textValue)
        {
            if (IsArray)
            {
                if (string.IsNullOrEmpty(textValue))
                {
                    return "new List<X3DNode>()";
                }

                throw new InvalidOperationException();
            }
            else
            {
                if (textValue == "NULL" || string.IsNullOrEmpty(textValue))
                {
                    return "null";
                }

                throw new InvalidOperationException();
            }
        }

        public BaseNodeFieldBuilder(IReadOnlyList<INodeTypeBuilder> dataTypes)
        {
            this.DataTypes = dataTypes;
        }
    }
}