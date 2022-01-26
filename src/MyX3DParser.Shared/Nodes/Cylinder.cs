
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using MyX3DParser.Generated.Model.AbstractNodes;
using MyX3DParser.Generated.Model.DataTypes;

namespace MyX3DParser.Generated.Model.Nodes
{
    public partial class Cylinder
    {
        private const int subdivision = 24;

        partial void Initialize()
        {

            var radius = this.radius.Value;
            var height = this.height.Value;
            var heightHalf = height / 2;
            var hasBottom = this.bottom.Value;
            var hasSide = this.side.Value;
            var hasTop = this.top.Value;
            var isSolid = this.solid.Value;


            var builder = new Shared.MeshBuilder();


            (float x, float z) previousPos = GetCirclePosition(radius, subdivision-1);
            for (int i = 0; i < subdivision; i++)
            {
                var pos = GetCirclePosition(radius, i);

                if (hasBottom)
                {
                    builder.AddTriangle( (previousPos.x, -heightHalf, previousPos.z), (pos.x, -heightHalf, pos.z), (0f, -heightHalf, 0f));
                }
                if (hasTop)
                {
                    builder.AddTriangle((pos.x, heightHalf, pos.z), (previousPos.x, heightHalf, previousPos.z), (0f, heightHalf, 0f));
                }
                if (hasSide)
                {
                    builder.AddTriangle((previousPos.x, heightHalf, previousPos.z), (pos.x, heightHalf, pos.z), (pos.x, -heightHalf, pos.z));
                    builder.AddTriangle((pos.x, -heightHalf, pos.z), (previousPos.x, -heightHalf, previousPos.z), (previousPos.x, heightHalf, previousPos.z));
                }

                if (isSolid)
                {

                    if (hasBottom)
                    {
                        builder.AddTriangle( (pos.x, -heightHalf, pos.z), (previousPos.x, -heightHalf, previousPos.z), (0f, -heightHalf, 0f));
                    }
                    if (hasTop)
                    {
                        builder.AddTriangle( (previousPos.x, heightHalf, previousPos.z), (pos.x, heightHalf, pos.z), (0f, heightHalf, 0f));
                    }
                    if (hasSide)
                    {
                        builder.AddTriangle( (pos.x, heightHalf, pos.z), (previousPos.x, heightHalf, previousPos.z), (pos.x, -heightHalf, pos.z));
                        builder.AddTriangle( (previousPos.x, -heightHalf, previousPos.z), (pos.x, -heightHalf, pos.z), (previousPos.x, heightHalf, previousPos.z));
                    }
                }



                previousPos = pos;
            }



            Mesh = builder.GetMeshAndDispose();
        }

        private static (float x, float z) GetCirclePosition(float radius, int i)
        {
            var relativeIndex = ((float)i) / subdivision;
            var angle = relativeIndex * 2 * Math.PI;
            return (x: radius * (float)Math.Cos(angle), z: radius * (float)Math.Sin(angle));
        }
    }
}