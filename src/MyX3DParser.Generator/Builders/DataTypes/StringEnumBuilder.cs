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
//     internal class StringEnumBuilder : IFileBuilder
//     {
//         private readonly List<string> values = new List<string>();
//
//         public IReadOnlyList<string> Values => values;
//
//         public StringEnumBuilder(string name)
//         {
//             Name = name;
//         }
//
//         public void AddValue(string value)
//         {
//             values.Add(value);
//         }
//
//         public string Name { get; }
//
//         public bool IsBounded { get; set; }
//
//         public string? AdditionalCondition { get; set; }
//
//
//         public string CleanSingleTypeName => Name;
//         public string CleanArrayTypeName => Name;
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
//         public string ToX3DString(string paramName) => $"{paramName}.InnerValue";
//
//         public override string ToString()
//         {
//             var items = values.Select(e => @$"        public static {Name} @{CleanEnumValue(e)} {{ get; }} = new {Name}(""{e}"");");
//             var definedItems = values.Select(e => @$"@{CleanEnumValue(e)}");
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
//         private readonly string __value;
//
//         public string InnerValue=> __value;
//
//         public bool IsBounded=> {(IsBounded?"true":"false")};
//
//         private {Name}(string value)
//         {{
//             this.__value = value;
//             {(AdditionalCondition==null?"": $"if(!({AdditionalCondition})) throw new InvalidOperationException();")}
//         }}
//
//         public static {Name} Parse(string value)
//         {{
//             return definedValues.{(IsBounded ? "Single" : "FirstOrDefault")}(o => o.__value == value){(IsBounded ? "" : $" ?? new {Name}(value)")};
//         }}
//
//         public static void Validate(string value)
//         {{
//             if(value==null)
//             {{
//                 throw new InvalidOperationException();
//             }}
//             {(IsBounded ? @$"var _= definedValues.Single(o => o.__value == value);" : "return;")};
//         }}
//
// {string.Join(Environment.NewLine, items)}
//
//         private static readonly IReadOnlyList<{Name}> definedValues = new []{{ {string.Join(", ", definedItems)} }};
//
//         public bool Equals({Name} that)
//         {{
//             return this.__value == that.__value;
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
//
//         public static implicit operator string({Name} value) => value.__value;
//
// {(IsBounded?"":@$"public static implicit operator {Name}(string value) => new {Name}(value);")}
//     }}
//
//     public static class {Name}Extensions
//     {{
//         public static {Name} To_{Name}(this string value)
//         {{
//             return {Name}.Parse(value);
//         }}
//     }}
// }}";
//
//
//         }
//         public static string CleanEnumValue(string value)
//         {
//             if (value.IsNullOrEmpty())
//             {
//                 return "Empty";
//             }
//
//             if (Regex.IsMatch(value, "^[^a-zA-Z_]"))
//             {
//                 value = "_" + value;
//             }
//
//             value = Regex.Replace(value, "[^a-zA-Z_0-9]", "_");
//             return value;
//         }
//
//         public string GetSingleValue(string textValue)
//         {
//             if (textValue.IsNullOrEmpty())
//             {
//                 if (IsBounded)
//                 {
//                     return $"{Name}.{CleanEnumValue(this.values.Single(o1 => o1 == textValue))}";
//                 }
//                 else
//                 {
//                     if (!values.Contains(""))
//                     {
//                         AddValue("");
//                     }
//                     return $"{Name}.Empty";
//                 }
//             }
//             else
//             {
//                 return $"{Name}.{CleanEnumValue(textValue)}";
//             }
//         }
//
//         public string GetArrayValue(string textValue)
//         {
//             throw new InvalidOperationException();
//         }
//     }
// }
