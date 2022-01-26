using System.Collections.Generic;

namespace MyX3DParser.Model.Builders
{
    [BuilderCategory("Nodes")]
    internal class NodeBuilder : ElementBuilder
    {
        public NodeBuilder(string name, IReadOnlyList<AbstractNodeBuilder> interfaces, string? defaultContainerField)
            : base(name, interfaces, defaultContainerField)
        {
        }
    }
}