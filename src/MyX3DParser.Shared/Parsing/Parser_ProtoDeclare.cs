using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using MyX3DParser.Generated.Model.DataTypes;
using MyX3DParser.Generated.Model.Fields;
using MyX3DParser.Generated.Model.Nodes;
using MyX3DParser.Generated.Model.Statements;
using MyX3DParser.Utils;

namespace MyX3DParser.Generated.Model.Parsing
{
    public static partial class Parser
    {
        public static void Parse_ProtoDeclare(XmlElement node, X3DContext context)
        {
            var childContext = context;
            string appinfo = "";
            string documentation = "";
            string name = "";

            List<XmlElement>? ProtoBody = null;
            ProtoInterface? ProtoInterface = null;

            foreach (XmlAttribute attr in node.Attributes)
            {
                switch (attr.LocalName)
                {
                    case "appinfo":
                        appinfo= SFString.Parse( attr.Value);
                        break;
                    case "documentation":
                        documentation = SFString.Parse(attr.Value);
                        break;
                    case "name":
                        name = SFString.Parse(attr.Value);
                        break;
                }
            }

            foreach (XmlElement childNode in node.ChildNodes)
            {
                switch (childNode.GetAttribute("containerField"))
                {
                    case "ProtoBody":
                        ProtoBody = childNode.ChildElements()
                            .ToList();
                        break;
                    case "ProtoInterface":
                        ProtoInterface = Parser.Parse_ProtoInterface(childNode, childContext);
                        break;

                    case "":
                        switch (childNode.LocalName)
                        {
                            case "ProtoBody":
                                ProtoBody = childNode.ChildElements()
                                    .ToList();
                                break;
                            case "ProtoInterface":
                                ProtoInterface = Parser.Parse_ProtoInterface(childNode, childContext);
                                break;
                        }

                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }

            if (ProtoBody == null)
            {
                throw new InvalidOperationException();
            }

            new ProtoDeclare(context, appinfo, documentation, name, ProtoInterface, ProtoBody);
        }
    }
}
