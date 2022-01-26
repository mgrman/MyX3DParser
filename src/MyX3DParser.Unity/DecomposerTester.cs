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
using U_GameObject = UnityEngine.GameObject;
using MyX3DParser.Generated.Model.AbstractNodes;
using MyX3DParser.Generated.Model.Nodes;
using System.Linq;
using MyX3DParser.Generated;
using Color = UnityEngine.Color;
using U_Transform = UnityEngine.Transform;
using UnityEngine;

namespace MyX3DParser.Unity
{
    [UnityEngine.ExecuteInEditMode]
    public class DecomposerTester : U_MonoBehaviour
    {
        [U_SerializeField]
        private U_Vector3 translation;

        [U_SerializeField]
        private U_Vector3 center;

        [U_SerializeField]
        private U_Vector3 rotation;

        [U_SerializeField]
        private U_Vector3 scaleOrientation;

        [U_SerializeField]
        private U_Vector3 scale;

        private U_Transform[] children;

        void Update()
        {
            children = gameObject.EnsureChildren(9);
            var i = 0;

            children[i].name = "ConvertToUnitySpace";
            children[i].localScale = new Vector3(1, 1, -1);
            i++;

            children[i].name = "translation";
            children[i].localPosition = translation;
                     i++;
            children[i].name = "center";
            children[i].localPosition = center;
                     i++;
            children[i].name = "rotation";
            children[i].localRotation = U_Quaternion.Euler(rotation);
                     i++;
            children[i].name = "scaleOrientation";
            children[i].localRotation = U_Quaternion.Euler(scaleOrientation);
                     i++;
            children[i].name = "scale";
            children[i].localScale = scale;
            i++;
            children[i].name = "-scaleOrientation";
            children[i].localRotation = U_Quaternion.Inverse(U_Quaternion.Euler(scaleOrientation));
            i++;
            children[i].name = "-center";
            children[i].localPosition = -center;
        }



        // Update is called once per frame
        void OnDrawGizmos()
        {
            {

                var transform1 = new Generated.Model.Nodes.Transform(null, Vec3f.ConstantValue_0_0_0, Vec3f.ConstantValue_0_0_0, center, new List<X3DNode>(), null, U_Quaternion.Euler(rotation), scale, U_Quaternion.Euler(scaleOrientation), translation, "", "");

                transform1.OnParentPositionsChange(null, new[] { Shared.SceneNodeData.Root, });

                var resultX3d = transform1.MyPositions[0];

                U_Gizmos.matrix = transform.localToWorldMatrix * resultX3d.Matrix;
            U_Gizmos.DrawWireCube(U_Vector3.zero, U_Vector3.one);
            U_Gizmos.DrawWireCube(new U_Vector3(0.5f, 0.5f, 0.5f), new U_Vector3(0.07f, 0.07f, 0.07f));
        }

            { 
            U_Gizmos.matrix = children.Last().transform.localToWorldMatrix;
            U_Gizmos.color = Color.red;
            U_Gizmos.DrawWireCube(U_Vector3.zero, new U_Vector3(0.9f, 0.9f, 0.9f));
            U_Gizmos.DrawWireCube(new U_Vector3(0.5f, 0.5f, 0.5f), new U_Vector3(0.05f, 0.05f, 0.05f));
        }


            {

                var transform1 = new Generated.Model.Nodes.Transform(null, Vec3f.ConstantValue_0_0_0, Vec3f.ConstantValue_0_0_0, center, new List<X3DNode>(), null, U_Quaternion.Euler(rotation), scale, U_Quaternion.Euler(scaleOrientation), translation, "", "");

                transform1.OnParentPositionsChange(null, new[] { new Shared.SceneNodeData(Matrix4x4.identity,true), });

                var resultX3d = transform1.MyPositions[0];

                var (translation2, rotation2, scale2, scaleOrientation2) = resultX3d.Matrix.Decompose();
                var trs = Matrix4x4.TRS(translation2, rotation2.normalized * scaleOrientation2.normalized, scale2);
                var trs2 = Matrix4x4.TRS(Vector3.zero, Quaternion.Inverse(scaleOrientation2.normalized), Vector3.one);


                var resultMatrix2 = transform.localToWorldMatrix * trs * trs2;

                U_Gizmos.matrix = resultMatrix2;
                U_Gizmos.color = Color.green;
                U_Gizmos.DrawWireCube(U_Vector3.zero, new U_Vector3(0.8f, 0.8f, 0.8f));
                U_Gizmos.DrawWireCube(new U_Vector3(0.5f, 0.5f, 0.5f), new U_Vector3(0.04f, 0.04f, 0.04f));
            }
        }
    }
}
