using System;
using System.Linq;
using MyX3DParser.Generated.Model.AbstractNodes;
using MyX3DParser.Generated.Model.Statements;
using MyX3DParser.Utils;

namespace MyX3DParser.Generated.Model.Nodes
{
    public partial class ProtoInstance
    {
        private X3DContext childContext;
        public X3DContext ChildContext => childContext;

        public X3DNode? SceneNode { get; private set; }

        partial void Initialize()
        {
            if (name.Value.IsNullOrEmpty())
            {
                throw new InvalidOperationException();
            }

            var protoDeclare = ParentContext.TryGetProtoDeclare(name.Value);

            if(protoDeclare!=null)
            {
                childContext = new X3DContext(ParentContext, this);
                foreach (var interfaceFieldDefinition in protoDeclare.ProtoInterface?.field ?? Array.Empty<field>())
                {
                    var fieldAccessType = interfaceFieldDefinition.accessType.Value;
                    var fieldName = interfaceFieldDefinition.name.Value;

                    if (this.fieldValue.TryGetBy(o => o.name.Value == interfaceFieldDefinition.name.Value, out var fieldValue))
                    {
                        childContext.AddInterfaceField(fieldAccessType, fieldName, MyX3DParser.Generated.Model.Parsing.Parser.ParseField(interfaceFieldDefinition.type.Value, fieldValue.value.Value, fieldValue.children.Value));
                    }
                    else
                    {
                        childContext.AddInterfaceField(fieldAccessType, fieldName, MyX3DParser.Generated.Model.Parsing.Parser.ParseField(interfaceFieldDefinition.type.Value, interfaceFieldDefinition.value.Value, interfaceFieldDefinition.children.Value));
                    }
                }

                var bodyElements = protoDeclare.ProtoBodyXmlElements.Select(o => MyX3DParser.Generated.Model.Parsing.Parser.Parse(o, childContext))
                    .ToList();

                SceneNode = bodyElements.First(o=>o!=null); // TODO not sure but we need to skip potential proto declarions in the begining

                while(SceneNode is ProtoInstance childProtoInstance)
                {
                    SceneNode = childProtoInstance.SceneNode;
                }

                if (SceneNode == null)
                {
                    // this might be fine by the standard, not sure. Does not seem like it though https://www.web3d.org/documents/specifications/19775-1/V3.2/Part01/concepts.html#PrototypeSemantics
                    throw new InvalidOperationException();
                }

                return;
            }

            var externProtoDeclare = ParentContext.TryGetExternProtoDeclare(name.Value);

            if (externProtoDeclare.HasValue)
            {
                childContext = new X3DContext(ParentContext, this);
                foreach (var interfaceFieldDefinition in externProtoDeclare.Value.@interface.field ?? Array.Empty<field>())
                {
                    var fieldAccessType = interfaceFieldDefinition.accessType.Value;
                    var fieldName = interfaceFieldDefinition.name.Value;

                    if (this.fieldValue.TryGetBy(o => o.name.Value == interfaceFieldDefinition.name.Value, out var fieldValue))
                    {
                        childContext.AddInterfaceField(fieldAccessType, fieldName, MyX3DParser.Generated.Model.Parsing.Parser.ParseField(interfaceFieldDefinition.type.Value, fieldValue.value.Value, fieldValue.children.Value));
                    }
                    else
                    {
                        childContext.AddInterfaceField(fieldAccessType, fieldName, MyX3DParser.Generated.Model.Parsing.Parser.ParseField(interfaceFieldDefinition.type.Value, interfaceFieldDefinition.value.Value, interfaceFieldDefinition.children.Value));
                    }
                }

                if (externProtoDeclare.Value.body != null)
                {
                    var bodyElements = externProtoDeclare.Value.body.Select(o => MyX3DParser.Generated.Model.Parsing.Parser.Parse(o, childContext))
                        .ToList();

                    SceneNode = bodyElements.First(o => o != null); // TODO not sure but we need to skip potential proto declarions in the begining

                    while (SceneNode is ProtoInstance childProtoInstance)
                    {
                        SceneNode = childProtoInstance.SceneNode;
                    }

                    if (SceneNode == null)
                    {
                        // this might be fine by the standard, not sure. Does not seem like it though https://www.web3d.org/documents/specifications/19775-1/V3.2/Part01/concepts.html#PrototypeSemantics
                        throw new InvalidOperationException();
                    }
                }
                else
                {
                      SceneNode = null;
                }

                return;
            }


            if (StaticConfig.DoNotThrowErrorsOnMissingPrototypes)
            {
                childContext = new X3DContext(ParentContext, this);
                SceneNode = null;
                return;
            }
            else
            {
                throw new InvalidOperationException();
            }

            //  if (protoInstance.DEF.Value.IsNotNullOrEmpty())
            //   {
            //     context.AddDEF(protoInstance.DEF.Value, sceneNode);
            //  }

        }
    }
}