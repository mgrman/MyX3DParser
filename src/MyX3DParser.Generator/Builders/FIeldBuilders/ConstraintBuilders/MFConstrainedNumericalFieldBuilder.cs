namespace MyX3DParser.Model.Builders
{
    [BuilderCategory("Fields")]
    internal class MFConstrainedNumericalFieldBuilder : MFFieldBuilder
    {
        private readonly MFFieldBuilder baseType;
        private readonly NumericalConstraintBuilder numericalConstraintBuilder;

        public MFConstrainedNumericalFieldBuilder(MFFieldBuilder baseType, NumericalConstraintBuilder numericalConstraintBuilder)
            : base($"{baseType.Name}_NumType_{numericalConstraintBuilder.Name}", baseType.X3DFieldName, baseType.DataType)
        {
            this.baseType = baseType;
            this.numericalConstraintBuilder = numericalConstraintBuilder;
        }

        public override string ToString()
        {
            var builder = new BaseConstrainedFieldBuilder(this, baseType.X3DFieldName, DataType.CleanArrayTypeName, $@"", CleanName, numericalConstraintBuilder.ConditionArray("value"), "");

            return builder.ToString();
        }
    }
}