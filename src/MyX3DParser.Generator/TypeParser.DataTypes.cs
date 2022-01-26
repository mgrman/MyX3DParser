using MyX3DParser.Model.Builders;
using MyX3DParser.Run;
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
        private static void GenerateDataTypes(X3dUnifiedObjectModel model,GeneratorConfig generatorConfig, List<IFileBuilder> builders)
        {
            var types = model.FieldTypes.EmptyIfNull()
                .Select(o => o.type.ThrowIfNull()
                    .Substring(2))
                .Distinct();
            foreach (var type in types)
            {
                switch (type)
                {
                    case "Node":
                        // handled via PrepareX3DAbstractTypePlaceholder method
                        break;

                    case "Time":
                    case "Double":
                    case "Float":
                    case "Int32":
                    case "Bool":
                    case "String":
                        builders.Add(new BCLTypeBuilder(type));
                        break;
                    default:

                        switch (generatorConfig.dataTypes)
                        {
                            case DataTypeBackingLibrary.Custom:
                                builders.Add(new CustomDataTypeBuilder(type));
                                break;
                            case DataTypeBackingLibrary.Unity:
                                if(UnityDataTypeBuilder.IsSupported(type))
                                {
                                    builders.Add(new UnityDataTypeBuilder(type));
                                }
                                else
                                {
                                    builders.Add(new CustomDataTypeBuilder(type));
                                }
                                break;
                            case DataTypeBackingLibrary.Numerics:
                                if (NumericsDataTypeBuilder.IsSupported(type))
                                {
                                    builders.Add(new NumericsDataTypeBuilder(type));
                                }
                                else
                                {
                                    builders.Add(new CustomDataTypeBuilder(type));
                                }
                                break;
                            default:
                                throw new InvalidOperationException();
                                break;
                        }
                        break;
                }
            }
        }
    }
}