using MyX3DParser.Model.Builders;
using MyX3DParser.Utils;
using System;
using System.Linq;
using System.Text;

namespace MyX3DParser.Model.Builders
{
    [BuilderCategory("Statements")]
    internal class StatementBuilder : ElementBuilder
    {
        public StatementBuilder(string name)
            : base(name, Array.Empty<AbstractNodeBuilder>(),null)
        {
        }
    }
}