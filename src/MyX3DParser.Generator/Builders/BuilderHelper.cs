using MyX3DParser.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyX3DParser.Model.Builders
{
    internal static class BuilderHelper
    {
        public const string BaseNamespace = "MyX3DParser.Generated.Model";

        private static Lazy<string> namespacesLazy = new Lazy<string>(() =>
         {
             return typeof(BuilderHelper).Assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && typeof(IFileBuilder).IsAssignableFrom(t))
             .Select(o => $"using {BaseNamespace}.{o.GetCategory()};")
             .Distinct()
             .LineJoin();
         });

        public static string Namespaces => namespacesLazy.Value;

        public static IDataTypeBuilder GetDataTypeBuilder(this IEnumerable<IFileBuilder> builders, string name)
        {
            if (name == "Node")
            {
                name = "X3DNode";
            }

            return builders.OfType<IDataTypeBuilder>()
                .Single(o => o.Name == name);
        }
        public static X3DNodeBuilder GetX3DNodeType(this IEnumerable<IFileBuilder> builders)
        {
            return builders.OfType<X3DNodeBuilder>()
                .Single();
        }

        public static IStringFieldBuilder GetStringFieldBuilder(this IEnumerable<IFileBuilder> builders, string name)
        {
            return builders.OfType<IStringFieldBuilder>()
                .Single(o => o.Name == name);
        }
        
        public static BCLTypeBuilder GetBCLDataTypeBuilder(this IEnumerable<IFileBuilder> builders, string name)
        {
            return builders.OfType<BCLTypeBuilder>().Single(o => o.Name == name);
        }

        public static AbstractNodeBuilder GetAbstractType(this IEnumerable<IFileBuilder> builders, string name)
        {
            return builders.OfType<AbstractNodeBuilder>()
                .Single(o => o.Name == name);
        }

        public static AbstractNodeBuilder? TryGetAbstractType(this IEnumerable<IFileBuilder> builders, string name)
        {
            return builders.OfType<AbstractNodeBuilder>()
                .SingleOrDefault(o => o.Name == name);
        }

        public static NodeBuilder GetConcreteNodeBuilder(this IEnumerable<IFileBuilder> builders, string name)
        {
            if (name == "Node")
            {
                name = "X3DNode";
            }
            
            
            return builders.OfType<NodeBuilder>()
                .Single(o => o.Name == name);
        }

        public static IReadOnlyList<INodeTypeBuilder> GetNodeBuilders(this IEnumerable<IFileBuilder> builders, string name)
        {
            var conditions = name.Split("|");
            
            return builders.OfType<INodeTypeBuilder>()
                .Where(o => conditions.Any(c=> o.Name == c))
                .ToList();
        }

        public static bool TryGetStatement(this IEnumerable<IFileBuilder> builders, string name, out StatementBuilder builder)
        {
            return builders.OfType<StatementBuilder>().TryGetBy(o => o.Name == name, out builder);
        }

        public static StatementBuilder GetStatement(this IEnumerable<IFileBuilder> builders, string name)
        {
            var builder= builders.OfType<StatementBuilder>().Single(o => o.Name == name);
            return builder;
        }

        // public static INodeFieldBuilder GetNodeFieldType(this IEnumerable<IFileBuilder> builders, string nodeTypeConstraint)
        // {
        //     return builders.OfType<INodeFieldBuilder>().Single(o => o.AcceptableNodeTypes == nodeTypeConstraint);
        // }

        public static IFieldBuilder GetField(this IEnumerable<IFileBuilder> builders, string fieldType)
        {
            return builders.OfType<IFieldBuilder>().Single(o => o.Name == fieldType);
        }
        public static BaseNodeFieldBuilder GetNodeFieldForType(this IEnumerable<IFileBuilder> builders, INodeTypeBuilder type, bool isArray)
        {
            return builders.OfType<BaseNodeFieldBuilder>().Where(o=>o.IsArray== isArray).Single(o => o.DataTypes.Count==1 && o.DataTypes[0]== type);
        }

        public static IFieldBuilder GetField(this IEnumerable<IFileBuilder> builders, string fieldType, string? fieldAcceptableNodeTypes, string? fieldSimpleType, string? fieldBaseType)
        {
            var compatibleFields = builders.OfType<IFieldBuilder>().Where(o => o.X3DFieldName == fieldType).ToList();

            if (fieldAcceptableNodeTypes == null && fieldSimpleType == null && fieldBaseType == null)
            {
                return compatibleFields.Single(o => o.Name == fieldType);
            }

            if (fieldType == "SFString" && fieldBaseType != null && fieldBaseType.StartsWith("xs:"))
            {
                return compatibleFields.OfType<SFStringRegexBuilder>().Single(o => o.RegexType.Name == fieldBaseType);
            }

            if (fieldAcceptableNodeTypes == null && fieldSimpleType == null && fieldBaseType != null)
            {
                return compatibleFields.Single(o => (o is IStringFieldBuilder aaa && aaa.Name == fieldBaseType) || o.Name == fieldBaseType);
            }

            if (fieldAcceptableNodeTypes == null && fieldSimpleType != null)
            {
                return compatibleFields.Single(o => (o is IStringFieldBuilder aaa && aaa.Name == fieldSimpleType) || o.Name == fieldSimpleType);
            }

            if (fieldAcceptableNodeTypes != null && fieldSimpleType == null && fieldBaseType == null)
            {
                var types = builders.GetNodeBuilders(fieldAcceptableNodeTypes);
                return compatibleFields.OfType<INodeFieldBuilder>()
                    .Single(o => o.DataTypes.IsSetEqual(types));
            }

            throw new NotImplementedException();
            //return builders.OfType<IFieldBuilder>().Single(o => o.X3DFieldType == fieldType && o.AcceptableTypesConstraint== fieldAcceptableNodeTypes && o.SimpleTypeConstraint== fieldSimpleType && o.BaseTypeConstraint==fieldBaseType);
        }

        public static SFFieldBuilder GetSFStringField(this IEnumerable<IFileBuilder> builders, string fieldType)
        {
            var sfField = builders.GetField(fieldType) as SFFieldBuilder;
            if (sfField == null)
            {
                throw new InvalidOperationException();
            }

            return sfField;
        }

        public static MFFieldBuilder GetMFStringField(this IEnumerable<IFileBuilder> builders, string fieldType)
        {
            var mfField = builders.GetField(fieldType) as MFFieldBuilder;
            if (mfField == null)
            {
                throw new InvalidOperationException();
            }

            return mfField;
        }
    }
}
