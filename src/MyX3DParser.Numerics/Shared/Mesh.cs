
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using MyX3DParser.Generated.Model.AbstractNodes;

namespace MyX3DParser.Shared
{
    public class Mesh
    {
        private readonly IReadOnlyList<int> indices;
        private readonly IReadOnlyList<Vector3> vertices;
        private readonly IReadOnlyList<Vector3>? normals;

        public Mesh(IReadOnlyList<int> indices, IReadOnlyList<Vector3> vertices, IReadOnlyList<Vector3>? normals)
        {
            this.indices = indices;
            this.vertices = vertices;
            this.normals = normals;
        }


        public Mesh()
        {
            indices = Array.Empty<int>();
            vertices = Array.Empty<Vector3>();
            normals = Array.Empty<Vector3>();
        }

        public IReadOnlyList<int> Indices => indices;

        public IReadOnlyList<Vector3> Vertices => vertices;

        public IReadOnlyList<Vector3>? Normals => normals;
    }
}