
using System;
using System.Collections.Generic;
using System.Text;
using MyX3DParser.Generated.Model.AbstractNodes;
using MyX3DParser.Generated.Model.DataTypes;

namespace MyX3DParser.Generated.Model.Nodes
{
    public partial class TimeSensor
    {

        partial void Initialize()
        {
            ParentContext.TimeChanged += UpdateTime;

            this.enabled.OnValueChange += o =>
            {
                    UpdateTime(ParentContext.CurrentTime);
            };
        }

        private void UpdateTime(float now)
        {

            if (!enabled.Value)
            {
                isActive.Value = false;
                return;
            }

            if (now >= startTime.Value)
            {
                if (stopTime.Value >= startTime.Value && now >= stopTime.Value && !loop.Value)
                {
                    if (isActive.Value)
                    {
                        fraction_changed.Value = 1.0f;
                    }
                }
                else if(!isActive.Value)
                {
                    fraction_changed.Value = 0.0f;
                }
            }
            else
            {
                return;
            }

            //if (!enabled.Value || now < startTime.Value || (now > stopTime.Value && !loop.Value))
            //{
            //    isActive.Value = false;
            //    return;
            //}

            isActive.Value = now < stopTime.Value || loop.Value;
            UpdateFraction(now);

            // TODO missing cycleTime
        }


        private void UpdateFraction(float now)
        {
            if (!isActive.Value)
            {
                fraction_changed.Value = 1;
                return;
            }
            time.Value = now;

            var temp = (now - startTime.Value) / cycleInterval.Value;
            var f = temp - (float)Math.Truncate(temp);

            fraction_changed.Value = f;
        }
    }
}