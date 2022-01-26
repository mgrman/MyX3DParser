using System.Collections.Generic;
using System.Linq;
using MyX3DParser.Utils;

namespace MyX3DParser.Model.Builders
{
    [BuilderCategory("Fields")]
    internal class MFNodeFieldBuilder : BaseNodeFieldBuilder
    {
        public override string X3DFieldName { get; } = "MFNode";
        public override bool IsArray => true;

        public MFNodeFieldBuilder(X3DNodeBuilder nodeBuilder)
            : base(new[] {nodeBuilder})
        {
        }

        public override string ToString()
        {
            var builder = new BaseFieldBuilder(this,
                CleanName,
                "IReadOnlyList<X3DNode>",
                $@"
        public IReadOnlyList<X3DNode> SceneValue 
        {{ 
            get 
            {{            
                return _value.Select(o=>o.GetSceneNode()).WhereNotNull().ToList();
            }}
        }}
        public static bool IsValueAccepted(X3DNode value)
        {{
            return true;
        }}
",
                "_value.Select(v=>v.ToX3DString(containerField,alreadySerializedNodes)).StringJoin(Environment.NewLine)");

            return builder.ToString();
        }
    }
}