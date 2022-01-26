
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyX3DParser.Generated.Model.AbstractNodes;
using MyX3DParser.Generated.Model.DataTypes;

namespace MyX3DParser.Generated.Model.Statements
{
    public partial class Scene
    {

        partial void ToX3DStringPrependNodes(HashSet<X3DNode> alreadySerializedNodes,StringBuilder sb, Action ensureEnded)
        {
            foreach (var protoDeclare in ParentContext.GetAllProtoDeclares())
            {
                ensureEnded();
                sb.Append(protoDeclare.ToX3DString(alreadySerializedNodes));
            }
        }

        partial void ToX3DStringAppendNodes(HashSet<X3DNode> alreadySerializedNodes, StringBuilder sb, Action ensureEnded)
        {
            foreach (var route in ParentContext.GetAllRoutes())
            {
                ensureEnded();
                sb.Append(route.ToX3DString(alreadySerializedNodes));
            }
        }

        partial void Initialize()
        {
            foreach (var child in children.Value.GetSceneNodes().OfType<X3DChildNode>())
            {
                child.OnParentPositionsChange(null, new[] { Shared.SceneNodeData.Root });
            }
        }
    }
}