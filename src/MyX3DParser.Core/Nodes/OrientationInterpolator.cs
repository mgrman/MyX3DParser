
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyX3DParser.Generated.Model.AbstractNodes;
using MyX3DParser.Generated.Model.DataTypes;

namespace MyX3DParser.Generated.Model.Nodes
{
    public partial class OrientationInterpolator
    {

        partial void Initialize()
        {
            setFraction.OnValueChange += UpdateValue;
        }

        private void UpdateValue(float fraction)
        {
            var value = MathUtils.InterpolateValue(key.Value, keyValue.Value, Rotation.Interpolate, fraction);

            this.value_changed.Value = value;
        }
    }
}