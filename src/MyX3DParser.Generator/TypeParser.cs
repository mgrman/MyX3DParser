using MyX3DParser.Model.Builders;
using MyX3DParser.Run;
using MyX3DParser.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace MyX3DParser.Model
{
    public static partial class TypeParser
    {
        internal static IReadOnlyList<IFileBuilder> Parse3_3(Stream stream, GeneratorConfig config)
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            if (stream == null)
            {
                throw new InvalidOperationException();
            }

            var xmlSerializer = new XmlSerializer(typeof(X3dUnifiedObjectModel));
            var model = xmlSerializer.Deserialize(stream) as X3dUnifiedObjectModel;

            if (model == null)
            {
                throw new InvalidOperationException();
            }

            var builders = new List<IFileBuilder>();

            GenerateDataTypes(model, config, builders);

            // GenerateSimpleTypes(model, builders);

            PrepareX3DAbstractTypePlaceholder(model, builders);

            GenerateFields(model, builders);


            var xsRegexex = new Dictionary<string, string>() {
                {"xs:ID", @"^[a-zA-Z0-9._\-()$]*$"},
                {"xs:IDREF", @"^[a-zA-Z0-9._\-()$]*$"},
                {"xs:NMTOKENS", @"^[a-zA-Z0-9._\-:]*(?: [a-zA-Z0-9._\-:]+)*$"},
                {"xs:NMTOKEN", @"^[a-zA-Z0-9._\-:]*$"}};

             foreach (var regex in xsRegexex)
             {
                 var regexBuilder = new RegexDataTypeBuilder(regex.Key, regex.Value, builders.GetBCLDataTypeBuilder("String"));
            
                 builders.Add(new SFStringRegexBuilder( builders.GetSFStringField("SFString"), regexBuilder));
             }
            
            GenerateSimpleTypes(model, builders);

            GenerateSimpleTypesDefiningFieldBaseTypes(model, builders);

            // numerical limits
            {
                var limits = model.ConcreteNodes.SelectMany(n => n.InterfaceDefinition.field.Select(o => (n.name, minInclusive: o.minInclusiveSpecified ? o.minInclusive : (decimal?) null, minExclusive: o.minExclusiveSpecified ? o.minExclusive : (decimal?) null, maxInclusive: o.maxInclusiveSpecified ? o.maxInclusive : (decimal?) null, maxExclusive: o.maxExclusiveSpecified ? o.maxExclusive : (decimal?) null, o.type)))
                    .Where(o => o.minInclusive != null || o.minExclusive != null || o.maxInclusive != null || o.maxExclusive != null)
                    .Where(o => !(o.type == "SFColor" && o.minInclusive == 0 && o.maxInclusive == 1))
                    .Where(o => !(o.type == "MFColor" && o.minInclusive == 0 && o.maxInclusive == 1))
                    .Where(o => !(o.type == "SFColorRGBA" && o.minInclusive == 0 && o.maxInclusive == 1))
                    .Where(o => !(o.type == "MFColorRGBA" && o.minInclusive == 0 && o.maxInclusive == 1))
                    .Select(o => (o.minInclusive, o.minExclusive, o.maxInclusive, o.maxExclusive, o.type))
                    .ToArray();

                var types = limits.Select(o => o.type)
                    .Distinct()
                    .WrapInQuotes()
                    .StringJoin(", ");

                foreach (var limit in limits)
                {
                    var baseField = builders.GetStringFieldBuilder(limit.type);

                    var constraint = new NumericalConstraintBuilder(limit.minInclusive, limit.minExclusive, limit.maxInclusive, limit.maxExclusive, baseField.DataType);

                    if (baseField is SFFieldBuilder sf)
                    {
                        var builder = new SFConstrainedNumericalFieldBuilder(sf, constraint);
                        builders.Add(builder);
                    }
                    else if (baseField is MFFieldBuilder mf)
                    {
                        var builder = new MFConstrainedNumericalFieldBuilder(mf, constraint);
                        builders.Add(builder);
                    }
                    else
                    {
                        throw new InvalidOperationException();
                    }
                }
            }

            // SFString string limits
            // {
            //     var usedBaseTypes = model.ConcreteNodes.SelectMany(n => n.InterfaceDefinition.field)
            //         .Where(o => o.type == "SFString" && o.baseType != null)
            //         .Select(o => o.baseType)
            //         .Where(o => o != "SFString")
            //         .Distinct()
            //         .ToArray();
            //
            //     var usedSimpleTypes = model.ConcreteNodes.SelectMany(n => n.InterfaceDefinition.field)
            //         .Where(o => o.type == "SFString" && o.simpleType != null)
            //         .Select(o => o.simpleType)
            //         .Concat(usedBaseTypes.Where(o => !o.StartsWith("xs:")))
            //         .Distinct()
            //         .ToArray();
            //
            //     usedBaseTypes = usedBaseTypes.Where(o => o.StartsWith("xs:"))
            //         .ToArray();
            //
            //     foreach (var simpleType in usedSimpleTypes)
            //     {
            //         // var typeBuilder = builders.OfType<StringEnumBuilder>()
            //         //     .Single(o => o.Name == simpleType);
            //
            //         // if (typeBuilder == null)
            //         // {
            //         //     throw new InvalidOperationException();
            //         // }
            //
            //         builders.Add(new SFStringSimpleTypeBuilder(builders.GetSFStringField("SFString"), simpleType));
            //     }
            //     //
            //     // foreach (var baseType in usedBaseTypes)
            //     // {
            //     //     var name = baseType.Substring(3);
            //     //
            //     //     var regexBuilder = new RegexDataTypeBuilder(baseType, builders.GetBCLDataTypeBuilder("String"));
            //     //
            //     //     builders.Add(new SFStringRegexBuilder( builders.GetSFStringField("SFString"), regexBuilder));
            //     // }
            // }

            // MFString limits
            // {
            //     var usedBaseTypes = model.ConcreteNodes.SelectMany(n => n.InterfaceDefinition.field)
            //         .Where(o => o.type == "MFString" && o.baseType != null)
            //         .Select(o => o.baseType)
            //         .Where(o => o != "MFString")
            //         .Distinct()
            //         .ToArray();
            //
            //     var usedSimpleTypes = model.ConcreteNodes.SelectMany(n => n.InterfaceDefinition.field)
            //         .Where(o => o.type == "MFString" && o.simpleType != null)
            //         .Select(o => o.simpleType)
            //         .Concat(usedBaseTypes.Where(o => !o.StartsWith("xs:")))
            //         .Distinct()
            //         .ToArray();
            //     usedBaseTypes = usedBaseTypes.Where(o => o.StartsWith("xs:"))
            //         .ToArray();
            //
            //     if (usedBaseTypes.Length != 0)
            //     {
            //         throw new NotImplementedException();
            //     }
            //
            //     foreach (var simpleType in usedSimpleTypes)
            //     {
            //         // var typeBuilder = builders.OfType<StringArrayEnumBuilder>()
            //         //     .Single(o => o.Name == simpleType);
            //         //
            //         // if (typeBuilder == null)
            //         // {
            //         //     throw new InvalidOperationException();
            //         // }
            //
            //         builders.Add(new MFStringSimpleTypeBuilder(builders.GetMFStringField("MFString"), simpleType));
            //     }
            // }

            PrepareAbstractTypes(model, builders);
            PrepareConcreteTypes(model, builders);

            var groups = builders.OfType<NodeBuilder>()
                .GroupBy(o => o.Name)
                .Select(o => o.Count())
                .Max();

            // SFNode  limits
            {
                var usedBaseTypes = model.ConcreteNodes.SelectMany(n => n.InterfaceDefinition.field)
                    .Where(o => o.type == "SFNode" && o.baseType != null)
                    .Select(o => o.baseType)
                    .Where(o => o != "SFNode")
                    .Distinct()
                    .ToArray();
                if (usedBaseTypes.Length != 0)
                {
                    throw new NotImplementedException();
                }

                var usedSimpleTypes = model.ConcreteNodes.SelectMany(n => n.InterfaceDefinition.field)
                    .Where(o => o.type == "SFNode" && o.simpleType != null)
                    .Select(o => o.simpleType)
                    .Distinct()
                    .ToArray();
                if (usedSimpleTypes.Length != 0)
                {
                    throw new NotImplementedException();
                }

                var nodeTypes = model.ConcreteNodes.SelectMany(n => n.InterfaceDefinition.field)
                    .Where(o => o.type == "SFNode" && o.acceptableNodeTypes != null)
                    .Select(o => o.acceptableNodeTypes)
                    .Distinct()
                    .ToArray();

                var statementTypes = model.Statements.SelectMany(n => n.InterfaceDefinition.field)
                    .Where(o => o.type == "SFNode" && o.acceptableNodeTypes != null)
                    .Select(o => o.acceptableNodeTypes)
                    .Distinct()
                    .ToArray();

                foreach (var nodeTypeConstraint in nodeTypes.Concat(statementTypes)
                    .Distinct())
                {
                    if (nodeTypeConstraint == "IS")
                    {
                        //skipped since this is parsing only value
                        continue;
                    }

                    if (builders.TryGetStatement(nodeTypeConstraint, out _))
                    {
                        continue;
                    }

                    var acceptedTypes = builders.GetNodeBuilders(nodeTypeConstraint);
         
                   var baseType = builders.GetField("SFNode") as BaseNodeFieldBuilder; // using root base type since hierarchy is not very clear

                    builders.Add(new SFNodeAcceptableNodeBuilder(acceptedTypes, baseType));
                }
            }

            // MFNode limits
            {
                var usedBaseTypes = model.ConcreteNodes.SelectMany(n => n.InterfaceDefinition.field)
                    .Where(o => o.type == "MFNode" && o.baseType != null)
                    .Select(o => o.baseType)
                    .Distinct()
                    .ToArray();
                if (usedBaseTypes.Length != 0)
                {
                    throw new NotImplementedException();
                }

                var usedSimpleTypes = model.ConcreteNodes.SelectMany(n => n.InterfaceDefinition.field)
                    .Where(o => o.type == "MFNode" && o.simpleType != null)
                    .Select(o => o.simpleType)
                    .Distinct()
                    .ToArray();
                if (usedSimpleTypes.Length != 0)
                {
                    throw new NotImplementedException();
                }

                var nodeTypes = model.ConcreteNodes.SelectMany(n => n.InterfaceDefinition.field)
                    .Where(o => o.type == "MFNode" && o.acceptableNodeTypes != null)
                    .Select(o => o.acceptableNodeTypes)
                    .Where(o => o != "X3DNode")
                    .Distinct()
                    .ToArray();

                var statementTypes = model.Statements.SelectMany(n => n.InterfaceDefinition.field)
                    .Where(o => o.type == "MFNode" && o.acceptableNodeTypes != null)
                    .Select(o => o.acceptableNodeTypes)
                    .Where(o => o != "X3DNode")
                    .Distinct()
                    .ToArray();


                foreach (var nodeTypeConstraint in nodeTypes.Concat(statementTypes)
                    .Distinct())
                {
                    if (builders.TryGetStatement(nodeTypeConstraint, out _))
                    {
                        continue;
                    }

                    var acceptedTypes = builders.GetNodeBuilders(nodeTypeConstraint);

                    BaseNodeFieldBuilder baseType = builders.GetField("MFNode") as BaseNodeFieldBuilder;
                    if (acceptedTypes.Count == 1 && acceptedTypes[0]
                        .Name == "CADFace")
                    {
                        var groupNodeBuilder = builders.GetNodeBuilders("X3DChildNode")[0];

                        // var groupBaseType = new MFNodeAcceptableNodeBuilder(new[] {groupNodeBuilder}, baseType);
                        // builders.Add(groupBaseType);
                        var groupBaseType = builders.GetNodeFieldForType(groupNodeBuilder, true);
                        builders.Add(new MFNodeAcceptableNodeBuilder(acceptedTypes, groupBaseType));
                    }
                    else
                    {
                        builders.Add(new MFNodeAcceptableNodeBuilder(acceptedTypes, baseType));
                    }

                }
            }


            GenerateAbstractTypes(model, builders);

            GenerateConcreteTypes(model, builders);

            GenerateParser(model, builders);

            return builders;
        }

        private static bool IsMF_MultiType(IFieldBuilder f)
        {
            return f.IsArray && (f as INodeFieldBuilder)!.DataTypes.Count>1;
        }

        private static bool IsMF_SingleType(IFieldBuilder f)
        {
            return f.IsArray && (f as INodeFieldBuilder)!.DataTypes.Count == 1;
        }

        private static bool IsSF_MultiType(IFieldBuilder f)
        {
            return !f.IsArray && (f as INodeFieldBuilder)!.DataTypes.Count > 1;
        }

        private static bool IsSF_SingleType(IFieldBuilder f)
        {
            return !f.IsArray && (f as INodeFieldBuilder)!.DataTypes.Count == 1;
        }
    }
}