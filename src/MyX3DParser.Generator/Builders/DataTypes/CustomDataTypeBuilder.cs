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
    internal class CustomDataTypeBuilder : IDataTypeBuilder
    {
        // TODO add constants also for array values
        
        
        static IReadOnlyDictionary<string, (string type, string[]? items)> DataTypesConfigs = new Dictionary<string, (string type, string[]? items)>()
        {
            {"Color", ("float", new[] {"R", "G", "B"})},
            {"ColorRGBA", ("float", new[] {"R", "G", "B", "A"})},
            {"Matrix3d", ("double", new[] {"M00", "M01", "M02", "M10", "M11", "M12", "M20", "M21", "M22"})},
            {"Matrix3f", ("float", new[] {"M00", "M01", "M02", "M10", "M11", "M12", "M20", "M21", "M22"})},
            {"Matrix4d", ("double", new[] {"M00", "M01", "M02", "M03", "M10", "M11", "M12", "M13", "M20", "M21", "M22", "M23", "M30", "M31", "M32", "M33"})},
            {"Matrix4f", ("float", new[] {"M00", "M01", "M02", "M03", "M10", "M11", "M12", "M13", "M20", "M21", "M22", "M23", "M30", "M31", "M32", "M33"})},
            {"Rotation", ("float", new[] {"X", "Y", "Z", "Angle"})},
            {"Vec2d", ("double", new[] {"X", "Y"})},
            {"Vec2f", ("float", new[] {"X", "Y"})},
            {"Vec3d", ("double", new[] {"X", "Y", "Z"})},
            {"Vec3f", ("float", new[] {"X", "Y", "Z"})},
            {"Vec4d", ("double", new[] {"X", "Y", "Z", "W"})},
            {"Vec4f", ("float", new[] {"X", "Y", "Z", "W"})},
            {"Image", ("int", null)}
        };

        public string Name { get; }

        public string CleanSingleTypeName => $"{BuilderHelper.BaseNamespace}.{this.GetCategory()}." + Name;
        private readonly List<(string constantName, string value, bool isArray)> constants = new List<(string constantName, string value, bool isArray)>();

        public CustomDataTypeBuilder(string name)
        {
            this.Name = name;

            var config = DataTypesConfigs[name];

            InnerType = config.type;
            Items = config.items;

            switch (InnerType)
            {
                case "float":
                    Parse = o => $"float.Parse({o}, System.Globalization.CultureInfo.InvariantCulture)";
                    break;
                case "double":
                    Parse = o => $"double.Parse({o}, System.Globalization.CultureInfo.InvariantCulture)";
                    break;
                case "byte":
                    Parse = o => $"byte.Parse({o}, System.Globalization.CultureInfo.InvariantCulture)";
                    break;
                case "int":
                    Parse = o => $"int.Parse({o}, System.Globalization.CultureInfo.InvariantCulture)";
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        public string InnerType { get; }

        public IReadOnlyList<string>? Items { get; }
        public Func<string, string> Parse { get; }

        public string ParseMethod(string paramName)
        {
            return $"{BuilderHelper.BaseNamespace}.{this.GetCategory()}.{Name}.Parse({paramName})";
        }

        public string ParseArrayMethod(string paramName)
        {
            return $"{BuilderHelper.BaseNamespace}.{this.GetCategory()}.{Name}.ParseArray({paramName})";
        }

        public string ToX3DString(string paramName) => $"{paramName}.ToX3DString()";

        public string ToStringAsArray()
        {
            if (Items != null)
            {
                throw new InvalidOperationException();
            }

            var template = $@"using System;
using System.Collections.Generic;
using System.Linq;
{BuilderHelper.Namespaces}

namespace {BuilderHelper.BaseNamespace}.{this.GetCategory()}
{{
    public partial struct {Name} : IEquatable<{Name}>
    {{
        public {Name}(IReadOnlyList<{InnerType}> value)
        {{
            this.Value = value;
        }}

        public {Name}(params {InnerType}[] value)
        {{
            this.Value = value;
        }}

        public static {Name} Parse(string value)
        {{
            return new {Name}(value.Split(new []{{' '}}, StringSplitOptions.RemoveEmptyEntries).Select(o=>{Parse("o")}).ToList());
        }}

        public static IReadOnlyList<{Name}> ParseArray(string value)
        {{
            return value.Split(new []{{','}}, StringSplitOptions.RemoveEmptyEntries).Select(Parse).ToList();
        }}

        public IReadOnlyList<{InnerType}> Value {{ get; }}

{GetConstants()}

        public override bool Equals(object? obj)
        {{
            return obj is {Name} that && Equals(that);
        }}

        public bool Equals({Name} other)
        {{
            return Value.SequenceEqual(other.Value);
        }}

        public override int GetHashCode()
        {{
            return -1937169414 + Value.Aggregate(0, (a, b) =>
            {{
                unchecked
                {{
                    return a + b * 17;
                }}
            }});
        }}

        public static bool operator ==({Name} left, {Name} right)
        {{
            return left.Equals(right);
        }}

        public static bool operator !=({Name} left, {Name} right)
        {{
            return !(left == right);
        }}

        public string ToX3DString()
        {{
            return ToString();
        }}

        public override string ToString()
        {{
            return string.Join("" "",Value.Select(o=>o.ToString(System.Globalization.CultureInfo.InvariantCulture)));
        }}
    }}
}}";

            return template;
        }

        public string ToStringWithItems()
        {
            if (Items == null)
            {
                throw new InvalidOperationException();
            }

            var template = $@"using System;
using System.Collections.Generic;
using System.Linq;
using MyX3DParser.Utils;
{BuilderHelper.Namespaces}

namespace {BuilderHelper.BaseNamespace}.{this.GetCategory()}
{{
    public partial struct {Name} : IEquatable<{Name}>
    {{
        public {Name}({Items.Select(o => $"{InnerType} {o.ToLower()}").StringJoin(", ")})
        {{
{Items.Select(o => $"            this.{o} = {o.ToLower()};").StringJoin(Environment.NewLine)}
        }}
        public {Name}({InnerType} value)
        {{
{Items.Select(o => $"            this.{o} = value;").StringJoin(Environment.NewLine)}
        }}
{Items.Select(o => $"        public {InnerType} {o} {{ get; }}").StringJoin(Environment.NewLine)}

        public static {Name} Parse(string value)
        {{
            var splitValues=value.Split(new []{{' '}}, StringSplitOptions.RemoveEmptyEntries);
            return Parse(splitValues);
        }}

        public static {Name} Parse(IEnumerable<string> value)
        {{
            var enumerator=value.GetEnumerator();
            if(!enumerator.MoveNext())
            {{
                return new {Name}();
            }}
            var {Items[0].ToLower()}={Parse("enumerator.Current")};
            if(!enumerator.MoveNext())
            {{
                return new {Name}({Items[0].ToLower()});
            }}
{Items.Skip(1).Select(o => @$"            var {o.ToLower()}={Parse("enumerator.Current")};
            if({(o == Items.Last() ? "" : "!")}enumerator.MoveNext())
            {{
                throw new InvalidOperationException();
            }}").LineJoin()}

            return new {Name}({Items.Select(o => o.ToLower()).StringJoin(", ")});
        }}

        public static IReadOnlyList<{Name}> ParseArray(string value)
        {{
            if(value.Contains(','))
            {{
                return value.Split(new []{{','}}, StringSplitOptions.RemoveEmptyEntries).Select(Parse).ToList();
            }}
            else
            {{
                return value.Split(new []{{' '}}, StringSplitOptions.RemoveEmptyEntries)
                    .GroupByIndex({Items.Count})
                    .Select(Parse)
                    .ToList();
            }}
        }}

{GetConstants()}


        public override bool Equals(object? obj)
        {{
            return obj is {Name} that && Equals(that);
        }}

        public bool Equals({Name} other)
        {{
            return {Items.Select(o => $"{o} == other.{o}").StringJoin(" && ")};
        }}

        public override int GetHashCode()
        {{
            int hashCode = -307843816;
{Items.Select(o => $"            hashCode = hashCode * -1521134295 + {o}.GetHashCode();").StringJoin(Environment.NewLine)}
            return hashCode;
        }}

        public static bool operator ==({Name} left, {Name} right)
        {{
            return left.Equals(right);
        }}

        public static bool operator !=({Name} left, {Name} right)
        {{
            return !(left == right);
        }}

        public static bool operator >({Name} left, {Name} right)
        {{
            return {Items.Select(o => $"left.{o} > right.{o}").StringJoin(" && ")};
        }}

        public static bool operator <({Name} left, {Name} right)
        {{
            return {Items.Select(o => $"left.{o} < right.{o}").StringJoin(" && ")};
        }}

        public static bool operator >=({Name} left, {Name} right)
        {{
            return {Items.Select(o => $"left.{o} >= right.{o}").StringJoin(" && ")};
        }}

        public static bool operator <=({Name} left, {Name} right)
        {{
            return {Items.Select(o => $"left.{o} <= right.{o}").StringJoin(" && ")};
        }}

        public string ToX3DString()
        {{
            return ToString();
        }}

        public override string ToString()
        {{
            return $""{Items.Select(o => "{" + o + ".ToString(System.Globalization.CultureInfo.InvariantCulture)}").StringJoin(" ")}"";
        }}
    }}
}}";

            return template;
        }

        public override string ToString()
        {
            if (Items == null)
            {
                return ToStringAsArray();
            }
            else
            {
                return ToStringWithItems();
            }
        }

        private string GetConstants()
        {
            return constants.Select(o =>
                {
                    if (o.isArray)
                    {
                        return $"        public static IReadOnlyList<{Name}> @{o.constantName} {{ get; }} = new []{{{GetInvocations(o.value)}}};";
                    }
                    else
                    {
                        return $"        public static {Name} @{o.constantName} {{ get; }} = new {Name}({GetValues(o.value).StringJoin(", ")});";
                    }
                })
                .LineJoin();
        }

        private string[] GetValues(string constantValue)
        {
            return constantValue.Split(" ")
                .Select(o =>
                {
                    switch (InnerType)
                    {
                        case "float":
                            return $"{o}f";
                        case "double":
                            return $"{o}d";
                        case "byte":
                            return $"{o}";
                        case "int":
                            return $"{o}";
                        default:
                            throw new InvalidOperationException();
                    }
                })
                .ToArray();
        }

        private string GetInvocations(string constantValue)
        {
            if (Items == null)
            {
                return $"new {Name}({GetValues(constantValue).Select(o1 => o1).StringJoin(", ")})";
            }
            else
            {
                return GetValues(constantValue)
                    .Select((o, i) => (o, i))
                    .GroupBy(o => o.i / Items.Count)
                    .Select(o => $"new {Name}({o.Select(o1 => o1.o).StringJoin(", ")})")
                    .StringJoin(", ");
            }
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
                if (constants.None(o=>o.constantName==constantName))
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
                return $"Array.Empty<{BuilderHelper.BaseNamespace}.{this.GetCategory()}.{Name}>()";
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
    }
}