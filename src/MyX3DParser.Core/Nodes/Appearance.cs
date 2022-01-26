
using System;
using System.Collections.Generic;
using System.Text;
using MyX3DParser.Generated.Model.AbstractNodes;
using MyX3DParser.Generated.Model.DataTypes;

namespace MyX3DParser.Generated.Model.Nodes
{
    public partial class Appearance
    {

        partial void Initialize()
        {
            var materialNode = material.SceneValue as Material;

            if (materialNode == null)
            {
                Material = new Shared.MeshMaterial();
                return;
            }
            var diffuse = new DataTypes.ColorRGBA(materialNode.diffuseColor.Value.R, materialNode.diffuseColor.Value.G, materialNode.diffuseColor.Value.B, 1 - materialNode.transparency.Value);
            Material = new Shared.MeshMaterial(materialNode.ambientIntensity.Value, diffuse, materialNode.emissiveColor.Value, materialNode.shininess.Value, materialNode.specularColor.Value);
        }
    }
}