namespace MyX3DParser.Generated.Model.DataTypes
{
    public class MeshMaterial
    {
        public MeshMaterial(float ambientIntensity, Color diffuseColor, Color emissiveColor, float shininess, Color specularColor, float transparency)
        {
            AmbientIntensity = ambientIntensity;
            this.diffuseColor = diffuseColor;
            this.emissiveColor = emissiveColor;
            this.shininess = shininess;
            this.specularColor = specularColor;
            this.transparency = transparency;
        }

        public float AmbientIntensity { get; }
        public Color diffuseColor { get; }
            public Color emissiveColor { get; }
            public float shininess { get; }
        public Color specularColor { get; }
        public float transparency { get; }

    }
}