using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyX3DParser.Model.Builders
{
    [BuilderCategory("DataTypes")]
    class RegexDataTypeBuilder     
    {
        public RegexDataTypeBuilder(string name, string regex, BCLTypeBuilder stringBuilder)
        {
            if (!name.StartsWith("xs:"))
            {
                throw new InvalidOperationException();
            }
            Name = name;
            BaseType = stringBuilder;
            Regex = regex;
        }

        public string CleanSingleTypeName => "string";

        public string Name { get; }
        public string CleanName => Name.Substring(3);
        public BCLTypeBuilder BaseType { get; }
        public string Regex { get; }

        public override string ToString()
        {
            return null!;
        }

        public string GetArrayValue(string textValue) => BaseType.GetArrayValue(textValue);

        public string GetSingleValue(string textValue) => BaseType.GetSingleValue(textValue);

        public string ParseArrayMethod(string paramName)
        {
            return BaseType.ParseArrayMethod(paramName);
        }

        public string ToX3DString(string paramName) => BaseType.ToX3DString(paramName);

        public string ParseMethod(string paramName)
        {
            return BaseType.ParseMethod(paramName);
        }
    }
}
