using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MyX3DParser.Utils;

namespace MyX3DParser.Model.Builders
{
    internal class ParserMethod_ElementBuilder
    {
        private readonly IElementBuilder type;

        public static readonly IReadOnlyList<string> customWrittenParser = new[]
        {
            //  "EXPORT", "IMPORT", //prototyping using whole X3D document -> needs to be added to context
            //"ExternProtoDeclare", "ProtoDeclare", "ProtoBody", "ProtoInstance", // prototype definition - > needs to be added to context, all but protoBody can be parsed directly, ProtoBody needs to remain as XML
            "ProtoDeclare",
            //"ROUTE" //Route handling -> needs to be added to context
        };

        public ParserMethod_ElementBuilder(IElementBuilder type)
        {
            this.type = type;
        }

        public string? ToParserMethod()
        {
            if (type.Name.IsContainedIn(customWrittenParser))
            {
                return null;
            }

            if (type.Fields.Where(f => !(f.type is IStringFieldBuilder) && !(f.type is INodeFieldBuilder))
                .Any(o => !type.IsRouteOnlyField(o)))
            {
                throw new InvalidOperationException();
            }

            var stringFields = type.Fields.Where(f => f.type is IStringFieldBuilder)
                .Select(o=> (type:o.type as IStringFieldBuilder, o.name, o.cleanName, o.accessType, o.defaultValue))
                .Where(o => !type.IsRouteOnlyField(o))
                .ToArray();

            var nodeFields = type.Fields.Where(f => f.type is INodeFieldBuilder)
                .Select(o => (type: o.type as INodeFieldBuilder, o.name, o.cleanName, o.accessType, o.defaultValue))
                .Where(o => !type.IsRouteOnlyField(o))
                // .Select(f =>
                // {
                //     return new NodeFieldBuilder(f, type);
                // })
                .ToArray();

            return @$"

public static {(type.Name == "ProtoInstance" ? "X3DNode?" : type.CleanSingleTypeName)} Parse_{type.Name}(XmlElement node, X3DContext context)
{{
    {type.Fields.Where(o => !type.IsRouteOnlyField(o)).Select(f => $"{(f.type.BuilderDataType)} @{f.cleanName}= {f.defaultValue};").LineJoin()}
    {type.Statements.Where(o => !o.isArray).Select(f => $"{f.type.CleanSingleTypeName} @{f.cleanName}= default;").LineJoin()}
    {type.Statements.Where(o => o.isArray).Select(f => $"List<{f.type.Name}> @{f.cleanName}= new List<{f.type.Name}>();").LineJoin()}

                {(stringFields.Any() ? $@"foreach(XmlAttribute attr in node.Attributes)
                {{
                    switch(attr.LocalName)
                    {{
                        {(type is NodeBuilder ? $@"case ""USE"":
                            var useNode= context.TryGetUSE(attr.Value);
                            if(useNode is {type.CleanSingleTypeName.Trim('?')} castedUseNode)
                            {{
                                return castedUseNode;
                            }}
                            else if(!StaticConfig.DoNotThrowErrorsOnMissingUSE )
                            {{
                                throw new InvalidOperationException();
                            }}
                            else
                            {{
                                return null;
                            }}" : "")}
                        {stringFields.Select(f => $@"            case ""{f.name}"":
                            @{f.cleanName} = {f.type.X3DFieldName}.Parse(attr.Value);
                            break;").LineJoin()}
                    }}
                }}" : "")}


                var isList=new List<connect>();
                foreach(XmlElement childNode in node.ChildElements())
                {{

                    switch(childNode.LocalName)
                    {{
{(type is NodeBuilder ? @$"                        case ""IS"":
                            var isNode=Parse_IS(childNode, context);
                            if(isNode==null)
                            {{
                                continue;
                            }}
                            foreach(var connection in isNode.connect)
                            {{
                                isList.Add(connection);
                                if(context.TryGetInitializationProtoField(connection.protoField.Value, out var protoValue))
                                {{
                                if(protoValue.isInitializeOnly)
                                {{
                                    switch(connection.nodeField.Value)
                                    {{
{type.Fields.Where(f=> f.accessType == "intializeOnly" || f.accessType == "inputOutput").Select(f => $@"                                    case ""{f.name}"":
                                        {{
                                            var castedField=(protoValue.field as {f.type.X3DFieldName});
                                            if(castedField==null)
                                            {{
                                                throw new InvalidOperationException();
                                            }}
                                            {(f.type.X3DFieldName == "MFNode" ? $@"@{f.cleanName}.Clear();
                                            @{f.cleanName}.AddRange(castedField.Value);
" : $@"@{f.cleanName}=castedField.Value;")}
                                        }}
                                        break;").LineJoin()}
                                    default:
                                        break;
                                    }}
                                }}
                                else
                                {{
                                    switch(connection.nodeField.Value)
                                    {{
{type.Fields.Where(f => f.accessType == "inputOutput").Select(f => $@"                                    case ""{f.name}"":
                                        {{
                                            var castedField=(protoValue.field as {f.type.X3DFieldName});
                                            if(castedField==null)
                                            {{
                                                throw new InvalidOperationException();
                                            }}
                                            {(f.type.X3DFieldName == "MFNode" ? $@"@{f.cleanName}.Clear();
                                            @{f.cleanName}.AddRange(castedField.Value);
" : $@"@{f.cleanName}=castedField.Value;")}
                                        }}
                                        break;").LineJoin()}
                                    default:
                                        break;
                                    }}
                                }}
                                }}
                            }}
                            break;" : "")} 
            {type.Statements.Where(s => !s.isArray).Select(s => $@"                        case ""{s.type.Name}"":
                            @{s.cleanName} = Parse_{s.type.Name}(childNode, context);
                            break;").LineJoin()}
            {type.Statements.Where(s => s.isArray).Select(s => $@"                        case ""{s.type.Name}"":
                            @{s.cleanName}.Add(Parse_{s.type.Name}(childNode, context));
                            break;").LineJoin()}
                        default:
                            var parsedNode = Parse(childNode, context);

                            if (parsedNode == null)
                            {{
                                continue;
                            }}
                            var sceneNode=(parsedNode as ProtoInstance)?.SceneNode ?? parsedNode;
                            var containerField=childNode.GetAttribute(""containerField"");
                            if(string.IsNullOrEmpty(containerField))
                            {{
                                containerField= sceneNode.DefaultContainerField;
                            }}
                            switch(containerField)
                            {{
                    {nodeFields.Select(f => $@"            case ""{f.name}"":
                                    {f.cleanName}{(f.type.IsArray?".Add(parsedNode)":"=parsedNode")};
                                    break;").LineJoin()}
                    {type.Statements.Where(s => !s.isArray).Select(s => $@"            case ""{s.name}"":
                                    @{s.cleanName} = Parse_{s.type.Name}(childNode, context);
                                    break;").LineJoin()}
                    {type.Statements.Where(s => s.isArray).Select(s => $@"            case ""{s.name}"":
                                    @{s.cleanName}.Add(Parse_{s.type.Name}(childNode, context));
                                    break;").LineJoin()}
                                default:                                    
                                    if(parsedNode==null)
                                    {{
                                    }}
{nodeFields.Select(f => $@"                                    else if({f.type.Name}.IsValueAccepted(sceneNode)) 
                                    {{
                                       {f.cleanName}{(f.type.IsArray ? ".Add(parsedNode)" : "=parsedNode")};
                                    }}").LineJoin()}
                                    else
                                    {{
                                        throw new InvalidOperationException();
                                    }}
                                    break;
                            }}
                
                            break;
                    }}
                }}


                var result= new {type.CleanSingleTypeName.Trim('?')}(context, {type.Fields.Where(o => !type.IsRouteOnlyField(o)).Select(f => $"@{f.cleanName}").StringJoin(", ")}{(type.Statements.Any() && type.Fields.Any() ? ", " : "")}{type.Statements.Select(s => $"@{s.cleanName}").StringJoin(", ")});

{(type is NodeBuilder ?$@"foreach(var connection in isList)
                {{
                    context.AddISConnection(result,connection);
                }}":"")}
                

                return result;
            }}
";
        }
        // internal class NodeFieldBuilder
        // {
        //     private readonly IElementBuilder nodeType;
        //     private readonly IFieldBuilder type;
        //     private readonly string name;
        //     private readonly string cleanName;
        //     private readonly string accessType;
        //     private readonly string defaultValue;
        //     private readonly INodeDataTypeBuilder nodeBuilder;
        //
        //     public NodeFieldBuilder((IFieldBuilder type, string name, string cleanName, string accessType, string defaultValue) data, IElementBuilder nodeType)
        //     {
        //         this.nodeType = nodeType;
        //         (type, name, cleanName, accessType, defaultValue) = data;
        //         //
        //         // isAddMethod = type.IsArray && Regex.IsMatch(name, "^add[A-Z]") && nodeType.Fields.Any(o => o.name == name.Substring("add".Length));
        //         // isRemoveMethod = type.IsArray && Regex.IsMatch(name, "^remove[A-Z]") && nodeType.Fields.Any(o => o.name == name.Substring("remove".Length));
        //
        //         nodeBuilder = (type.DataType as INodeDataTypeBuilder)!;
        //     }
        //
        //     public string X3DName => name;
        //
        //     public string variableName => $@"@{cleanName}";
        //
        //     public string DeclarationOfParsingVariable
        //     {
        //         get
        //         {
        //             if (type.IsArray)
        //             {
        //                 return $"List<{type.DataType.Name}> {variableName}= new List<{type.DataType.Name}>();";
        //             }
        //             else
        //             {
        //                 return $"{(type.DataType.CleanSingleTypeName)} {variableName}= {defaultValue};";
        //             }
        //         }
        //     }
        //
        //     public string ContainerFieldParsingCode(string paramName)
        //     {
        //         return $@"
        //                     {{
        //                         {assignParsedVal(paramName)}
        //                     }}";
        //     }
        //
        //     public string assignVal(string paramName)
        //     {
        //         if (type.IsArray && nodeBuilder.AcceptableNodeTypesSplit.Count > 1)
        //         {
        //             return $"{variableName}.AddRange({paramName}.Select(o => new {type.DataType.Name}(o)));";
        //         }
        //         else if (type.IsArray && nodeBuilder.AcceptableNodeTypesSplit.Count == 1)
        //         {
        //             return $"{variableName}.AddRange({paramName}.Select(o => o as {type.DataType.Name} ?? throw new InvalidOperationException()));";
        //         }
        //         else
        //         {
        //             return assignParsedVal(paramName);
        //         }
        //     }
        //
        //     public string assignParsedVal(string paramName)
        //     {
        //         if (type.IsArray && nodeBuilder.AcceptableNodeTypesSplit.Count > 1)
        //         {
        //             return @$"if({paramName}!=null) 
        //             {{ 
        //                 {variableName}.Add(new {type.DataType.Name}({paramName})); 
        //             }}";
        //         }
        //         else if (type.IsArray && nodeBuilder.AcceptableNodeTypesSplit.Count == 1)
        //         {
        //             return $"{variableName}.Add({paramName} as {type.DataType.Name} ?? throw new InvalidOperationException());";
        //         }
        //         else if (!type.IsArray && nodeBuilder.AcceptableNodeTypesSplit.Count > 1)
        //         {
        //             return $"{variableName} = {paramName} == null ? null : new {type.DataType.Name}({paramName});";
        //         }
        //         else if (!type.IsArray && nodeBuilder.AcceptableNodeTypesSplit.Count == 1)
        //         {
        //             return $"{variableName} = {paramName} == null ? null : ({paramName} as  {type.DataType.Name} ?? throw new InvalidOperationException());";
        //         }
        //         else
        //         {
        //             throw new InvalidOperationException();
        //         }
        //     }
        //
        //     public string TypeCheck(string paramName)
        //     {
        //         return (type.DataType as INodeDataTypeBuilder)!.AcceptableNodeTypesSplit.Select(t => $"{paramName} is {t}")
        //             .StringJoin(" || ");
        //     }
        //
        //     public string HandleIS(string paramName)
        //     {
        //         return $@"{{
        //                         var temp= ({type.ConvertISValueMethod(paramName)}).Value;
        //                                         {assignVal($"temp")}
        //                     }}";
        //     }
        // }
    }
}