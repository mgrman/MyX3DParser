using MyX3DParser.Generated.Model.AbstractNodes;
using MyX3DParser.Generated.Model.DataTypes;
using MyX3DParser.Utils;
using System.Collections.Generic;
using System.Linq;

namespace MyX3DParser.Generated.Model.Nodes
{
    public partial class Switch
    {

        partial void Initialize()
        {
            this.@addChildren.OnChange += OnUpdateTransformation;
            this.@children.OnChange += OnUpdateTransformation;
            this.@removeChildren.OnChange += OnUpdateTransformation;
            this.whichChoice.OnChange += OnUpdateTransformation;
        }


        private void OnUpdateTransformation(IX3DField field)
        {
            UpdateMatrices();
        }

        partial void UpdatePositionsForChild(X3DChildNode child, ref IReadOnlyList<Shared.SceneNodeData> positions)
        {
            var childIndex = children.Value.IndexOf(child);
            if (childIndex != whichChoice.Value)
            {
                positions = positions.Select(o => o.CombineWithChild(Shared.SceneNodeData.Deactivation)).ToList();
            }
        }
    }
}