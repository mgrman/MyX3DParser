using MyX3DParser.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyX3DParser.Model.Builders
{
    [BuilderCategory("Fields")]
    internal class SFNodeAcceptableNodeBuilder : BaseNodeFieldBuilder
    {
        public SFNodeAcceptableNodeBuilder(IReadOnlyList<INodeTypeBuilder> nodeBuilders, BaseNodeFieldBuilder baseType)
            : base(nodeBuilders)
        {
            this.baseType = baseType;
        }
        
        public override string X3DFieldName { get; } = "SFNode";
        public override bool IsArray { get; } = false;

        private BaseNodeFieldBuilder baseType;

        public override string ToString()
        {
            var constraint = $@"value.GetSceneNode() ==null || {DataTypes.Select(d => $"value.GetSceneNode() is {d.CleanName}").StringJoin(" || ")}";
            var builder = new BaseConstrainedFieldBuilder(this,
                "SFNode",
                "X3DNode?",
                DataTypes.Where(o => !(o is X3DNodeBuilder))
                    .Select(d => $@"
        public {d.Name}? SceneValue_{d.Name}
        {{ 
            get 
            {{            
                return SceneValue as {d.Name};
            }}
        }}

")
                    .LineJoin()+@$"
        public static new bool IsValueAccepted(X3DNode value)
        {{
            return {constraint};
        }}",
                CleanName,
                constraint,
                "");

            return builder.ToString();
        }
    }
}