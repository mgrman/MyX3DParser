using System;
using System.Collections;
using System.Collections.Generic;
using MyX3DParser.Generated.Model.AbstractNodes;
using MyX3DParser.Generated.Model.Fields;
using MyX3DParser.Generated.Model.Nodes;

namespace MyX3DParser.Generated.Model
{
    public static class X3DUtils
    {
        public static X3DNode? GetSceneNode(this X3DNode? node)
        {
            {
                while (node is ProtoInstance protoInstance)
                {
                    {
                        node = protoInstance.SceneNode;
                    }
                }

                return node;
            }
        }

        public static IEnumerable<X3DNode> GetSceneNodes(this IEnumerable<X3DNode> nodes)
        {
            foreach (var node in nodes)
            {
                var sceneNode = node.GetSceneNode();
                if (sceneNode != null)
                {
                    yield return sceneNode;
                }
            }
        }
    }
}