
using System;
using System.Collections.Generic;
using System.Text;
using MyX3DParser.Generated.Model.AbstractNodes;
using MyX3DParser.Generated.Model.DataTypes;

namespace MyX3DParser.Generated.Model.Nodes
{
    public partial class IndexedFaceSet
    {
        partial void Initialize()
        {
            var ngonIndices = this.coordIndex.Value;

            var coordNode = this.coord.SceneValue_X3DCoordinateNode as Coordinate;
            if (coordNode == null)
            {
                Mesh = new Shared.Mesh();
                return;
            }

            var normalNode = this.normal.SceneValue_X3DNormalNode as Normal;

            var normals = normalNode?.vector.Value;

            var definedCoords = coordNode.point.Value;

            if (normals != null && normals.Count != definedCoords.Count)
            {
                normals = null;
            }


            if (normals == null)
            {

            }


            var builder = new Shared.MeshBuilder();

            var buffer = new List<int>();

            foreach (var index in ngonIndices)
            {
                if (index == -1)
                {
                    for (int i = 2; i < buffer.Count; i++)
                    {
                        var posA = definedCoords[buffer[0]];
                        var posB = definedCoords[buffer[i - 1]];
                        var posC = definedCoords[buffer[i]];
                        builder.AddTriangle(posA, posB, posC);
                    }
                    buffer.Clear();
                }
                else
                {
                    buffer.Add(index);
                }
            }

            Mesh = builder.GetMeshAndDispose();
        }
    }
}