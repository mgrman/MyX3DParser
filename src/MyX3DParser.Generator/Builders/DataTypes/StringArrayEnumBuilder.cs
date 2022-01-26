// using MyX3DParser.Utils;
// using System;
// using System.Collections.Generic;
// using System.Diagnostics.Contracts;
// using System.Linq;
// using System.Text;
// using System.Text.RegularExpressions;
// using System.Threading.Tasks;
//
// namespace MyX3DParser.Model.Builders
// {
//     [BuilderCategory("DataTypes")]
//     internal class StringArrayEnumBuilder :IFileBuilder
//     {
//         private readonly List<(string cleanName, IReadOnlyList<string> value)> values = new List<(string cleanName, IReadOnlyList<string> value)>();
//
//         public IReadOnlyList<(string cleanName, IReadOnlyList<string> value)> Values => values;
//
//         public StringArrayEnumBuilder(string name)
//         {
//             Name = name;
//         }
//
//         public void AddValue(string value)
//         {
//             var parsedValues = BCLTypeBuilder.ParseMFStringValue(value);
//
//             if (values.Any(v => v.value.SequenceEqual(parsedValues)))
//             {
//                 return;
//             }
//
//
//             values.Add((parsedValues.Select(e1 => StringEnumBuilder.CleanEnumValue(e1)).StringJoin("_").PadLeft(1, '_'), parsedValues));
//         }
//
//         public string Name { get; }
//         public bool IsBounded { get; set; }
//
//         public string CleanSingleTypeName => Name;
//         public string CleanArrayTypeName => throw new InvalidOperationException();
//
//
//         public string ParseMethod(string paramName)
//         {
//             return $"{Name}.Parse({paramName})";
//         }
//
//         public string ParseArrayMethod(string paramName)
//         {
//             throw new InvalidOperationException();
//         }
//
//
//         public override string ToString()
//         {
//
//
//             var rawCtor = (Func<IReadOnlyList<string>, string>)(values =>
//             {
//                 var items = string.Join(", ", values.Select(val => $"\"{val}\""));
//                 return $"new {typeof(string).Name}[]{{{items}}}";
//             });
//
//
//             var items = values.Select(e => @$"        public static {Name} @{e.cleanName} {{ get; }} = new {Name}({ rawCtor(e.value)});");
//
//
//             var definedItems = values.Select(e => @$"@{e.cleanName}");
//
//
//             return @$"using System;
// using System.Collections.Generic;
// using System.Linq;
// {BuilderHelper.Namespaces}
//
// namespace {BuilderHelper.BaseNamespace}.{this.GetCategory()}
// {{
//     public class {Name} : IEquatable<{Name}>
//     {{
//         private readonly IReadOnlyList<string> __value;
//
//         public IReadOnlyList<string> InnerValue=> __value;
//
//         public bool IsBounded => {(IsBounded ? "true" : "false")};
//
//         private {Name}(IReadOnlyList<string> value)
//         {{
//             this.__value = value;
//         }}
//
//         public static {Name} Parse(string value)
//         {{
//              var values = ParseMFStringValue(value);
//             return Parse(values);
//         }}
//
//         public static {Name} Parse(IReadOnlyList<string> values)
//         {{
//             return definedValues.{(IsBounded ? "Single" : "FirstOrDefault")}(o => o.__value.SequenceEqual(values)){(IsBounded ? "" : $" ?? new {Name}(values)")};
//         }}
//
//         public static void Validate(IReadOnlyList<string> values)
//         {{
//             if(values==null)
//             {{
//                 throw new InvalidOperationException();
//             }}
//             {(IsBounded ? @$"var _= definedValues.Single(o => o.__value.SequenceEqual(values));" : "return;")};
//         }}
//
//         private static IReadOnlyList<string> ParseMFStringValue(string value)
//         {{
//             if (value.StartsWith(""\""""))
//             {{
//                 return value.Split('\""').Select((o, i) => new {{ o, i }}).Where(o => o.i % 2 == 1).Select(o => o.o).ToArray();
//             }}
//             else
//             {{
//                 return value.Split(' ');
//             }}
//         }}
//
//         public static Func<IReadOnlyList<string>,bool> Validator{{get;}}= values=> definedValues.Any(o => o.__value.SequenceEqual(values));
//
// {string.Join(Environment.NewLine, items)}
//
//         private static readonly IReadOnlyList<{Name}> definedValues = new {Name}[]{{ {string.Join(", ", definedItems)} }};
//
//         public bool Equals({Name} that)
//         {{
//             return this.__value.SequenceEqual(that.__value);
//         }}
//
//         public override bool Equals(object obj)
//         {{
//             var that = obj as {Name};
//             if (object.ReferenceEquals(that, null))
//             {{
//                 return false;
//             }}
//             return Equals(that);
//         }}
//
//         public override int GetHashCode()
//         {{
//             return -1584136870 + __value.GetHashCode();
//         }}
//
//         public static bool operator ==({Name}? left, {Name}? right)
//         {{
//             if (object.ReferenceEquals(left, null) && object.ReferenceEquals(right, null))
//             {{
//                 return true;
//             }}
//             if (object.ReferenceEquals(left, null) || object.ReferenceEquals(right, null))
//             {{
//                 return false;
//             }}
//             return left.Equals(right);
//         }}
//
//         public static bool operator !=({Name}? left, {Name}? right)
//         {{
//             return !(left == right);
//         }}
//     }}
//
//     public static class {Name}Extensions
//     {{
//         public static {Name} To_{Name}(this IReadOnlyList<string> value)
//         {{
//             return {Name}.Parse(value);
//         }}
//     }}
// }}";
//
//
//
//         }
//
//         public string GetSingleValue(string textValue)
//         {
//             if (textValue.IsNullOrEmpty())
//             {
//                 if (IsBounded)
//                 {
//                     return $"{Name}.{this.Values.First().cleanName}";
//                 }
//                 else
//                 {
//                     if (!Values.Any(o => !o.value.Any()))
//                     {
//                         AddValue("");
//                     }
//                     return $"{Name}.Empty";
//                 }
//
//             }
//
//             var values = BCLTypeBuilder.ParseMFStringValue(textValue);
//
//             var existingValue = this.Values.Any(v => v.value.SequenceEqual(values));
//
//             if (existingValue)
//             {
//                 return $"{Name}.{this.Values.FirstOrDefault(v => v.value.SequenceEqual(values)).cleanName}";
//             }
//             else
//             {
//                 return $"{Name}.Parse(new string[]{{{values.WrapInQuotes().StringJoin(", ")}}})";
//             }
//
//         }
//
//         public string GetArrayValue(string textValue)
//         {
//             throw new InvalidOperationException();
//         }
//
//         public string ToX3DString(string paramName) => $"{paramName}.InnerValue";
//
//         public string ToX3DArrayString(string paramName) => throw new InvalidOperationException();
//
//     }
// }
