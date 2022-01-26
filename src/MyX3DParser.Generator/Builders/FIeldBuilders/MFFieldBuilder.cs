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
    internal class MFFieldBuilder : IStringFieldBuilder
    {
        // TODO should use mutable list as inner collection. but then default value means copy

        public string Name { get; }

        string IFieldBuilder.BuilderDataType => DataType.CleanArrayTypeName;
        public string CleanName =>
            Path.GetInvalidFileNameChars()
                .Aggregate(Name, (n, c) => n.Replace(c, '_'));
        
        public IDataTypeBuilder DataType { get; }
        public string ParseMethod(string arg)
        {
            return DataType.ParseArrayMethod(arg);
        }

        bool IFieldBuilder.IsArray => true;


        public string GetValue(string textValue) => DataType.GetArrayValue(textValue);

        public string X3DFieldName {get;}

        public MFFieldBuilder(string name, string x3DFieldName, IDataTypeBuilder dataType)
        {
            this.Name = name;
            this.X3DFieldName = x3DFieldName;
            this.DataType = dataType;
        }

        public override string ToString()
        {
            var builder = new BaseFieldBuilder(this,
                CleanName,
                DataType.CleanArrayTypeName,
                @$"
        public static {DataType.CleanArrayTypeName} Parse(string textValue)
        {{
            return {DataType.ParseArrayMethod("textValue")};
        }}", 
                $@"$"" {{containerField}}=\""{{{DataType.ToX3DArrayString("_value")}}}\""""");

            return builder.ToString();
        }
    }
}
