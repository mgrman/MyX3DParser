using MyX3DParser.Generated.Model;
using MyX3DParser.Generated.Model.AbstractNodes;
using MyX3DParser.Generated.Model.DataTypes;
using MyX3DParser.Generated.Model.Nodes;
using MyX3DParser.Generated.Model.Parsing;
using SharpGLTF.Geometry;
using SharpGLTF.IO;
using SharpGLTF.Materials;
using SharpGLTF.Scenes;
using SharpGLTF.Schema2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Xml;
using VERTEX = SharpGLTF.Geometry.VertexTypes.VertexPosition;

namespace MyX3DParser.Generated
{
    public static class ConvertToGLTF
    {
        public static byte[] ConvertToGLB(string x3dText)
        {
            // save the model in different formats
            var modelRoot = ConvertToGltfModel(x3dText);

            //var json = model.GetJSON(true);
            using (var stream = new MemoryStream())
            {
                var context = WriteContext
                    .CreateFromStream(stream)
                    .WithBinarySettings();

                //   context.Validation = SharpGLTF.Validation.ValidationMode.Skip;


                context.WriteBinarySchema2("test", modelRoot);
                return stream.ToArray();
            }
        }

        public static Stream ConvertToGLBStream(string x3dText)
        {
            // save the model in different formats
            var modelRoot = ConvertToGltfModel(x3dText);

            //var json = model.GetJSON(true);
            var stream = new MemoryStream();
            
                var context = WriteContext
                    .CreateFromStream(stream)
                    .WithBinarySettings();

                //   context.Validation = SharpGLTF.Validation.ValidationMode.Skip;


                context.WriteBinarySchema2("test", modelRoot);
                stream.Seek(0, SeekOrigin.Begin);
                return stream;
            
        }

        private static ModelRoot ConvertToGltfModel(string x3dText)
        {
            var x3dXmlDoc = new XmlDocument();
            x3dXmlDoc.LoadXml(x3dText);
            var x3d = Parser.Parse_X3D(x3dXmlDoc.DocumentElement, new X3DContext());


            // create a mesh with two primitives, one for each material
            // create a scene

            var scene = new SharpGLTF.Scenes.SceneBuilder();

            var meshes = x3d.ParentContext.ShapeNodes.ToDictionary(o => o,
                shape =>
                {
                    var x3dMaterial = shape.Material;
                    var mesh = new MeshBuilder<VERTEX>("mesh");

                    var material = new MaterialBuilder()
                    .WithDoubleSide(true)
                    .WithBaseColor(new Vector4(x3dMaterial.diffuseColor.R, x3dMaterial.diffuseColor.G, x3dMaterial.diffuseColor.B, x3dMaterial.diffuseColor.A))
                    .WithEmissive(new Vector3(x3dMaterial.emissiveColor.R, x3dMaterial.emissiveColor.G, x3dMaterial.emissiveColor.B));
                    //.WithMetallicRoughness(x3dMaterial.shininess)


                    var prim = mesh.UsePrimitive(material);

                    for (int i = 0; i < shape.Mesh.Indices.Count; i += 3)
                    {
                        var a = shape.Mesh.Vertices[shape.Mesh.Indices[i]];
                        var b = shape.Mesh.Vertices[shape.Mesh.Indices[i + 1]];
                        var c = shape.Mesh.Vertices[shape.Mesh.Indices[i + 2]];

                        prim.AddTriangle(new VERTEX(a), new VERTEX(b), new VERTEX(c));
                    }
                    return mesh;
                });


            NodeBuilder parent = new NodeBuilder();
            foreach (var child in x3d.Scene.children.SceneValue)
            {
                ProcessNode(child, parent, meshes, scene);
            }



            // save the model in different formats

            var model = scene.ToGltf2();
            return model;


        }

        private static void ProcessNode(X3DNode x3dNode, NodeBuilder parent,IReadOnlyDictionary<X3DShapeNode,MeshBuilder<VERTEX>> meshCache, SceneBuilder scene)
        {
            if (x3dNode is Switch switchNode)
            {
                for (int i = 0; i < switchNode.children.SceneValue.Count; i++)
                {
                    var child = switchNode.children.SceneValue[i];
                    if (i != switchNode.whichChoice.Value)
                    {
                        continue;
                    }
                    ProcessNode(child, parent, meshCache, scene);
                }
            }
            else if (x3dNode is Transform transformNode)
            {
                var node1a = new NodeBuilder();
                node1a.WithLocalTranslation(transformNode.translation.Value + transformNode.center.Value);
                node1a.WithLocalRotation(transformNode.rotation.Value * transformNode.scaleOrientation.Value);
                node1a.WithLocalScale(transformNode.scale.Value);
                parent.AddNode(node1a);

                var node2 = new NodeBuilder();
                node2.WithLocalRotation(System.Numerics.Quaternion.Inverse(transformNode.scaleOrientation.Value));
                node1a.AddNode(node2);

                var node3 = new NodeBuilder();
                node3.WithLocalTranslation(-transformNode.center.Value);
                node2.AddNode(node3);

                foreach (var child in transformNode.children.SceneValue)
                {
                    ProcessNode(child, node3, meshCache, scene);
                }
            }
            else if (x3dNode is X3DShapeNode shapeNode)
            {
                if (meshCache.TryGetValue(shapeNode, out var mesh))
                {
                    scene.AddRigidMesh(mesh, parent);
                }
            }
            else if (x3dNode is X3DGroupingNode groupNode)
            {
                var node = new NodeBuilder();
                if (parent != null)
                {
                    parent.AddNode(node);
                }
                foreach(var child in groupNode.children.SceneValue)
                {
                    ProcessNode(child, node, meshCache, scene);
                }
            }
        }


    }
}
