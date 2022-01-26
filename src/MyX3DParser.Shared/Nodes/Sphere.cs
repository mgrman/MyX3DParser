
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using MyX3DParser.Generated.Model.AbstractNodes;
using MyX3DParser.Generated.Model.DataTypes;

namespace MyX3DParser.Generated.Model.Nodes
{
    public partial class Sphere
    {
        private const int subdivisionX = 24;
        private const int subdivisionY = 12;

        partial void Initialize()
        {
            var radius = this.radius.Value;
            var isSolid = this.solid.Value;

            var builder = new Shared.MeshBuilder();

            for (int ix = 0; ix < subdivisionX; ix++)
            {
                for (int iy = 0; iy < subdivisionX; iy++)
                {
                    var pos00 = GetCirclePosition(ix, iy);
                    var pos10 = GetCirclePosition(ix+1, iy);
                    var pos01 = GetCirclePosition(ix, iy+1);
                    var pos11 = GetCirclePosition(ix+1, iy+1);

                    builder.AddTriangle(pos00, pos10, pos11);
                    builder.AddTriangle(pos00, pos11, pos01);
                    if (isSolid)
                    {
                        builder.AddTriangle(pos10, pos00, pos11);
                        builder.AddTriangle( pos11, pos00, pos01);
                    }
                }
            }

            Mesh = builder.GetMeshAndDispose();
        }

        private (float x, float y, float z) GetCirclePosition(int ix,int iy)
        {
            var radius = this.radius.Value;

            var relativeIndexX = ((float)(ix% subdivisionX)) / subdivisionX;
            var angleX = relativeIndexX * 2 * Math.PI;
            var relativeIndexY = ((float)(iy % subdivisionY)) / subdivisionY;
            var angleY = relativeIndexY * Math.PI;

            return (x: radius * (float)Math.Sin(angleY) * (float)Math.Cos(angleX), y: radius * (float)Math.Sin(angleX) * (float)Math.Sin(angleY), z: radius * (float)Math.Cos(angleY) );
        }
    }
}