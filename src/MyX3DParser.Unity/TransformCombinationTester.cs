using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyX3DParser.Generated.Model.DataTypes;
using MyX3DParser.Generated.Model.AbstractNodes;
using MyX3DParser.Generated;
using UnityEngine;

namespace MyX3DParser.Unity
{
    [UnityEngine.ExecuteInEditMode]
    public class TransformCombinationTester : MonoBehaviour
    {
        [SerializeField]
        private Vector3 translation1;

        [SerializeField]
        private Vector3 center1;

        [SerializeField]
        private Vector3 rotation1;

        [SerializeField]
        private Vector3 scaleOrientation1;

        [SerializeField]
        private Vector3 scale1;

        [SerializeField]
        private Vector3 translation2;

        [SerializeField]
        private Vector3 center2;

        [SerializeField]
        private Vector3 rotation2;

        [SerializeField]
        private Vector3 scaleOrientation2;

        [SerializeField]
        private Vector3 scale2;

        private Transform[] children;

        void Update()
        {
            children = gameObject.EnsureChildren(16);
            var i = 0;

            children[i].name = "ConvertToUnitySpace";
            children[i].localScale = new Vector3(1, 1, -1);
            i++;

            children[i].name = "translation1";
            children[i].localPosition = translation1;
            i++;
            children[i].name = "center1";
            children[i].localPosition = center1;
            i++;
            children[i].name = "rotation1";
            children[i].localRotation = Quaternion.Euler(rotation1);
            i++;
            children[i].name = "scaleOrientation1";
            children[i].localRotation = Quaternion.Euler(scaleOrientation1);
            i++;
            children[i].name = "scale1";
            children[i].localScale = scale1;
            i++;
            children[i].name = "-scaleOrientation1";
            children[i].localRotation = Quaternion.Inverse(Quaternion.Euler(scaleOrientation1));
            i++;
            children[i].name = "-center1";
            children[i].localPosition = -center1;
            i++;

            children[i].name = "translation2";
            children[i].localPosition = translation2;
            i++;
            children[i].name = "center2";
            children[i].localPosition = center2;
            i++;
            children[i].name = "rotation2";
            children[i].localRotation = Quaternion.Euler(rotation2);
            i++;
            children[i].name = "scaleOrientation2";
            children[i].localRotation = Quaternion.Euler(scaleOrientation2);
            i++;
            children[i].name = "scale2";
            children[i].localScale = scale2;
            i++;
            children[i].name = "-scaleOrientation2";
            children[i].localRotation = Quaternion.Inverse(Quaternion.Euler(scaleOrientation2));
            i++;
            children[i].name = "-center2";
            children[i].localPosition = -center2;

        }

        // Update is called once per frame
        void OnDrawGizmos()
        {
            var resultX3d = CombineViaTransform();

            Gizmos.matrix = transform.localToWorldMatrix * resultX3d.Matrix;
            Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
            Gizmos.DrawWireCube(new Vector3(0.5f, 0.5f, 0.5f), new Vector3(0.07f, 0.07f, 0.07f));


            Gizmos.matrix = children.Last()
                .transform.localToWorldMatrix ;
            Gizmos.color = UnityEngine.Color.green;
            Gizmos.DrawWireCube(Vector3.zero, new Vector3(0.9f, 0.9f, 0.9f));
            Gizmos.DrawWireCube(new Vector3(0.5f, 0.5f, 0.5f), new Vector3(0.03f, 0.03f, 0.03f));
        }

        private Shared.SceneNodeData CombineViaTransform(Shared.SceneNodeData? sceneNodeData=null)
        {
            var transform2 = new Generated.Model.Nodes.Transform(null,
                Vec3f.ConstantValue_0_0_0,
                Vec3f.ConstantValue_0_0_0,
                center2,
                new List<X3DNode>(),
                null,
                Quaternion.Euler(rotation2),
                scale2,
                Quaternion.Euler(scaleOrientation2),
                translation2,
                "",
                "");

            var transform1 = new Generated.Model.Nodes.Transform(null,
                Vec3f.ConstantValue_0_0_0,
                Vec3f.ConstantValue_0_0_0,
                center1,
                new List<X3DNode>() { transform2 },
                null,
                Quaternion.Euler(rotation1)
                    ,
                scale1,
                Quaternion.Euler(scaleOrientation1)
                    ,
                translation1,
                "",
                "");

            transform1.OnParentPositionsChange(null, new[] { sceneNodeData ?? Shared.SceneNodeData.Root, });

            var resultX3d = transform2.MyPositions[0];
            return resultX3d;
        }

    }
}