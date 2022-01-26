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
    internal class SFStringSimpleTypeBuilder : SFFieldBuilder 
    {
        private readonly SFFieldBuilder sfStringBuilder;
        private readonly string simpleType;
        private readonly bool isBounded;
        private readonly IReadOnlyList<string> definedValues;

        public SFStringSimpleTypeBuilder(SFFieldBuilder sfStringBuilder, string simpleType, bool isBounded, IEnumerable<string> definedValues)
            : base(simpleType, sfStringBuilder.X3DFieldName, sfStringBuilder.DataType)
        {
            this.sfStringBuilder = sfStringBuilder;
            this.simpleType = simpleType;
            this.isBounded = isBounded;
            this.definedValues = definedValues.ToList();
        }

        public override string ToString()
        {
            var builder = new BaseConstrainedFieldBuilder(this,
                "SFString",
                DataType.CleanSingleTypeName,
                $@"
private static readonly IReadOnlyList<{DataType.CleanSingleTypeName}> definedValues = new []{{ {string.Join(", ", definedValues.WrapInQuotes())} }};
",
                CleanName,
                isBounded?"definedValues.Any(o => o == value)":"true", 
                ""
            );


            return builder.ToString();
        }
    }
}