
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using MyX3DParser.Generated.Model.AbstractNodes;
using MyX3DParser.Generated.Model.DataTypes;
using MyX3DParser.Shared;

namespace MyX3DParser.Generated.Model.Nodes
{
    public partial class Box
    {
        private const int subdivision = 24;

        partial void Initialize()
        {
            var size = this.size.Value.ToTuple();
            var halfSize = (x:size.x / 2,y: size.y / 2,z: size.z / 2);
            var isSolid = this.solid.Value;

            var builder = new Shared.MeshBuilder();

            var vertices = new[] {
                (-halfSize.x, -halfSize.y, -halfSize.z),
                ( halfSize.x, -halfSize.y, -halfSize.z),
                ( halfSize.x,  halfSize.y, -halfSize.z),
                (-halfSize.x,  halfSize.y, -halfSize.z),
                (-halfSize.x,  halfSize.y, halfSize.z),
                ( halfSize.x,  halfSize.y, halfSize.z),
                ( halfSize.x, -halfSize.y, halfSize.z),
                (-halfSize.x, -halfSize.y, halfSize.z),
            };

            builder.AddTriangle(vertices[0], vertices[2], vertices[1]); //face front
	        builder.AddTriangle(vertices[0], vertices[3], vertices[2]);
	        builder.AddTriangle(vertices[2], vertices[3], vertices[4]); //face top
	        builder.AddTriangle(vertices[2], vertices[4], vertices[5]);
	        builder.AddTriangle(vertices[1], vertices[2], vertices[5]); //face right
	        builder.AddTriangle(vertices[1], vertices[5], vertices[6]);
	        builder.AddTriangle(vertices[0], vertices[7], vertices[4]); //face left
	        builder.AddTriangle(vertices[0], vertices[4], vertices[3]);
	        builder.AddTriangle(vertices[5], vertices[4], vertices[7]); //face back
	        builder.AddTriangle(vertices[5], vertices[7], vertices[6]);
	        builder.AddTriangle(vertices[0], vertices[6], vertices[7]); //face bottom
	        builder.AddTriangle(vertices[0], vertices[1], vertices[6]);

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