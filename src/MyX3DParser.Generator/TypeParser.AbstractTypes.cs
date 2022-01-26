using MyX3DParser.Model.Builders;
using MyX3DParser.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace MyX3DParser.Model
{
    public static partial class TypeParser
    {
        private static void PrepareX3DAbstractTypePlaceholder(X3dUnifiedObjectModel model, List<IFileBuilder> builders)
        {
            foreach (var abstractNodeType in model.AbstractNodeTypes.EmptyIfNull())
            {
                if (abstractNodeType.name == "X3DNode")
                {
                    var abstractNodeClass = new X3DNodeBuilder();
                    builders.Add(abstractNodeClass);
                }
            }
        }

        private static void PrepareAbstractTypes(X3dUnifiedObjectModel model, List<IFileBuilder> builders)
        {
            foreach (var abstractNodeType in model.AbstractObjectTypes.EmptyIfNull())
            {
                // X3D Standard deviation: All abstract objects are used as X3DNode interface so it is added to the inherited ones. Otherwise these interfaces would have to be considered as statements.  
                var abstractObjectClass = new AbstractNodeBuilder(abstractNodeType.name.ThrowIfNull(), new[] {builders.GetAbstractType("X3DNode")});
                builders.Add(abstractObjectClass);
            }

            var abstractNodesToProcess = new Queue<X3dUnifiedObjectModelAbstractNodeType>(model.AbstractNodeTypes.EmptyIfNull());
            while (abstractNodesToProcess.Count != 0)
            {
                var abstractNodeType = abstractNodesToProcess.Dequeue();
                if (abstractNodeType.name == "X3DNode")
                {
                    continue;
                }

                var interfaces = GetInterfaces(abstractNodeType)
                    .Select(builders.TryGetAbstractType)
                    .ToListNotNull(out var anyInterfaceNull);

                if (anyInterfaceNull)
                {
                    abstractNodesToProcess.Enqueue(abstractNodeType);
                    continue;
                }

                var abstractNodeClass = new AbstractNodeBuilder(abstractNodeType.name.ThrowIfNull(), interfaces);

                builders.Add(abstractNodeClass);
            }
        }

        private static void GenerateAbstractTypes(X3dUnifiedObjectModel model, List<IFileBuilder> builders)
        {
            foreach (var abstractNodeType in model.AbstractObjectTypes.EmptyIfNull())
            {
                var abstractObjectClass = builders.GetAbstractType(abstractNodeType.name.ThrowIfNull());

                foreach (var field in (abstractNodeType.InterfaceDefinition?.field).EmptyIfNull())
                {
                    // X3D Standard deviation: IS and USE field are not exposed, only used during serialization
                    if (field.name == "IS" || field.name == "USE")
                    {
                        continue;
                    }

                    abstractObjectClass.AddField(builders.GetField(field.type.ThrowIfNull(), null, field.simpleType, field.baseType), field.name.ThrowIfNull());
                }
            }

            foreach (var abstractNodeType in model.AbstractNodeTypes.EmptyIfNull())
            {
                var abstractNodeClass = builders.GetAbstractType(abstractNodeType.name.ThrowIfNull());

                foreach (var field in (abstractNodeType.InterfaceDefinition?.field).EmptyIfNull())
                {
                    // X3D Standard deviation: IS and USE field are not exposed, only used during serialization
                    if (field.name == "IS" || field.name == "USE")
                    {
                        continue;
                    }

                    abstractNodeClass.AddField(builders.GetField(field.type.ThrowIfNull(), field.acceptableNodeTypes, field.simpleType, field.baseType), field.name.ThrowIfNull());
                }
            }
        }

        private static IEnumerable<string> GetInterfaces(X3dUnifiedObjectModelAbstractNodeType abstractNodeType)
        {
            if (abstractNodeType.InterfaceDefinition == null)
            {
                return Array.Empty<string>();
            }

            return new[] {abstractNodeType.InterfaceDefinition.Inheritance?.baseType}.Concat(abstractNodeType.InterfaceDefinition.AdditionalInheritance.EmptyIfNull()
                    .Select(o => o.baseType))
                .WhereNotNull();
        }
    }
}