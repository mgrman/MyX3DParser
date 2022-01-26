
using System.Numerics;

namespace MyX3DParser.Generated.Model.Nodes
{
    public partial class PositionInterpolator
    {

        partial void Initialize()
        {
            setFraction.OnValueChange += UpdateValue;
        }

        private void UpdateValue(float fraction)
        {
            var value = MathUtils.InterpolateValue(key.Value, keyValue.Value, Vector3.Lerp, fraction);

            this.value_changed.Value = value;
        }
    }
}