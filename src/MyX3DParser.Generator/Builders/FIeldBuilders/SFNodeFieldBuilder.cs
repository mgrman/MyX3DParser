using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using MyX3DParser.Utils;

namespace MyX3DParser.Model.Builders
{
    [BuilderCategory("Fields")]
    internal class SFNodeFieldBuilder : BaseNodeFieldBuilder
    {
        public override string X3DFieldName { get; } = "SFNode";
        public override bool IsArray => false;

        public SFNodeFieldBuilder(X3DNodeBuilder nodeBuilder)
            : base(new[] {nodeBuilder})
        {
        }

        public override string ToString()
        {
            var builder = new BaseFieldBuilder(this,
                CleanName,
                "X3DNode?",
                $@"

        public X3DNode? SceneValue 
        {{ 
            get 
            {{
                return _value.GetSceneNode();
            }}
        }}
        public static bool IsValueAccepted(X3DNode value)
        {{
            return true;
        }}",
                "_value?.ToX3DString(containerField,alreadySerializedNodes) ?? string.Empty");

            return builder.ToString();
        }
    }
}