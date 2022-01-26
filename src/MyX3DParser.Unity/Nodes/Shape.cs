
using System;
using System.Collections.Generic;
using System.Text;
using MyX3DParser.Generated.Model.AbstractNodes;
using MyX3DParser.Generated.Model.DataTypes;

namespace MyX3DParser.Generated.Model.Nodes
{
    public partial class Shape
    {

        partial void UpdatePositionInner(ref Shared.SceneNodeData position)
        {
            var resultMatrix = position.Matrix * Shared.SceneNodeData.MirrorZ;

            position = new Shared.SceneNodeData(resultMatrix, position.IsVisible);
        }
    }
}