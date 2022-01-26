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
    public class X3DTester2 : U_MonoBehaviour
    {
        [HideInInspector]
        [System.NonSerialized]
        private UnityEngine.TextAsset oldX3D;

        [U_SerializeField]
        private UnityEngine.TextAsset X3D;

        [U_SerializeField]
        private U_Material baseMaterial;

        private X3D? x3dNode;

        bool UpdateMeshes()
        {
            if (X3D == null)
            {
                oldX3D = null;
                x3dNode = null;
                return false;
            }

            if (oldX3D == X3D && transform.childCount!=0)
            {
                return false;
            }

            for (int i = transform.childCount; i >0; i--)
            {
                GameObject.DestroyImmediate(transform.GetChild(0).gameObject);
            }

            var x3dText = X3D.text;

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(x3dText);
            var x3d = Parser.Parse_X3D(xmlDoc.DocumentElement, new X3DContext());


            var meshes = x3d.ParentContext.ShapeNodes
                .ToDictionary(o => o, os =>
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

                    return (unityMesh, unityMaterial);
                });

            foreach (var child in x3d.Scene.children.SceneValue)
            {
                ProcessNode(child, gameObject, meshes);
            }

            oldX3D = X3D;
            x3dNode = x3d;

            return true;
        }

        void Update()
        {
            x3dNode?.ParentContext.TriggerNextFrame(Time.deltaTime);

            UpdateMeshes();
        }

        private static void ProcessNode(X3DNode x3dNode, GameObject parent, IReadOnlyDictionary<X3DShapeNode, (Mesh,U_Material)> meshCache)
        {
            if (x3dNode is Switch switchNode)
            {
                var go = new GameObject();
                go.name = "switch";
                go.transform.SetParent(parent.transform, false);
                go.transform.localPosition = Vector3.zero;
                go.transform.localScale = Vector3.one;
                go.transform.localRotation = Quaternion.identity;
                for (int i = 0; i < switchNode.children.SceneValue.Count; i++)
                {
                    var child = switchNode.children.SceneValue[i];
                    ProcessNode(child, go, meshCache);
                }
                for (int i = 0; i < go.transform.childCount; i++)
                {
                    go.transform.GetChild(i).gameObject.SetActive(i == switchNode.whichChoice.Value);
                }
            }
            else if (x3dNode is Generated.Model.Nodes.Transform transformNode)
            {
                var node1 = new GameObject();
                node1.name = $"T1: t:{transformNode.translation.Value} c:{transformNode.center.Value} r:{transformNode.rotation.Value} so:{transformNode.scaleOrientation.Value} s:{transformNode.scale.Value}";
                node1.transform.SetParent(parent.transform, false);
                node1.transform.localPosition= (transformNode.translation.Value + transformNode.center.Value);
                node1.transform.localRotation = (transformNode.rotation.Value * transformNode.scaleOrientation.Value);
                node1.transform.localScale = (transformNode.scale.Value);
                parent = node1;

                if (transformNode.scaleOrientation.Value != Quaternion.identity)
                {
                    var node2 = new GameObject();
                    node2.name = $"T2: -so:{Quaternion.Inverse(transformNode.scaleOrientation.Value)}";
                    node2.transform.SetParent(parent.transform, false);
                    node2.transform.localPosition = Vector3.zero;
                    node2.transform.localRotation = (Quaternion.Inverse(transformNode.scaleOrientation.Value));
                    node2.transform.localScale = Vector3.one;
                    parent = node2;
                }
                if (transformNode.center.Value != Vector3.zero)
                {
                    var node3 = new GameObject();
                    node3.name = $"T3: -c:{(-transformNode.center.Value)}";
                    node3.transform.SetParent(parent.transform, false);
                    node3.transform.localPosition = (-transformNode.center.Value);
                    node3.transform.localRotation = Quaternion.identity;
                    node3.transform.localScale = Vector3.one;
                    parent = node3;
                }

                foreach (var child in transformNode.children.SceneValue)
                {
                    ProcessNode(child, parent, meshCache);
                }
            }
            else if (x3dNode is X3DShapeNode shapeNode)
            {
                if (meshCache.TryGetValue(shapeNode, out var mesh))
                {

                    var go = new GameObject();
                    go.name = "mesh";
                    go.transform.SetParent(parent.transform, false);
                    go.transform.localPosition = Vector3.zero;
                    go.transform.localScale = new Vector3(1, 1, -1);
                    go.transform.localRotation = Quaternion.identity;

                    go.AddComponent<MeshFilter>().sharedMesh = mesh.Item1;
                    go.AddComponent<MeshRenderer>().sharedMaterial = mesh.Item2;
                }
            }
            else if (x3dNode is X3DGroupingNode groupNode)
            {
                var go = new GameObject();
                go.name = "group";
                go.transform.SetParent(parent.transform, false);
                go.transform.localPosition = Vector3.zero;
                go.transform.localScale = Vector3.one;
                go.transform.localRotation = Quaternion.identity;
                foreach (var child in groupNode.children.SceneValue)
                {
                    ProcessNode(child, go, meshCache);
                }
            }
        }
    }
}
