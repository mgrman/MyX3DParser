
namespace MyX3DParser.Shared
{
    public class MeshMaterial
    {
        public MeshMaterial()
           :this(1, new UnityEngine.Color(1,0,1,1), new UnityEngine.Color(1, 0, 1), 0, new UnityEngine.Color(1, 0, 1))
        {

        }

        public MeshMaterial(float ambientIntensity, UnityEngine.Color diffuseColor, UnityEngine.Color emissiveColor, float shininess, UnityEngine.Color specularColor)
        {
            AmbientIntensity = ambientIntensity;
            this.diffuseColor = diffuseColor;
            this.emissiveColor = emissiveColor;
            this.shininess = shininess;
            this.specularColor = specularColor;
        }

        public float AmbientIntensity { get; }
        public UnityEngine.Color diffuseColor { get; }
        public UnityEngine.Color emissiveColor { get; }
        public float shininess { get; }
        public UnityEngine.Color specularColor { get; }

    }
}