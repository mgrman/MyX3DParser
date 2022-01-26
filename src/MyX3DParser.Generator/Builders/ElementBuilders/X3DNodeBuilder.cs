using System;

namespace MyX3DParser.Model.Builders
{
    internal class X3DNodeBuilder : AbstractNodeBuilder
    {
        public new const string Name = "X3DNode";

        public X3DNodeBuilder() : base(Name, Array.Empty<AbstractNodeBuilder>())
        {
        }

    }
}
