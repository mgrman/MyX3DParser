using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyX3DParser.Model.Builders
{
    [BuilderCategory("Fields")]
    internal class SFStringRegexBuilder : SFFieldBuilder
    {
        private SFFieldBuilder sfStringBuilder;
        public RegexDataTypeBuilder RegexType { get; }
        
        public SFStringRegexBuilder(SFFieldBuilder sfStringBuilder, RegexDataTypeBuilder regexType)
            : base(regexType.Name, sfStringBuilder.X3DFieldName, sfStringBuilder.DataType)
        {
            this.sfStringBuilder = sfStringBuilder;
            this.RegexType = regexType;
        }

        public override string ToString()
        {
            var builder = new BaseConstrainedFieldBuilder(this,
                "SFString",
                DataType.CleanSingleTypeName,
                $@"",
                CleanName,
                @$"StaticConfig.IsRegexCheckingLenient?true:Regex.IsMatch(value, @""{RegexType.Regex}"")",
                "using System.Text.RegularExpressions;");

            return builder.ToString();
        }
    }
}