using Microsoft.VisualBasic.CompilerServices;
using MyX3DParser.Model.Builders;
using MyX3DParser.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyX3DParser.Model.Builders
{
    [BuilderCategory("DataTypes")]
    internal abstract class BaseProxyDataTypeBuilder : IDataTypeBuilder
    {
        private readonly List<(string constantName, string value, bool isArray)> constants = new List<(string constantName, string value, bool isArray)>();

        public BaseProxyDataTypeBuilder(string name,string cleanSingleTypeName, int? componentCount)
        {
            this.Name = name;

            CleanSingleTypeName = cleanSingleTypeName;
            this.componentCount = componentCount;
        }

        public string Name { get; }

        public string CleanSingleTypeName { get; }

        private readonly int? componentCount;

        public override string ToString()
        {

            var template = $@"using System;
using System.Collections.Generic;
using System.Linq;
using MyX3DParser.Utils;
{BuilderHelper.Namespaces}

namespace {BuilderHelper.BaseNamespace}.{this.GetCategory()}
{{
    public static partial class {Name}
    {{
{(constants.Select(o =>
            {
                if (o.isArray)
                {
                    return $"        public static IReadOnlyList<{CleanSingleTypeName}> @{o.constantName} {{ get; }} = {ParseArrayMethod($"\"{o.value}\"")};";
                }
                else
                {
                    return $"        public static {CleanSingleTypeName} @{o.constantName} {{ get; }} = {ParseMethod($"\"{o.value}\"")};";
                }
            })
                .LineJoin())}


        public static IReadOnlyList<{CleanSingleTypeName}> ParseArray(string value)
        {{
{(componentCount.HasValue?$@"
            if(value.Contains(','))
            {{
                return value.Split(new []{{','}}, StringSplitOptions.RemoveEmptyEntries).Select(Parse).ToList();
            }}
            else
            {{
                return value.Split(new []{{' '}}, StringSplitOptions.RemoveEmptyEntries)
                    .GroupByIndex({componentCount})
                    .Select(Parse)
                    .ToList();
            }}" : $@"            return value.Split(new []{{','}}, StringSplitOptions.RemoveEmptyEntries).Select(Parse).ToList();
            ")}
        }}

    }}
}}";

            return template;
        }

        public string GetSingleValue(string textValue)
        {
            if (textValue.IsNullOrEmpty())
            {
                throw new InvalidOperationException();
                //var constantName = $"Default";
                //if (!constants.Contains((constantName, "0", false)))
                //{
                //    constants.Add((constantName, "0", false));
                //}

                //return $"{BuilderHelper.BaseNamespace}.{this.GetCategory()}.{Name}.{constantName}";
            }
            else
            {
                var constantName = $"ConstantValue_{textValue.Split(" ").Select(n => decimal.Parse(n).ToNumberString()).StringJoin("_")}";
                if (constants.None(o => o.constantName == constantName))
                {
                    constants.Add((constantName, textValue, false));
                }

                return $"{BuilderHelper.BaseNamespace}.{this.GetCategory()}.{Name}.{constantName}";
            }
        }

        public string GetArrayValue(string textValue)
        {
            if (textValue.IsNullOrEmpty())
            {
                return $"Array.Empty<{CleanSingleTypeName}>()";
            }
            else
            {
                var constantName = $"ConstantArray_{textValue.Split(" ").Select(n => decimal.Parse(n).ToNumberString()).StringJoin("_")}";

                if (constants.None(o => o.constantName == constantName))
                {
                    constants.Add((constantName, textValue, true));
                }

                return $"{BuilderHelper.BaseNamespace}.{this.GetCategory()}.{Name}.{constantName}";
            }
        }

        public static IReadOnlyList<string> ParseMFStringValue(string value)
        {
            if (value.StartsWith("\""))
            {
                return value.Split('\"')
                    .Select((o, i) => new { o, i })
                    .Where(o => o.i % 2 == 1)
                    .Select(o => o.o)
                    .ToArray();
            }
            else
            {
                return value.Split(' ');
            }
        }

        public string ParseMethod(string o)
        {
            return  $"{BuilderHelper.BaseNamespace}.{this.GetCategory()}.{Name}.Parse({o})";
        }

        public string ParseArrayMethod(string o)
        {
            return @$"{BuilderHelper.BaseNamespace}.{this.GetCategory()}.{Name}.ParseArray({o})";
        }

        public string ToX3DString(string o) => $"{BuilderHelper.BaseNamespace}.{this.GetCategory()}.{Name}.ToX3DString({o})";

        public string ToX3DArrayString(string paramName) => $"{paramName}.Select(inner=>{ToX3DString("inner")}).StringJoin(\", \")";

        public string GreaterMethod(string paramLeft, string paramRight) => $"{BuilderHelper.BaseNamespace}.{this.GetCategory()}.{Name}.Greater({paramLeft},{paramRight})";
        public string GreaterOrEqualMethod(string paramLeft, string paramRight) => $"{BuilderHelper.BaseNamespace}.{this.GetCategory()}.{Name}.GreaterOrEqual({paramLeft},{paramRight})";
        public string LesserMethod(string paramLeft, string paramRight) => $"{BuilderHelper.BaseNamespace}.{this.GetCategory()}.{Name}.Lesser({paramLeft},{paramRight})";
        public string LesserOrEqualMethod(string paramLeft, string paramRight) => $"{BuilderHelper.BaseNamespace}.{this.GetCategory()}.{Name}.LesserOrEqual({paramLeft},{paramRight})";
    }
}