
using System;
using System.Collections.Generic;
using System.Text;
using MyX3DParser.Generated.Model.AbstractNodes;
using MyX3DParser.Generated.Model.DataTypes;

namespace MyX3DParser.Generated.Model.Nodes
{
    public partial class Shape
    {

        partial void Initialize()
        {


            var meshNode = @geometry.SceneValue_X3DGeometryNode;
            if (meshNode == null)
            {
                Mesh = new Shared.Mesh();
                return;
            }
            Mesh = meshNode.Mesh ?? new Shared.Mesh();



            var appearanceNode = appearance.SceneValue_X3DAppearanceNode;

            if (appearanceNode != null)
            {
                Material = appearanceNode.Material;
            }
            else
            {
                Material = new Shared.MeshMaterial();
            }

            ParentContext.AddShape(this);
        }
    }
}