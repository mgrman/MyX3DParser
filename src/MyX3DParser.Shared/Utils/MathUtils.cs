using System;
using System.Collections;
using System.Collections.Generic;
using MyX3DParser.Generated.Model.AbstractNodes;
using MyX3DParser.Generated.Model.Fields;
using MyX3DParser.Generated.Model.Nodes;

namespace MyX3DParser.Generated.Model
{
    public static class MathUtils
    {
        public static T InterpolateValue<T>(IReadOnlyList<float> keys, IReadOnlyList<T> values, Func<T,T,float,T> interpolate, float fraction)
        {
            if (keys.Count != values.Count)
            {
                throw new InvalidOperationException();
            }
            if (keys.Count == 0)
            {
                throw new InvalidOperationException();
            }
            if (keys.Count ==1)
            {
                return values[0];
            }

            var index = GetKeyIndex(keys,fraction);

            if (index < 0)
            {
                return values[0];
            }
            if (index >= keys.Count-1)
            {
                return values[values.Count-1];
            }

            var nextIndex = index + 1;

                var keyBefore = keys[index];
                var keyAfter = keys[nextIndex];

                var alpha = (fraction - keyBefore) / (keyAfter - keyBefore);

                var valueBefore = values[index];
                var valueAfter = values[nextIndex];


            return interpolate(valueBefore, valueAfter, alpha);

            }

            public static int GetKeyIndex(IReadOnlyList<float> keys, float fraction)
            {
                if (fraction <= keys[0])
                {
                    return -1;
                }
                if (fraction >= keys[keys.Count - 1])
                {
                    return keys.Count - 1;
                }

                for (int i = 0; i < keys.Count-1; i++)
                {
                    var keyBefore = keys[i];
                    var keyAfter = keys[i + 1];
                    if (fraction >= keyBefore && fraction <= keyAfter)
                    {
                        return i;
                    }
                }

                throw new InvalidOperationException();
            }
        }
}