
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyX3DParser.Generated.Model.AbstractNodes;
using MyX3DParser.Generated.Model.DataTypes;


namespace MyX3DParser.Shared
{
    public class Mesh
    {
        private readonly IReadOnlyList<int> indices;
        private readonly IReadOnlyList<Vec3f> vertices;
        private readonly IReadOnlyList<Vec3f>? normals;

        public Mesh(IReadOnlyList<int> indices, IReadOnlyList<Vec3f> vertices, IReadOnlyList<Vec3f>? normals)
        {
            this.indices = indices;
            this.vertices = vertices;
            this.normals = normals;
        }
        public Mesh(IReadOnlyList<int> indices, IReadOnlyList<Vec3f> vertices)
        {
            this.indices = indices;
            this.vertices = vertices;
            this.normals = null;
        }

        public Mesh()
        {
            this.indices = Array.Empty<int>();
            this.vertices = Array.Empty<Vec3f>();
            this.normals = Array.Empty<Vec3f>();
        }

        public IReadOnlyList<int> Indices => indices;

        public IReadOnlyList<Vec3f> Vertices => vertices;

        public IReadOnlyList<Vec3f>? Normals => normals;
    }
}