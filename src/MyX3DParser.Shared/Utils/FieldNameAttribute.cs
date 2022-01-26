using System;

namespace MyX3DParser.Generated.Model.DataTypes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
    public class FieldNameAttribute : Attribute
    {
        public FieldNameAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}