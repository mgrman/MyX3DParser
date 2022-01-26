using System;
using System.Collections.Generic;
using System.Text;
using MyX3DParser.Generated.Model.DataTypes;
using U_MonoBehaviour = UnityEngine.MonoBehaviour;
using U_SerializeField = UnityEngine.SerializeField;
using U_Vector3 = UnityEngine.Vector3;
using U_Matrix4x4 = UnityEngine.Matrix4x4;
using U_Quaternion = UnityEngine.Quaternion;
using U_Gizmos = UnityEngine.Gizmos;
using U_Debug = UnityEngine.Debug;
using U_Mesh = UnityEngine.Mesh;
using U_Material = UnityEngine.Material;
using U_Graphics = UnityEngine.Graphics;
using MyX3DParser.Generated.Model.AbstractNodes;
using MyX3DParser.Generated.Model.Nodes;
using System.Xml;
using MyX3DParser.Generated.Model;
using MyX3DParser.Generated.Model.Parsing;
using System.Linq;
using MyX3DParser.Generated;
using UnityEngine;
using Color = MyX3DParser.Generated.Model.DataTypes.Color;
using MyX3DParser.Generated.Model.Statements;

namespace MyX3DParser.Unity
{
    [UnityEngine.ExecuteInEditMode]
    public class X3DTester : U_MonoBehaviour
    {
        [HideInInspector]
        [System.NonSerialized]
        private UnityEngine.TextAsset oldX3D;

        [U_SerializeField]
        private UnityEngine.TextAsset X3D;


        [U_SerializeField]
        private U_Material baseMaterial;

        private X3D? x3dNode;

        private List<(U_Mesh unityMesh, U_Material material, X3DShapeNode node)> shapes;

        bool UpdateMeshes()
        {
            if (X3D == null)
            {
                shapes = null;
                oldX3D = null;
                x3dNode = null;
                return false;
            }

            if (oldX3D == X3D && shapes != null)
            {
                return false;
            }

            var x3dText = X3D.text;

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(x3dText);
            var x3d = Parser.Parse_X3D(xmlDoc.DocumentElement, new X3DContext());

            shapes = x3d.ParentContext.ShapeNodes
                .Where(o=>o.Mesh.Indices.Count>0)
                .Select(os =>
                 {
                     var unityMesh = new U_Mesh();

                     var vertices = os.Mesh.Vertices as Vector3[] ?? os.Mesh.Vertices.ToArray();
                     var indices = os.Mesh.Indices as int[] ?? os.Mesh.Indices.ToArray();

                     for (int i = 0; i < indices.Length / 3; i++)
                     {
                         var start = indices[i * 3];
                         indices[i * 3] = indices[i * 3 + 1];
                         indices[i * 3 + 1] = start;
                     }

                     unityMesh.vertices = vertices;
                     unityMesh.SetTriangles(indices, 0);
                     unityMesh.RecalculateNormals();

                     U_Material unityMaterial;
                     if (baseMaterial == null)
                     {
                         unityMaterial = new U_Material(UnityEngine.Shader.Find("Standard"));
                     }
                     else
                     {
                         unityMaterial = new U_Material(baseMaterial);
                     }

                     unityMaterial.SetColor("_Color", os.Material.diffuseColor);
                     if (os.Material.emissiveColor != Color.ConstantValue_0_0_0)
                     {
                         unityMaterial.SetColor("_EmissionColor", os.Material.emissiveColor);
                         unityMaterial.EnableKeyword("_EMISSION");
                         unityMaterial.globalIlluminationFlags = MaterialGlobalIlluminationFlags.BakedEmissive;
                     }

                     if (os.Material.diffuseColor.a != 1)
                     {
                         unityMaterial.SetOverrideTag("RenderType", "Transparent");
                         unityMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                         unityMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                         unityMaterial.SetInt("_ZWrite", 0);
                         unityMaterial.SetInt("_Mode", 3);
                         unityMaterial.DisableKeyword("_ALPHATEST_ON");
                         unityMaterial.DisableKeyword("_ALPHABLEND_ON");
                         unityMaterial.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                         unityMaterial.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                     }


                     return (unityMesh, unityMaterial, os);
                 })
                .ToList();

            oldX3D = X3D;
            x3dNode = x3d;

            return true;
        }

        void Update()
        {
            x3dNode?.ParentContext.TriggerNextFrame(Time.deltaTime);

            UpdateMeshes();
            foreach (var shape in shapes)
            {
                foreach (var position in shape.node.MyPositions)
                {
                    if (!position.IsVisible)
                    {
                        return;
                    }
                    U_Graphics.DrawMesh(shape.unityMesh, transform.localToWorldMatrix * position.Matrix, shape.material, UnityEngine.LayerMask.NameToLayer("Default"));
                }
            }
        }

        void OnDrawGizmos()
        {
            UpdateMeshes();
            foreach (var shape in shapes)
            {
                foreach (var position in shape.node.MyPositions)
                {
                    if (!position.IsVisible)
                    {
                        return;
                    }
                    U_Gizmos.color = shape.material.color;
                    U_Gizmos.matrix = transform.localToWorldMatrix* position.Matrix;
                    UnityEngine.Gizmos.DrawMesh(shape.unityMesh);
                }
            }

        }
    }
}
