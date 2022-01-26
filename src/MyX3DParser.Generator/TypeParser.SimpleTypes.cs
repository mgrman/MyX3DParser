using MyX3DParser.Model.Builders;
using MyX3DParser.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace MyX3DParser.Model
{
    public static partial class TypeParser
    {
        private static void GenerateSimpleTypes(X3dUnifiedObjectModel model, List<IFileBuilder> builders)
        {
            foreach (var simpleEnum in model.SimpleTypeEnumerations.EmptyIfNull())
            {
                // X3D Standard deviation: These simple types are not used anywhere, and just enumerate fields with given accessType
                if (simpleEnum.name == "initializeOnlyAccessTypes" || simpleEnum.name == "inputOnlyAccessTypes" || simpleEnum.name == "inputOutputAccessTypes" || simpleEnum.name == "outputOnlyAccessTypes")
                {
                    continue;
                }
        
                switch (simpleEnum.baseType)
                {
                    case "SFString":
                    case "xs:NMTOKEN":
                    {
                        var sfstring = builders.GetSFStringField(simpleEnum.baseType);
                        var builder = new SFStringSimpleTypeBuilder(sfstring, simpleEnum.name.ThrowIfNull(), IsBounded(model, simpleEnum), simpleEnum.enumeration.EmptyIfNull().Select(o=>o.value).ToList());
        
                        builders.Add(builder);
                        break;
                    }
                    case "MFString":
                    {
                        var mfstring = builders.GetMFStringField("MFString");
                        var builder = new MFStringSimpleTypeBuilder(mfstring,simpleEnum.name.ThrowIfNull(), IsBounded(model, simpleEnum), simpleEnum.enumeration.EmptyIfNull().Select(o=>o.value).ToList());
        
                        builders.Add(builder);
                        break;
                    }
                    // case var o when o.StartsWith("xs:"):
                    // {
                    //     var regexBuilder = new RegexDataTypeBuilder(simpleEnum.baseType, builders.GetBCLDataTypeBuilder("String"));
                    //     builders.Add(new SFStringRegexBuilder(builders.GetSFStringField("SFString"), regexBuilder));
                    //     break;
                    // }
                    case "SFFloat" when simpleEnum.name == "intensityType":
                    case "SFVec3f" when simpleEnum.name == "bboxSizeType":
                        // handled via GenerateSimpleTypesDefiningFieldBaseTypes
                        continue;
                    default:
                        throw new InvalidOperationException();
                }
            }
        }

        private static bool IsBounded(X3dUnifiedObjectModel model, X3dUnifiedObjectModelSimpleType simpleEnum)
        {
            var isBoundedInNode = model.ConcreteNodes.SelectMany(o => o.InterfaceDefinition.field)
                .Where(o => o.simpleType == simpleEnum.name)
                .Select(o => !o.additionalEnumerationValuesAllowed)
                .EnsureAllTheSame()
                .Take(1)
                .ToList();
            if (isBoundedInNode.Count == 1)
            {
                return isBoundedInNode[0];
            }

            var isBoundedInStatement = model.Statements.SelectMany(o => o.InterfaceDefinition.field)
                .Where(o => o.simpleType == simpleEnum.name)
                .Select(o => !o.additionalEnumerationValuesAllowed)
                .EnsureAllTheSame()
                .Take(1)
                .ToList();
            if (isBoundedInStatement.Count == 1)
            {
                return isBoundedInStatement[0];
            }

            var isContainerFieldType = model.ConcreteNodes.Select(o => o.InterfaceDefinition.containerField)
                .Any(o => o.type == simpleEnum.name);
            if (isContainerFieldType)
            {
                return true;
            }

            if (simpleEnum.name == "geoMetadataKeyValues")
            {
                return true; // based on reference page
            }

            if (simpleEnum.name == "geoSystemType")
            {
                // based on appinfo, the type values are bounded but not part of the xml (they can be generated though)
                return false;
            }

            if (simpleEnum.name == "hanimHumanoidInfoKeyValues")
            {
                return true; // based on reference page
            }

            if (simpleEnum.appinfo.Contains("Unbounded, additional values are allowed."))
            {
                return false;
            }

            if (simpleEnum.appinfo.Contains("Bounded, no additional values are allowed."))
            {
                return true;
            }

            throw new InvalidOperationException();
        }

        private static void GenerateSimpleTypesDefiningFieldBaseTypes(X3dUnifiedObjectModel model, List<IFileBuilder> builders)
        {
            foreach (var simpleEnum in model.SimpleTypeEnumerations.EmptyIfNull())
            {
                switch (simpleEnum.baseType)
                {
                    case "xs:NMTOKEN":
                    case "SFString":
                    case "MFString":
                        // handled via GenerateSimpleTypes
                        continue;
                    case "SFFloat" when simpleEnum.name == "intensityType":
                        {
                            var baseBuilder = builders.GetSFStringField("SFFloat");
                            var dataType = baseBuilder.DataType;
                            var builder = new SFFieldWithCustomConstraintBuilder("intensityType", $"{dataType.GreaterOrEqualMethod("value", dataType.GetSingleValue("0"))} && {dataType.LesserOrEqualMethod("value", dataType.GetSingleValue("1"))}", builders.GetSFStringField("SFFloat"));
                        builders.Add(builder);
                        break;
                    }
                    case "SFVec3f" when simpleEnum.name == "bboxSizeType":
                    {
                        var baseBuilder = builders.GetSFStringField("SFVec3f");
                        var dataType = baseBuilder.DataType;
                        var builder = new SFFieldWithCustomConstraintBuilder("bboxSizeType", $"value == {dataType.GetSingleValue("-1 -1 -1")} || {dataType.GreaterOrEqualMethod("value", dataType.GetSingleValue("0 0 0"))}", baseBuilder);
                        builders.Add(builder);
                        break;
                    }
                    default:
                        throw new InvalidOperationException();
                }
            }
        }
    }
}