using MyX3DParser.Model.Builders;
using MyX3DParser.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace MyX3DParser.Model
{
    public static partial class TypeParser
    {
        private static void GenerateFields(X3dUnifiedObjectModel model, List<IFileBuilder> builders)
        {
            foreach (var fieldType in model.FieldTypes.EmptyIfNull())
            {
                if (fieldType.type == null)
                {
                    throw new InvalidOperationException();
                }

                if (fieldType.type == "SFNode")
                {
                    var nodeTypeBuilder = builders.GetX3DNodeType();
                    builders.Add(new SFNodeFieldBuilder(nodeTypeBuilder));
                }
                else if (fieldType.type == "MFNode")
                {
                    var nodeTypeBuilder = builders.GetX3DNodeType();
                    builders.Add(new MFNodeFieldBuilder(nodeTypeBuilder));
                }
                else if (fieldType.type.StartsWith("SF"))
                {
                    var dataType = fieldType.type.Substring(2);
                    var dataTypeBuilder = builders.GetDataTypeBuilder(dataType);
                    builders.Add(new SFFieldBuilder(fieldType.type, fieldType.type, dataTypeBuilder));
                }
                else if (fieldType.type.StartsWith("MF"))
                {
                    var dataType = fieldType.type.Substring(2);
                    var dataTypeBuilder = builders.GetDataTypeBuilder(dataType);
                    builders.Add(new MFFieldBuilder(fieldType.type, fieldType.type, dataTypeBuilder));
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}