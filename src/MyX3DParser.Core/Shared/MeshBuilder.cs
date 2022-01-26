using MyX3DParser.Generated.Model.DataTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyX3DParser.Shared
{
    class MeshBuilder
    {
        private readonly List<int> buffer;
        private readonly List<int> triangleIndices;
        private readonly List<Vec3f> coords;
        private readonly List<Vec3f> normals;

        public MeshBuilder()
        {
            buffer = new List<int>();

            triangleIndices = new List<int>();
            coords = new List<Vec3f>();

            normals = new List<Vec3f>();
        }

        public void AddTriangle((float x,float y ,float z) posA, (float x, float y, float z) posB, (float x, float y, float z) posC)
        {
            AddTriangle(new Vec3f(posA.x, posA.y, posA.z), new Vec3f(posB.x, posB.y, posB.z), new Vec3f(posC.x, posC.y, posC.z));
        }
        public void AddTriangle(Vec3f posA, Vec3f posB, Vec3f posC) {


            triangleIndices.Add(coords.Count);
            coords.Add(posA);

            triangleIndices.Add(coords.Count);
            coords.Add(posB);

            triangleIndices.Add(coords.Count);
            coords.Add(posC);

            var faceNormal = Vec3f.Cross(posB - posA, posC - posA);

            normals.Add(faceNormal);
            normals.Add(faceNormal);
            normals.Add(faceNormal);
        }


        public Mesh GetMeshAndDispose()
        {
            var result=new Shared.Mesh(triangleIndices, coords, normals);



            return result;  
        }

    }
}
