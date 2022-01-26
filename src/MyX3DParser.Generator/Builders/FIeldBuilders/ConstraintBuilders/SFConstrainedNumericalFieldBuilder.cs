using MyX3DParser.Model.Builders;
using System.Text;

namespace MyX3DParser.Model.Builders
{
    [BuilderCategory("Fields")]
    internal class SFConstrainedNumericalFieldBuilder : SFFieldBuilder
    {
        private readonly SFFieldBuilder baseType;
        private readonly NumericalConstraintBuilder numericalConstraintBuilder;

        public SFConstrainedNumericalFieldBuilder(SFFieldBuilder baseType, NumericalConstraintBuilder numericalConstraintBuilder)
            : base($"{baseType.Name}_NumType_{numericalConstraintBuilder.Name}", baseType.X3DFieldName, baseType.DataType)
        {
            this.baseType = baseType;
            this.numericalConstraintBuilder = numericalConstraintBuilder;
        }

        public override string ToString()
        {
            var builder = new BaseConstrainedFieldBuilder(this, baseType.X3DFieldName, DataType.CleanSingleTypeName, $@"", CleanName,  numericalConstraintBuilder.ConditionSingle("value"), "");

            return builder.ToString();
        }
    }
}