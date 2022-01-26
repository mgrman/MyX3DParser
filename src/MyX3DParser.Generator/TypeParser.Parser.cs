using MyX3DParser.Model.Builders;
using MyX3DParser.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MyX3DParser.Model
{
    public static partial class TypeParser
    {
        private static void GenerateParser(X3dUnifiedObjectModel model, List<IFileBuilder> builders)
        {
            var methods = new List<(string content, string name)>();

            var directParsableStatements = new[]
            {
                "EXPORT", "IMPORT", //prototyping using whole X3D document -> needs to be added to context
                "ExternProtoDeclare", "ProtoDeclare", "ProtoInstance", // prototype definition - > needs to be added to context, all but protoBody can be parsed directly, ProtoBody needs to remain as XML
                "ROUTE" //Route handling -> needs to be added to context
            };

            var genericParseMethod = @$"

public static X3DNode? Parse(XmlElement node, X3DContext context)
{{
    switch(node.LocalName)
    {{
{builders.OfType<NodeBuilder>().Select(f => $@"        case ""{f.Name}"":
            return {(ParserMethod_ElementBuilder.customWrittenParser.Contains(f.Name) ? $@"Parser.Parse_{f.Name}(node, context)" : $@"Parse_{f.Name}(node, context)")};
").LineJoin()}
        default:
            if(TryParseStatement(node, context, out var result))
            {{
                return result;
            }}
            else if(!StaticConfig.DoNotThrowErrorsOnUnknownNodes)
            {{
                throw new InvalidOperationException($""Unknown node type '{{node.LocalName}}'"");
            }}
            else 
            {{
                return null;
            }}
    }}
}}";
            methods.Add((genericParseMethod, "genericParseMethod"));

            var handleStatementPartial = @$"

public static bool TryParseStatement(XmlElement node, X3DContext context,out X3DNode? result)
{{
    switch(node.LocalName)
    {{
{builders.OfType<StatementBuilder>().Where(type => type.Name.IsContainedIn(directParsableStatements)).Select(f => $@"        case ""{f.Name}"":
            {(ParserMethod_ElementBuilder.customWrittenParser.Contains(f.Name) ? $@"Parser.Parse_{f.Name}(node, context)" : $@"Parse_{f.Name}(node, context)")};
            result=null;
            return true;
").LineJoin()}
        default:
            result=null;
            return false;
    }}
}}";
            methods.Add((handleStatementPartial,"handleStatementPartial"));

            var parseField = @$"
public static IX3DField ParseField(string fieldType, string stringValue, IReadOnlyList<X3DNode> children)
{{
    switch(fieldType)
    {{
{builders.OfType<IFieldBuilder>().Where(f => f.Name == f.X3DFieldName).Select(f => {
                string assignVal;
                if (f is INodeFieldBuilder && f.IsArray)
                {
                    assignVal = $"children";
                }
                else if (f is INodeFieldBuilder && !f.IsArray)
                {
                    assignVal = $"children.SingleOrDefault()";
                }
                else if (f is IStringFieldBuilder sf)
                {
                    assignVal = $"{f.X3DFieldName}.Parse(stringValue)";
                }
                else {
                    throw new InvalidOperationException();
                    
                }

                return $@"        case ""{f.Name}"":
            return new {f.Name}({assignVal});
"; }).LineJoin()}
        default:
            throw new InvalidOperationException();
    }}
}}";
            methods.Add((parseField,"parseField"));

            foreach (var type in builders.OfType<IElementBuilder>())
            {
                var typeBuilder = new ParserMethod_ElementBuilder(type);
                var method = typeBuilder.ToParserMethod();
                if (method == null)
                {
                    continue;
                }

                methods.Add((method, type.CleanName));
            }


            foreach (var method in methods)
            {
                var rawBuilder = new ParsingBuilder($"Parser_{method.name}",
                    $@"using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using MyX3DParser.Utils;
{BuilderHelper.Namespaces}

namespace {BuilderHelper.BaseNamespace}.{typeof(ParsingBuilder).GetCategory()}
{{
    public  static partial class Parser
    {{" + method.content + @$"
    }}
}}");
                builders.Add(rawBuilder);
            }

        }
    }
}