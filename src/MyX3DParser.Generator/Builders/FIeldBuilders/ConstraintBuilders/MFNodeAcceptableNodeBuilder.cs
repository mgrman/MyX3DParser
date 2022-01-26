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
    internal class MFNodeAcceptableNodeBuilder : BaseNodeFieldBuilder
    {
        public MFNodeAcceptableNodeBuilder(IReadOnlyList<INodeTypeBuilder> nodeBuilders, BaseNodeFieldBuilder baseType)
        :base(nodeBuilders)
        {
            BaseType = baseType;
        }

        public IFieldBuilder BaseType { get; }


        public override string X3DFieldName { get; } = "MFNode";
        public override bool IsArray { get; } = true;

        public override string ToString()
        {
            var constraintName = DataTypes.Select(o => o.CleanName)
                .StringJoin("_");
            
            var builder = new BaseConstrainedFieldBuilder(this,
                BaseType.CleanName,
                "IReadOnlyList<X3DNode>",
                DataTypes.Where(o => !(o is X3DNodeBuilder))
                    .Select(d => $@"
        public IReadOnlyList<{d.Name}> SceneValue_{d.Name} 
        {{ 
            get 
            {{            
                return SceneValue.OfType<{d.Name}>().ToList();
            }}
        }}
")
                    .LineJoin() +$@"

        public static new bool IsValueAccepted(X3DNode value)
        {{
            return value.GetSceneNode() ==null || {DataTypes.Select(d => $"value.GetSceneNode() is {d.CleanName}").StringJoin(" || ")};
        }}",
                CleanName,
                $@"value.All(v=> v.GetSceneNode() ==null || {DataTypes.Select(d => $"v.GetSceneNode() is {d.CleanName}").StringJoin(" || ")})",
                "");


            return builder.ToString();
        }
    }
}
