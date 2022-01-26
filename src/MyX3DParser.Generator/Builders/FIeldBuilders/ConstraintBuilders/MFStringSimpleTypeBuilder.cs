using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MyX3DParser.Utils;

namespace MyX3DParser.Model.Builders
{
    [BuilderCategory("Fields")]
    internal class MFStringSimpleTypeBuilder : MFFieldBuilder
    {
        private readonly MFFieldBuilder mfStringBuilder;
        private readonly string simpleType;
        private readonly bool isBounded;
        private readonly IReadOnlyList<(string cleanName, IReadOnlyList<string> value)> definedValues;

        public MFStringSimpleTypeBuilder(MFFieldBuilder mfStringBuilder, string simpleType, bool isBounded, IEnumerable<string> definedValues)
            : base(simpleType, mfStringBuilder.X3DFieldName,mfStringBuilder.DataType)
        {
            this.mfStringBuilder = mfStringBuilder;
            this.simpleType = simpleType;
            this.isBounded = isBounded;
            this.definedValues = definedValues
                .Select(o =>
                {

                                 var parsedValues = BCLTypeBuilder.ParseMFStringValue(o);
                    
                                 return (parsedValues.Select(e1 => CleanEnumValue(e1)).StringJoin("_").PadLeft(1, '_'), parsedValues);
                })
                .ToList();
        }


        public override string ToString()
        {
            var builder = new BaseConstrainedFieldBuilder(this,
                "MFString",
                DataType.CleanArrayTypeName,
                $@"       
private static readonly {DataType.CleanName}[][] definedValues = new {DataType.CleanName}[][]{{ {string.Join(", ", definedValues.Select(o => $"new []{{{o.value.WrapInQuotes().StringJoin(", ")} }}"))} }};
",
                CleanName,
                isBounded ? "definedValues.Any(o => o.SequenceEqual(value))" : "true",
                "");

            return builder.ToString();
        }

                 public static string CleanEnumValue(string value)
                 {
                     if (value.IsNullOrEmpty())
                     {
                         return "Empty";
                     }
        
                     if (Regex.IsMatch(value, "^[^a-zA-Z_]"))
                     {
                         value = "_" + value;
                     }
        
                     value = Regex.Replace(value, "[^a-zA-Z_0-9]", "_");
                     return value;
                 }
    }
}
