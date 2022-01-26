using MyX3DParser.Model.Builders;
using MyX3DParser.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

namespace MyX3DParser.Model.Builders
{
    [BuilderCategory("AbstractNodes")]
    internal class AbstractNodeBuilder : INodeTypeBuilder
    {
        private readonly List<(IFieldBuilder type, string name)> fields = new List<(IFieldBuilder type, string name)>();

        public string Name { get; }
        public string CleanName => Name.CleanFileName();
        public IReadOnlyList<AbstractNodeBuilder> Interfaces { get; }
        public IReadOnlyList<(IFieldBuilder type, string name)> Fields => fields;

        public string CleanSingleTypeName => Name + "?";

        public string CleanArrayTypeName => $"IReadOnlyList<{Name}>";

        public AbstractNodeBuilder(string name, IReadOnlyList<AbstractNodeBuilder> interfaces)
        {
            this.Name = name;
            this.Interfaces = interfaces;
        }

        public void AddField(IFieldBuilder fieldBuilder, string name)
        {
            fields.Add((fieldBuilder, name));
        }

        public string CleanPropName(string name)
        {
            if (name.StartsWith("set_"))
            {
                return $"set{char.ToUpper(name[4])}{name.Substring(5)}";
            }

            return name;
        }

        public override string ToString()
        {
            var inheritedFields = Interfaces.Flatten()
                .SelectMany(o => o.fields)
                .Select(o => o.name)
                .ToHashSet();
            return $@"using System;
using System.Collections.Generic;
{BuilderHelper.Namespaces}

namespace {BuilderHelper.BaseNamespace}.{this.GetCategory()}
{{
    public interface {CleanName}{(Interfaces.Any() ? " : " : "")}{Interfaces.Select(o => o.Name).StringJoin(", ")}
    {{
{fields.Where(f => !inheritedFields.Contains(f.name)).Select(f => $"         {f.type.CleanName} @{CleanPropName(f.name)} {{get;}}").LineJoin()}

{(CleanName == "X3DNode" ? @$"
        string ToX3DString(string containerField, HashSet<X3DNode> alreadySerializedNodes);
        string Name {{get;}}
        string? DefaultContainerField {{get;}}
        IX3DField GetInputField(string fieldName);
        IX3DField? TryGetInputField(string fieldName);
        IX3DField GetOutputField(string fieldName);
        IX3DField? TryGetOutputField(string fieldName);
        IX3DField GetInputOutputField(string fieldName);
        IX3DField? TryGetInputOutputField(string fieldName);
        X3DContext ParentContext{{get;}}
" : "")}
{(CleanName == "X3DChildNode" ? @$"
        void OnParentPositionsChange(X3DChildNode? parent, IReadOnlyList<Shared.SceneNodeData> parentMatrices);
IReadOnlyList<Shared.SceneNodeData> MyPositions{{get; }} 
" : "")}
{(CleanName == "X3DShapeNode" ? @$"
        
        Shared.Mesh Mesh {{ get; }}
        Shared.MeshMaterial Material {{ get; }}
" : "")}
{(CleanName == "X3DGeometryNode" ? @$"
        
        Shared.Mesh Mesh {{ get; }}
" : "")}
{(CleanName == "X3DAppearanceNode" ? @$"
        
        Shared.MeshMaterial Material {{ get; }}
" : "")}
    }}
}}";
        }

        private IEnumerable<AbstractNodeBuilder> GetAllInterfaces(IEnumerable<AbstractNodeBuilder> interfaces)
        {
            foreach (var @interface in interfaces)
            {
                yield return @interface;

                foreach (var childInterface in GetAllInterfaces(@interface.Interfaces))
                {
                    yield return childInterface;
                }
            }
        }
    }

    internal static class AbstractNodeBuilderExtensions
    {
        public static IEnumerable<AbstractNodeBuilder> Flatten(this IEnumerable<AbstractNodeBuilder> items)
        {
            foreach (var item in items)
            {
                yield return item;

                foreach (var flatItem in Flatten(item.Interfaces))
                {
                    yield return flatItem;
                }
            }
        }
    }
}