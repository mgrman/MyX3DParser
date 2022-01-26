using MyX3DParser.Model.Builders;
using MyX3DParser.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MyX3DParser.Model.Builders
{
    internal abstract class ElementBuilder : IElementBuilder
    {
        private readonly List<(IFieldBuilder type, string name, string cleanName, string accessType, string defaultValue)> fields = new List<(IFieldBuilder type, string name, string cleanName, string accessType, string defaultValue)>();
        private readonly List<(StatementBuilder type, bool isArray, string name, string cleanName)> statements = new List<(StatementBuilder type, bool isArray, string name, string cleanName)>();

        public IReadOnlyList<(IFieldBuilder type, string name, string cleanName, string accessType, string defaultValue)> Fields =>
            fields.Select(o => (o.type, o.name, o.cleanName, o.accessType, o.defaultValue))
                .ToList();

        public IReadOnlyList<(StatementBuilder type, bool isArray, string name, string cleanName)> Statements => statements;

        public string Name { get; }
        public string CleanName => Name.CleanFileName();
        public IReadOnlyList<AbstractNodeBuilder> Interfaces { get; }
        public string? DefaultContainerField { get; }

        public string CleanSingleTypeName => $"{BuilderHelper.BaseNamespace}.{this.GetCategory()}.{Name}?";

        public string CleanArrayTypeName => $"IReadOnlyList<{BuilderHelper.BaseNamespace}.{this.GetCategory()}.{Name}>";

        public ElementBuilder(string name, IReadOnlyList<AbstractNodeBuilder> interfaces, string? defaultContainerField)
        {
            this.Name = name;
            this.Interfaces = interfaces;
            DefaultContainerField = defaultContainerField;
        }

        public void AddStatementField(StatementBuilder statementBuilder, bool isArray, string name, string defaultValue)
        {
            if (defaultValue.IsNotNullOrEmpty() && defaultValue != "NULL")
            {
                throw new InvalidOperationException();
            }

            statements.Add((statementBuilder, isArray, name, CleanPropName(name)));
        }

        public string CleanPropName(string name)
        {
            if (name.StartsWith("set_"))
            {
                return $"set{char.ToUpper(name[4])}{name.Substring(5)}";
            }

            return name.Replace("-", "_");
        }

        public void AddField(IFieldBuilder fieldBuilder, string name, string defaultValue, string accessType)
        {
            fields.Add((fieldBuilder, name, CleanPropName(name), accessType, fieldBuilder.GetValue(defaultValue)));
        }

        public bool IsRouteOnlyField((IFieldBuilder type, string name, string cleanName, string accessType, string defaultValue) f)
        {
            return f.accessType == "outputOnly" || f.accessType == "inputOnly";
        }

        public override string ToString()
        {
            var ctorParams = (new[] {"X3DContext parentContext"}).Concat(fields.Where(f => !IsRouteOnlyField(f))
                    .Select(o => $"{(o.type.BuilderDataType)} @{o.cleanName}"))
                .Concat(statements.Select(s => $"{(s.isArray ? $"IReadOnlyList<{s.type.CleanName}>" : $"{s.type.CleanName}?")} @{s.name}"))
                .ToList();

            var ctorAssignments = (new[] {"this.ParentContext = parentContext;"}).Concat(fields.Where(f => !IsRouteOnlyField(f))
                    .Select(o => $"            this.@{o.cleanName} = new {o.type.CleanName}(@{o.cleanName});"))
                .Concat(fields.Where(f => IsRouteOnlyField(f))
                    .Select(o => $"            this.@{o.cleanName} = new {o.type.CleanName}({o.defaultValue});"))
                .Concat(statements.Select(s => $"            this.@{s.name} = @{s.name};"))
                .ToList();

            var ctorFieldChanges = fields.Select(o => $"            this.@{o.cleanName}.OnChange+={o.cleanName}Changed;");

            var changeMethods = fields.Select(o => @$"            private void {o.cleanName}Changed(IX3DField field)
            {{
                ParentContext.OnFieldChanged(this,""{o.name}"",field);
            }}
");

            var fieldsFromInterfacesThatChangeTypeInClass = Interfaces.Flatten()
                .SelectMany(o => o.Fields)
                .Distinct()
                .Except(fields.Select(f => (f.type, f.name)))
                .Select(f => (name: f.name, oldType: f.type, newType: fields.Single(o => o.name == f.name)
                    .type, baseType: Interfaces.Flatten()
                    .Single(i => i.Fields.Any(f1 => f1.name == f.name))))
                .ToList();

            //  skipped fields still need to be initialized

            // TODO
            // foreach (var a in fieldsFromInterfacesThatChangeTypeInClass)
            // {
            //     if (a.newType.BaseType != a.oldType)
            //     {
            //         throw new InvalidOperationException();
            //     }
            // }

            var cleanFields = fields.Where(f => !IsRouteOnlyField(f))
                .ToList();

            var noNodeFields = !(fields.Any(o => o.type is INodeFieldBuilder) || statements.Any());

            var fieldsWithVariants = fields.SelectMany(CreateFieldVariants)
                .ToList();

            IEnumerable<(string x3dName, string propName, bool isInputType, bool isOutputType)> CreateFieldVariants((IFieldBuilder type, string name, string cleanName, string accessType, string defaultValue) f)
            {

                switch (f.accessType)
                {
                    case "initializeOnly":
                        break;
                    case "inputOnly":
                        yield return (x3dName: f.name, propName: f.cleanName, isInputType: true, isOutputType: false);



                        if (f.name.StartsWith("set_") && !fields.Any(o => o.name == f.name.Substring("set_".Length)))
                        {
                            yield return (x3dName: f.name.Substring("set_".Length), propName: f.cleanName, isInputType: true, isOutputType: false);
                        }

                        yield break;
                    case "outputOnly":
                        yield return (x3dName: f.name, propName: f.cleanName, isInputType: false, isOutputType: true);


                        if (f.name.EndsWith("_changed") && !fields.Any(o => o.name == f.name.Substring(0,f.name.Length- "_changed".Length)))
                        {
                            yield return (x3dName:  f.name.Substring(0,f.name.Length- "_changed".Length), propName: f.cleanName, isInputType: false, isOutputType: true);
                        }

                        yield break;
                    case "inputOutput":

                        yield return (x3dName: f.name, propName: f.cleanName, isInputType: true, isOutputType: true);
                        
                        if (!fields.Any(o => o.name == ("set_" + f.name)))
                        {
                            yield return (x3dName: "set_" + f.name, propName: f.cleanName, isInputType: true, isOutputType: false);
                        }

                        if (!fields.Any(o => o.name == (f.name + "_changed")))
                        {
                            yield return (x3dName: f.name + "_changed", propName: f.cleanName, isInputType: false, isOutputType: true);
                        }

                        yield break;
                    default:
                        throw new InvalidOperationException();
                }
            }

            return $@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyX3DParser.Utils;
{BuilderHelper.Namespaces}

namespace {BuilderHelper.BaseNamespace}.{this.GetCategory()}
{{
    public partial class {Name}{(Interfaces.Any() ? " : " : "")}{Interfaces.Select(o => o.CleanName).StringJoin(", ")}
    {{

        public {Name}({ctorParams.StringJoin(", ")})
        {{
{ctorAssignments.LineJoin()}
{(IsStatement ? "" : ctorFieldChanges.LineJoin())}

            {(IsStatement ? "" : $@"
            if(!string.IsNullOrEmpty(this.DEF.Value))
            {{
                ParentContext.AddDEF(this.DEF.Value,this);
            }}
")}
                
            Initialize();
        }}

        partial void Initialize();

        public string Name=>""{Name}"";

        public string? DefaultContainerField => {(DefaultContainerField == null ? "null" : @$"""{DefaultContainerField}""")};
        
        public X3DContext ParentContext {{get;}}   

{fields.Select(f => $"         public {f.type.CleanName} @{f.cleanName} {{get;}}").LineJoin()}
{statements.Select(s => $"         public {(s.isArray ? $"IReadOnlyList<{s.type.CleanName}>" : $"{s.type.CleanName}?")} @{s.name} {{get;}}").LineJoin()}
{fieldsFromInterfacesThatChangeTypeInClass.Select(a => $"         {a.oldType.CleanName} {a.baseType.CleanName}.{a.name} => @{a.name};").LineJoin()}

{(IsStatement ? "" : changeMethods.LineJoin())}        


        public IX3DField GetInputField(string fieldName)
        {{
{(Name == "ProtoInstance" ? "return childContext.GetInputField(fieldName);" : $@"
            switch(fieldName)
            {{
{fieldsWithVariants.Where(o => o.isInputType).Select(f => @$"                case ""{f.x3dName}"": 
                    return @{f.propName};").LineJoin()}
                default:
                    throw new InvalidOperationException();
            }}")}
        }}

        public IX3DField? TryGetInputField(string fieldName)
        {{
{(Name == "ProtoInstance" ? "return childContext.TryGetInputField(fieldName);" : $@"
            switch(fieldName)
            {{
{fieldsWithVariants.Where(o => o.isInputType).Select(f => @$"                case ""{f.x3dName}"": 
                    return @{f.propName};").LineJoin()}
                default:
                    return null;
            }}")}
        }}

        public IX3DField GetOutputField(string fieldName)
        {{
{(Name == "ProtoInstance" ? "return childContext.GetOutputField(fieldName);" : $@"
            switch(fieldName)
            {{
{fieldsWithVariants.Where(o => o.isOutputType).Select(f => @$"                case ""{f.x3dName}"": 
                    return @{f.propName};").LineJoin()}
                default:
                    throw new InvalidOperationException();
            }}")}
        }}

        public IX3DField? TryGetOutputField(string fieldName)
        {{
{(Name == "ProtoInstance" ? "return childContext.TryGetOutputField(fieldName);" : $@"
            switch(fieldName)
            {{
{fieldsWithVariants.Where(o => o.isOutputType).Select(f => @$"                case ""{f.x3dName}"": 
                    return @{f.propName};").LineJoin()}
                default:
                    return null;
            }}")}
        }}

        public IX3DField GetInputOutputField(string fieldName)
        {{
{(Name == "ProtoInstance" ? "return childContext.GetInputOutputField(fieldName);" : $@"
            switch(fieldName)
            {{
{fieldsWithVariants.Where(o => o.isOutputType && o.isInputType).Select(f => @$"                case ""{f.x3dName}"": 
                    return @{f.propName};").LineJoin()}
                default:
                    throw new InvalidOperationException();
            }}")}
        }}

        public IX3DField? TryGetInputOutputField(string fieldName)
        {{
{(Name == "ProtoInstance" ? "return childContext.TryGetInputOutputField(fieldName);" : $@"
            switch(fieldName)
            {{
{fieldsWithVariants.Where(o => o.isOutputType && o.isInputType).Select(f => @$"                case ""{f.x3dName}"": 
                    return @{f.propName};").LineJoin()}
                default:
                    return null;
            }}")}
        }}

        public string ToX3DString({(IsStatement ? "" : $"string containerField,")} HashSet<X3DNode> alreadySerializedNodes)
        {{
            {(IsStatement ? "" : $@"
            if(alreadySerializedNodes.Contains(this))
            {{
                if(!string.IsNullOrEmpty(DEF.Value))
                {{
                    return $""<{Name} USE=\""{{DEF.Value}}\"" />"";
                }}
                else
                {{
                    throw new InvalidOperationException(""Node was already serialized but does not have DEF defined!"");
                }}
            }}

            alreadySerializedNodes.Add(this);")}

            var sb=new StringBuilder();
            sb.Append(""<{Name} "");

            {(IsStatement ? "" : $@"
            if(containerField!=string.Empty{(DefaultContainerField == null ? "" : $@" && containerField != ""{DefaultContainerField}""")})
            {{
                sb.Append($""containerField=\""{{containerField}}\"""");
            }}
            ")}
            
{cleanFields.Where(o => o.type is IStringFieldBuilder).Select(o => $"            if({((o.type as IStringFieldBuilder).CompareMethod($"{o.cleanName}.Value", o.defaultValue))}) sb.Append(@{o.cleanName}.ToX3DString(\"{o.name}\", alreadySerializedNodes));").LineJoin()}
            
{(noNodeFields ? @"            sb.Append(""/>"");" : $@"

            bool isEnded=false;
            void EnsureEnded()
            {{
                if(isEnded) return;
                sb.Append("">"");
                isEnded=true;
            }}

            ToX3DStringPrependNodes(alreadySerializedNodes,sb,EnsureEnded);

{cleanFields.Where(o => o.type is INodeFieldBuilder && !o.type.IsArray).Select(o => $@"            if(@{o.cleanName}.Value!=null)
            {{
                EnsureEnded();
                var node=@{o.cleanName}.Value;
                sb.Append(node.ToX3DString(IsContainerFieldRequired(node)?""{o.name}"":"""",alreadySerializedNodes));
            }}").LineJoin()}

{cleanFields.Where(o => o.type is INodeFieldBuilder && o.type.IsArray).Select(o => $@"            foreach(var node in @{o.cleanName}.Value)
            {{
                EnsureEnded();
                sb.Append(node.ToX3DString(IsContainerFieldRequired(node)?""{o.name}"":"""",alreadySerializedNodes));
            }}").LineJoin()}


{statements.Where(o => !o.isArray).Select(o => $@"            if(@{o.cleanName}!=null)
            {{
                EnsureEnded();
                sb.Append(@{o.cleanName}.ToX3DString(alreadySerializedNodes));
            }}").LineJoin()}

{statements.Where(o => o.isArray).Select(o => $@"foreach(var statement in @{o.cleanName})
            {{
                EnsureEnded();
                sb.Append(statement.ToX3DString(alreadySerializedNodes));
            }}").LineJoin()}

            ToX3DStringAppendNodes(alreadySerializedNodes,sb,EnsureEnded);

            if(isEnded)
            {{
                sb.Append(""</{Name}>"");
            }}
            else
            {{
                sb.Append(""/>"");
            }}")}
            return sb.ToString();
        }}
{(noNodeFields ? "" : $@"
        partial void ToX3DStringPrependNodes(HashSet<X3DNode> alreadySerializedNodes,StringBuilder sb,Action ensureEnded);
        partial void ToX3DStringAppendNodes(HashSet<X3DNode> alreadySerializedNodes,StringBuilder sb,Action ensureEnded);

        private bool IsContainerFieldRequired(X3DNode? node)
        {{
            node=node.GetSceneNode();

            if(node==null)
            {{
                return true;
            }}
            int counter=0;
            {cleanFields.Where(o => o.type is INodeFieldBuilder nodeBuilder).Select(o => $@"counter+=({(o.type as INodeFieldBuilder).DataTypes.Select(ant => $"node is {ant.CleanName}").StringJoin(" || ")})?1:0;").LineJoin()}

            if(counter==0) throw new InvalidOperationException();
            return counter > 1;
        }}
")}

{(!Interfaces.Flatten().Any(i=>i.Name=="X3DChildNode")?"":$@"


        private Dictionary<(X3DChildNode?,bool),IReadOnlyList<Shared.SceneNodeData>> parentPositionsMap=new Dictionary<(X3DChildNode?,bool),IReadOnlyList<Shared.SceneNodeData>>();

public IReadOnlyList<Shared.SceneNodeData> MyPositions{{get; private set;}} = Array.Empty<Shared.SceneNodeData>();

        public void OnParentPositionsChange(X3DChildNode? parent, IReadOnlyList<Shared.SceneNodeData> parentPositions)
        {{
            parentPositionsMap[(parent,true)]=parentPositions;

            UpdateMatrices();     
       }}

        public void UpdateMatrices()
        {{
            var myPositions=parentPositionsMap
                .Values
                .SelectMany(o=>o)
                .Select(UpdatePosition)
                .ToList() as IReadOnlyList<Shared.SceneNodeData>;
MyPositions=myPositions;


            foreach (var child in GetSceneChildren())
            {{
                var childPositions=myPositions;
                UpdatePositionsForChild(child,ref childPositions);
                child.OnParentPositionsChange(this,childPositions);
            }}
        }}

        private Shared.SceneNodeData UpdatePosition(Shared.SceneNodeData position)
        {{
            UpdatePositionInner(ref position);
            return position;
        }}

         partial void UpdatePositionInner(ref Shared.SceneNodeData position);


         partial void UpdatePositionsForChild(X3DChildNode child,ref IReadOnlyList<Shared.SceneNodeData> positions);

        private IEnumerable<X3DChildNode> GetSceneChildren()
        {{
            
{cleanFields.Where(o => o.type is INodeFieldBuilder)
                .Select(o =>o.type.IsArray? $@"
        foreach(var c in {o.cleanName}.Value.GetSceneNodes().OfType<X3DChildNode>())
{{
yield return c;
}}
":$@"
var {o.cleanName}_scene={o.cleanName}.Value.GetSceneNode() as X3DChildNode;
if({o.cleanName}_scene!=null)
{{
yield return {o.cleanName}_scene;
}}
").LineJoin()}
        }}

                    ")}


{(!Interfaces.Flatten().Any(i => i.Name == "X3DShapeNode") ? "" : $@"


        public Shared.Mesh Mesh {{get; private set;}}

      public  Shared.MeshMaterial Material {{get; private set;}}

                    ")}
{(!Interfaces.Flatten().Any(i => i.Name == "X3DGeometryNode") ? "" : $@"


        public Shared.Mesh Mesh {{get; private set;}}

                    ")}
{(!Interfaces.Flatten().Any(i => i.Name == "X3DAppearanceNode") ? "" : $@"


      public  Shared.MeshMaterial Material {{get; private set;}}

                    ")}

    }}
}}";
        }

        public bool IsStatement => Interfaces.Count == 0;
    }
}