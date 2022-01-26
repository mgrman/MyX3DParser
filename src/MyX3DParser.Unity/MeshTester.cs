using System;
using System.Collections.Generic;
using System.Text;
using MyX3DParser.Generated.Model.DataTypes;
using U_MonoBehaviour = UnityEngine.MonoBehaviour;
using U_SerializeField = UnityEngine.SerializeField;
using U_Vector3 = UnityEngine.Vector3;
using U_Quaternion = UnityEngine.Quaternion;
using U_Gizmos = UnityEngine.Gizmos;
using U_Debug = UnityEngine.Debug;
using U_Mesh = UnityEngine.Mesh;
using MyX3DParser.Generated.Model.AbstractNodes;
using MyX3DParser.Generated.Model.Nodes;
using System.Xml;
using MyX3DParser.Generated.Model;
using MyX3DParser.Generated.Model.Parsing;
using System.Linq;
using UnityEngine;

namespace MyX3DParser.Unity
{
    [UnityEngine.ExecuteInEditMode]
    public class MeshTester : U_MonoBehaviour
    {

        [U_SerializeField]
        private UnityEngine.TextAsset X3D;




        // Update is called once per frame
        void Update()
        {
            if (X3D == null)
            {
                return;
            }

            var x3dText = X3D.text;


            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(x3dText);
            var x3d = Parser.Parse_X3D(xmlDoc.DocumentElement, new X3DContext());

            for (int i = 0; i < x3d.ParentContext.ShapeNodes.Count; i++)
            {
                X3DShapeNode shape = x3d.ParentContext.ShapeNodes[i];
                var unityMesh = new U_Mesh();
                unityMesh.vertices = shape.Mesh.Vertices.ToArray();
                unityMesh.SetTriangles(shape.Mesh.Indices.ToArray(), 0);
                unityMesh.RecalculateNormals();

                {

                    if (transform.childCount < (i + 1))
                    {
                        new GameObject(i.ToString()).transform.SetParent(transform);
                    }

                    var child = transform.GetChild(i);

                    var meshFilter = child.GetComponent<MeshFilter>();
                    if (meshFilter == null)
                    {
                        meshFilter = child.gameObject.AddComponent<MeshFilter>();
                    }

                    var meshRenderer = child.GetComponent<MeshRenderer>();
                    if (meshRenderer == null)
                    {
                        meshRenderer = child.gameObject.AddComponent<MeshRenderer>();
                    }

                    meshFilter.mesh = unityMesh;
                }
            }

        }
    }
}
