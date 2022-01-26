using MyX3DParser.Generated.Model.AbstractNodes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace MyX3DParser.Generated.Model.Statements
{
    public partial class ProtoDeclare
    {
        public IReadOnlyList<XmlElement> ProtoBodyXmlElements { get; }

        public ProtoDeclare(X3DContext parentContext, string @appinfo, string @documentation, string @name, ProtoInterface? @ProtoInterface, IReadOnlyList<XmlElement> @protoBodyXmlElements)
            : this(parentContext, appinfo, documentation, name, null, ProtoInterface)
        {
            this.ProtoBodyXmlElements = protoBodyXmlElements;
        }

        partial void Initialize()
        {
            ParentContext.AddProtoDeclare(this);
        }
        partial void ToX3DStringAppendNodes(HashSet<X3DNode> alreadySerializedNodes, StringBuilder sb, Action ensureEnded)
        {
            sb.Append("<ProtoBody>");
            foreach (var element in ProtoBodyXmlElements)
            {
                ensureEnded();
                sb.Append(element.OuterXml);
            }
            sb.Append("</ProtoBody>");
        }
    }
}
