using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MyX3DParser.Shared
{
    class MeshBuilder
    {
        private readonly List<int> buffer;
        private readonly List<int> triangleIndices;
        private readonly List<Vector3> coords;
        private readonly List<Vector3> normals;

        public MeshBuilder()
        {
            buffer = new List<int>();

            triangleIndices = new List<int>();
            coords = new List<Vector3>();

            normals = new List<Vector3>();
        }

        public void AddTriangle((float x,float y ,float z) posA, (float x, float y, float z) posB, (float x, float y, float z) posC)
        {
            AddTriangle(new Vector3(posA.x, posA.y, posA.z), new Vector3(posB.x, posB.y, posB.z), new Vector3(posC.x, posC.y, posC.z));
        }
        public void AddTriangle(Vector3 posA, Vector3 posB, Vector3 posC) {


            triangleIndices.Add(coords.Count);
            coords.Add(posA);

            triangleIndices.Add(coords.Count);
            coords.Add(posB);

            triangleIndices.Add(coords.Count);
            coords.Add(posC);

            var faceNormal = Vector3.Normalize( Vector3.Cross(posB - posA, posC - posA));

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
