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
using MyX3DParser.Generated.Model.AbstractNodes;
using MyX3DParser.Generated.Model.Nodes;
using MyX3DParser.Generated;

namespace MyX3DParser.Unity
{
    [UnityEngine.ExecuteInEditMode]
    public class DualTransformTester : U_MonoBehaviour
    {
        [U_SerializeField]
        private U_Vector3 translation1;

        [U_SerializeField]
        private U_Vector3 rotation1;

        [U_SerializeField]
        private U_Vector3 scale1;


        [U_SerializeField]
        private U_Vector3 translation2;

        [U_SerializeField]
        private U_Vector3 rotation2;

        [U_SerializeField]
        private U_Vector3 scale2;

        private UnityEngine.GameObject child1;
        private UnityEngine.GameObject child2;

        void Update()
        {
            child1 = transform.childCount == 1 ? transform.GetChild(0).gameObject : null;

            if (child1 == null)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    UnityEngine.Object.DestroyImmediate(transform.GetChild(0).gameObject);
                }

                child1 = new UnityEngine.GameObject("child1");
                child1.transform.SetParent(transform);
            }

            child1.transform.localPosition = translation1;
            child1.transform.localRotation = U_Quaternion.Euler(rotation1);
            child1.transform.localScale = scale1;


            child2 = child1.transform.childCount == 1 ? child1.transform.GetChild(0).gameObject : null;

            if (child2 == null)
            {
                for (int i = 0; i < child1.transform.childCount; i++)
                {
                    UnityEngine.Object.DestroyImmediate(child1.transform.GetChild(0).gameObject);
                }

                child2 = new UnityEngine.GameObject("child1");
                child2.transform.SetParent(child1.transform);
            }
            child2.transform.localPosition = translation2;
            child2.transform.localRotation = U_Quaternion.Euler(rotation2);
            child2.transform.localScale = scale2;
        }


        // Update is called once per frame
        void OnDrawGizmos()
        {

            var transform2 = new Transform(null, Vec3f.ConstantValue_0_0_0, Vec3f.ConstantValue_0_0_0, Vec3f.ConstantValue_0_0_0, new List<X3DNode>(), null, U_Quaternion.Euler(rotation2), scale2, Rotation.ConstantValue_0_0_1_0, translation2, "", "");


            var transform1 = new Transform(null, Vec3f.ConstantValue_0_0_0, Vec3f.ConstantValue_0_0_0, Vec3f.ConstantValue_0_0_0, new List<X3DNode>() { transform2 }, null, U_Quaternion.Euler(rotation1), scale1, Rotation.ConstantValue_0_0_1_0, translation1, "", "");



            transform1.OnParentPositionsChange(null, new[] { Shared.SceneNodeData.Root, });

            var resultX3d = transform2.MyPositions[0];

            U_Gizmos.matrix = transform.localToWorldMatrix * resultX3d.Matrix;
            U_Gizmos.DrawWireCube(U_Vector3.zero, U_Vector3.one);



            U_Gizmos.matrix = child2.transform.localToWorldMatrix;
            U_Gizmos.DrawCube(U_Vector3.zero, new U_Vector3(0.9f, 0.9f, 0.9f));
        }
    }
}
