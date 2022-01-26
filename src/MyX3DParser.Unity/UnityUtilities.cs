
using U_MonoBehaviour = UnityEngine.MonoBehaviour;
using U_SerializeField = UnityEngine.SerializeField;
using U_Vector3 = UnityEngine.Vector3;
using U_Quaternion = UnityEngine.Quaternion;
using U_Gizmos = UnityEngine.Gizmos;
using U_Debug = UnityEngine.Debug;
using U_GameObject = UnityEngine.GameObject;
using U_Transform = UnityEngine.Transform;


namespace MyX3DParser.Unity
{
    public static class UnityUtilities
    {


        public static U_Transform[] EnsureChildren(this U_GameObject gameObject, int counter)
        {
            return gameObject.transform.EnsureChildren(counter);
        }
        public static U_Transform[] EnsureChildren(this U_Transform gameObject ,int counter)
        {
            var parent = gameObject;

            var children = new U_Transform[counter];
            for (int i = 0; i < counter; i++)
            {
                children[i] = parent.EnsureChild();
                parent = children[i];
            }

            return children;
        }


        private static U_Transform EnsureChild(this U_Transform parent)
        {
            var child = parent.transform.childCount == 1
                ? parent.transform.GetChild(0)
                : null;

            if (child == null)
            {
                for (int i = 0; i < parent.transform.childCount; i++)
                {
                    UnityEngine.Object.DestroyImmediate(parent.transform.GetChild(0)
                        .gameObject);
                }

                child = new UnityEngine.GameObject("child").transform;
                child.SetParent(parent.transform);
            }

            child.localPosition = U_Vector3.zero;
            child.localRotation = U_Quaternion.identity;
            child.localScale = U_Vector3.one;
            return child;
        }
    }
}