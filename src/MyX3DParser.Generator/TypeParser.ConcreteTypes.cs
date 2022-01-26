using MyX3DParser.Model.Builders;
using MyX3DParser.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyX3DParser.Model
{
    public static partial class TypeParser
    {
        private static void PrepareConcreteTypes(X3dUnifiedObjectModel model, List<IFileBuilder> builders)
        {
            foreach (var concreteNode in model.ConcreteNodes.EmptyIfNull())
            {
                if (concreteNode.InterfaceDefinition == null)
                {
                    throw new InvalidOperationException();
                }

                var baseTypeName = concreteNode.InterfaceDefinition.Inheritance?.baseType;
                var interfaceNames = concreteNode.InterfaceDefinition.AdditionalInheritance?.Select(o => o.baseType) ?? Array.Empty<string>();

                var interfaces = interfaceNames.Select(builders.GetAbstractType)
                    .ToList();
                if (baseTypeName != null)
                {
                    interfaces.Add(builders.GetAbstractType(baseTypeName));
                }

                var concreteNodeClass = new NodeBuilder(concreteNode.name.ThrowIfNull(), interfaces, concreteNode.InterfaceDefinition?.containerField?.@default);

                builders.Add(concreteNodeClass);
            }

            foreach (var statement in model.Statements.EmptyIfNull())
            {
                var statementClass = new StatementBuilder(statement.name.ThrowIfNull());

                builders.Add(statementClass);
            }
        }

        private static void GenerateConcreteTypes(X3dUnifiedObjectModel model, List<IFileBuilder> builders)
        {
            foreach (var concreteNode in model.ConcreteNodes.EmptyIfNull())
            {
                if (concreteNode.InterfaceDefinition == null)
                {
                    throw new InvalidOperationException();
                }

                var concreteNodeClass = builders.GetConcreteNodeBuilder(concreteNode.name.ThrowIfNull());

                foreach (var field in concreteNode.InterfaceDefinition.field.EmptyIfNull())
                {
                    // X3D Standard deviation: IS and USE field are not exposed, only used during serialization
                    if (field.name == "IS" || field.name == "USE")
                    {
                        continue;
                    }

                    if (builders.TryGetStatement(field.name.ThrowIfNull(), out var statement))
                    {
                        var isArray = field.type.ThrowIfNull()
                            .StartsWith("MF");
                        concreteNodeClass.AddStatementField(statement, isArray, field.name.ThrowIfNull(), field.@default.EmptyIfNull());
                    }
                    else
                    {
                        concreteNodeClass.AddField(builders.GetField(field.type.ThrowIfNull(), field.acceptableNodeTypes, field.simpleType, field.baseType), field.name.ThrowIfNull(), field.@default ?? model.FieldTypes.Single(o=>o.type==field.type).defaultValue ?? string.Empty, field.accessType.ThrowIfNull());
                    }
                }
            }

            foreach (var statement in model.Statements.EmptyIfNull())
            {
                if (statement.InterfaceDefinition == null)
                {
                    throw new InvalidOperationException();
                }

                var statementClass = builders.GetStatement(statement.name.ThrowIfNull());

                foreach (var field in statement.InterfaceDefinition.field.EmptyIfNull())
                {
                    if (builders.TryGetStatement(field.name.ThrowIfNull(), out var fieldStatement))
                    {
                        var isArray = field.type.ThrowIfNull()
                            .StartsWith("MF");
                        statementClass.AddStatementField(fieldStatement, isArray, field.name.ThrowIfNull(), field.@default.EmptyIfNull());
                    }
                    else
                    {
                        statementClass.AddField(builders.GetField(field.type.ThrowIfNull(), field.acceptableNodeTypes, field.simpleType, field.baseType), field.name.ThrowIfNull(), field.@default.EmptyIfNull(), field.accessType.ThrowIfNull());
                    }
                }
            }
        }
    }
}