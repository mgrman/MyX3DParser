using MyX3DParser.Model.Builders;
using MyX3DParser.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MyX3DParser.Model.Builders
{
    [BuilderCategory("Fields")]
    internal class SFFieldBuilder : IStringFieldBuilder
    {
        public string Name { get; }

        public string CleanName => Name.CleanFileName();
        public IDataTypeBuilder DataType { get; }

        string IFieldBuilder.BuilderDataType => DataType.CleanSingleTypeName;
        public string ParseMethod(string arg)
        {
            return DataType.ParseMethod(arg);
        }

        public string X3DFieldName { get; }

        bool IFieldBuilder.IsArray => false;

        public string GetValue(string textValue) => DataType.GetSingleValue(textValue);

        public SFFieldBuilder(string name, string x3DFieldType, IDataTypeBuilder dataType)
        {
            this.Name = name;
            this.X3DFieldName = x3DFieldType;
            this.DataType = dataType;
            
            // TODO add regex validation for parsing method
        }

        public override string ToString()
        {
            var builder = new BaseFieldBuilder(this,
                CleanName,
                DataType.CleanSingleTypeName,
                $@"
        public static {DataType.CleanSingleTypeName} Parse(string textValue)
        {{
            return {DataType.ParseMethod("textValue")};
        }}", 
                $@"$"" {{containerField}}=\""{{({DataType.ToX3DString("_value")})}}\"""""
                );

            return builder.ToString();
        }
    }
}
