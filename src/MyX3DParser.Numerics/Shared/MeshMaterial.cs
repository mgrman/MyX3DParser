
using System.Drawing;

namespace MyX3DParser.Shared
{
    public class MeshMaterial
    {
        public MeshMaterial()
           :this(1, new Generated.Model.DataTypes.ColorRGBA(1,0,1,1), new Generated.Model.DataTypes.Color(1, 0, 1), 0, new Generated.Model.DataTypes.Color(1, 0, 1))
        {

        }

        public MeshMaterial(float ambientIntensity, Generated.Model.DataTypes.ColorRGBA diffuseColor, Generated.Model.DataTypes.Color emissiveColor, float shininess, Generated.Model.DataTypes.Color specularColor)
        {
            AmbientIntensity = ambientIntensity;
            this.diffuseColor = diffuseColor;
            this.emissiveColor = emissiveColor;
            this.shininess = shininess;
            this.specularColor = specularColor;
        }

        public float AmbientIntensity { get; }
        public Generated.Model.DataTypes.ColorRGBA diffuseColor { get; }
        public Generated.Model.DataTypes.Color  emissiveColor { get; }
        public float shininess { get; }
        public Generated.Model.DataTypes.Color specularColor { get; }

    }
}