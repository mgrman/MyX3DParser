
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyX3DParser.Generated.Model.AbstractNodes;
using MyX3DParser.Generated.Model.DataTypes;

namespace MyX3DParser.Generated.Model.Nodes
{
    public partial class IntegerSequencer
    {

        partial void Initialize()
        {
            setFraction.OnValueChange += UpdateValue;
            UpdateValue(setFraction.Value);
        }

        private void UpdateValue(float fraction)
        {
            var index = MathUtils.GetKeyIndex(key.Value,  fraction);

            if (index < 0)
            {
                index = 0;
            }
            if (index > key.Value.Count - 1)
            {
                index = key.Value.Count - 1;
            }

            this.value_changed.Value = keyValue.Value[index];
        }
    }
}