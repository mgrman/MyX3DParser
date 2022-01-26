using Microsoft.VisualBasic.CompilerServices;
using MyX3DParser.Model.Builders;
using MyX3DParser.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyX3DParser.Model.Builders
{
    [BuilderCategory("Fields")]
    internal class SFFieldWithCustomConstraintBuilder : SFFieldBuilder
    {
        private readonly string condition;
        private readonly SFFieldBuilder baseType;

        public SFFieldWithCustomConstraintBuilder(string name, string condition, SFFieldBuilder baseType)
            :base(name, baseType.X3DFieldName, baseType.DataType)
        {
            this.condition = condition;
            this.baseType = baseType;
        }

        public override string ToString()
        {
            var builder = new BaseConstrainedFieldBuilder(this,
                baseType.X3DFieldName,
                DataType.CleanSingleTypeName,
                $@"",
                CleanName,
                condition,
                "");

            return builder.ToString();
        }


    }
}
