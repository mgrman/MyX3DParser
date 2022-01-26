using System;
using System.Collections.Generic;
using System.Xml;
using MyX3DParser.Generated.Model.AbstractNodes;
using MyX3DParser.Generated.Model.DataTypes;
using MyX3DParser.Generated.Model.Fields;
using MyX3DParser.Generated.Model.Nodes;
using MyX3DParser.Generated.Model.Statements;

namespace MyX3DParser.Generated.Model
{
    public class X3DContext
    {
        private readonly X3DContext? parentContext;
        private readonly Dictionary<string, X3DNode> defs = new Dictionary<string, X3DNode>();
        private readonly Dictionary<string, ProtoDeclare> protoDeclares = new Dictionary<string, ProtoDeclare>();
        private readonly Dictionary<string, ExternProtoDeclare> externProtoDeclares = new Dictionary<string, ExternProtoDeclare>();
        private readonly Dictionary<string, IReadOnlyList<XmlElement>> externProtoDeclaresImplementations = new Dictionary<string, IReadOnlyList<XmlElement>>();
        private readonly List<ProtoDeclare> protoDeclaresInOrder = new List<ProtoDeclare>();

        private readonly Dictionary<string, FieldDefinition> fieldDefinitions = new Dictionary<string, FieldDefinition>();
        private readonly List<ROUTE> routes = new List<ROUTE>();
        private readonly Dictionary<(X3DNode node, IX3DField field), List<(X3DNode node, IX3DField field)>> routesFrom = new Dictionary<(X3DNode node, IX3DField field), List<(X3DNode node, IX3DField field)>>();
        private readonly List<X3DShapeNode> shapeNodes=new List<X3DShapeNode>();

        public X3DContext()
        {
            parentContext = null;
            fieldDefinitions.Add("metadata", new FieldDefinition("initializeOnly", "metadata", new SFNode(null)));
        }

        public X3DContext(X3DContext parentContext, ProtoInstance protoInstance)
        {
            ProtoInstance = protoInstance;
            this.parentContext = parentContext;

            fieldDefinitions.Add("metadata", new FieldDefinition("initializeOnly", "metadata", new SFNode((parentContext.GetProtoField("metadata").field as SFNode)!.Value)));
        }


        internal void AddShape(X3DShapeNode shapeNode)
        {
            shapeNodes.Add(shapeNode);
            if (parentContext != null)
            {
                parentContext.AddShape(shapeNode);
            }
        }

        public IReadOnlyList<X3DShapeNode> ShapeNodes => shapeNodes;


        public ProtoInstance? ProtoInstance { get; }
        public X3DContext? ParentContext => parentContext;

        public void AddDEF(string def, X3DNode node)
        {
            defs.Add(def, node);
        }

        public X3DNode GetUSE(string use)
        {
            return defs[use];
        }

        public X3DNode? TryGetUSE(string use)
        {
            if(defs.TryGetValue(use, out var def))
            {
                return def;
            }
            return null;
        }

        public bool TryGetInitializationProtoField(string protoField, out (IX3DField field, bool isInitializeOnly) result)
        {
            if (fieldDefinitions.TryGetValue(protoField, out var data) && (data.accessType == "initializeOnly" || data.accessType == "inputOutput"))
            {
                result=(data.field, data.accessType == "initializeOnly");
                return true;
            }


            result = default;
            return false;
        }


        public IX3DField GetInputField(string fieldName)
        {
            if (fieldDefinitions.TryGetValue(fieldName, out var result) && (result.accessType == "inputOnly" || result.accessType == "inputOutput"))
            {
                return result.field;
            }

            throw new InvalidOperationException();
        }

        public IX3DField? TryGetInputField(string fieldName)
        {
            if (fieldDefinitions.TryGetValue(fieldName, out var result) && (result.accessType == "inputOnly" || result.accessType == "inputOutput"))
            {
                return result.field;
            }

            return null;
        }

        public IX3DField GetOutputField(string fieldName)
        {
            if (fieldDefinitions.TryGetValue(fieldName, out var result) && (result.accessType == "outputOnly" || result.accessType == "inputOutput"))
            {
                return result.field;
            }

            throw new InvalidOperationException();
        }

        public IX3DField? TryGetOutputField(string fieldName)
        {
            if (fieldDefinitions.TryGetValue(fieldName, out var result) && (result.accessType == "outputOnly" || result.accessType == "inputOutput"))
            {
                return result.field;
            }

            return null;
        }

        public IX3DField GetInputOutputField(string fieldName)
        {
            if (fieldDefinitions.TryGetValue(fieldName, out var result) && ( result.accessType == "inputOutput"))
            {
                return result.field;
            }

            throw new InvalidOperationException();
        }

        public IX3DField? TryGetInputOutputField(string fieldName)
        {
            if (fieldDefinitions.TryGetValue(fieldName, out var result) && (result.accessType == "inputOutput"))
            {
                return result.field;
            }

            return null;
        }

        
        
        
        
        private (IX3DField field, string accessType) GetProtoField(string protoField)
        {
            if (fieldDefinitions.TryGetValue(protoField, out var result))
            {
                return (result.field, result.accessType);
            }

            throw new InvalidOperationException($"Field with name '{protoField}' was not found!");
        }

        public void AddProtoDeclare(ProtoDeclare protoDeclare)
        {
            protoDeclares.Add(protoDeclare.name.Value, protoDeclare);
            protoDeclaresInOrder.Add(protoDeclare);
        }

        public void AddExternProtoDeclare(ExternProtoDeclare protoDeclare)
        {
            externProtoDeclares.Add(protoDeclare.name.Value, protoDeclare);
        }


        public void AddExternProtoDeclareImplementation(string name, IReadOnlyList<XmlElement> body)
        {
            externProtoDeclaresImplementations.Add(name, body);
        }


        public void AddRoute(ROUTE route)
        {
            // TODO evaluate routes
            routes.Add(route);

            var fromNode = TryGetUSE(route.fromNode.Value);
            if (fromNode == null)
            {
                if (StaticConfig.DoNotThrowErrorsOnMissingUSE)
                {
                    return;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
            var fromField = fromNode.GetOutputField(route.fromField.Value);

            var toNode = TryGetUSE(route.toNode.Value);
            if (toNode == null)
            {
                if (StaticConfig.DoNotThrowErrorsOnMissingUSE)
                {
                    return;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
            var toField = toNode.GetInputField(route.toField.Value);

            AddRouteInner(fromNode, route.fromField.Value, fromField, toNode, route.toField.Value, toField);

            toField.SetValue(fromField);
        }

        internal void ClearProtoTypes()
        {
            protoDeclares.Clear();
            protoDeclaresInOrder.Clear();
            // TODO add validation that no prototypes are using in hierarchy
        }

        /// <summary>
        /// INCLUDES inherited from parent context
        /// </summary>
        public ProtoDeclare? TryGetProtoDeclare(string name)
        {
            if (protoDeclares.TryGetValue(name, out var protoDeclare))
            {
                return protoDeclare;
            }

            if (parentContext != null)
            {
                return parentContext.TryGetProtoDeclare(name);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// INCLUDES inherited from parent context
        /// </summary>
        public (ExternProtoDeclare @interface, IReadOnlyList<XmlElement>? body)? TryGetExternProtoDeclare(string name)
        {
            if (externProtoDeclares.TryGetValue(name, out var @interface))
            {
                var body = externProtoDeclaresImplementations.TryGetValue(name, out var temp) ? temp : null;
                return (@interface, body);
            }

            if (parentContext != null)
            {
                return parentContext.TryGetExternProtoDeclare(name);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// not inherited from parent context
        /// </summary>
        public IEnumerable<ProtoDeclare> GetAllProtoDeclares()
        {
            return protoDeclaresInOrder;
        }

        /// <summary>
        /// not inherited from parent context
        /// </summary>
        public IEnumerable<ROUTE> GetAllRoutes()
        {
            return routes;
        }

        public void AddInterfaceField(string accessType, string fieldName, IX3DField value)
        {
            if (ProtoInstance == null || parentContext==null)
            {
                throw new InvalidOperationException();
            }

            fieldDefinitions.Add(fieldName, new FieldDefinition(accessType, fieldName, value));

            value.OnChange += f => OnFieldChanged(ProtoInstance, fieldName, value);
            value.OnChange += f => parentContext.OnFieldChanged(ProtoInstance, fieldName, value);
        }

        private class FieldDefinition
        {
            public FieldDefinition(string accessType, string fieldName, IX3DField field)
            {
                this.accessType = accessType;
                this.fieldName = fieldName;
                this.field = field;
            }

            public string @accessType { get; }
            public string @fieldName { get; }
            public IX3DField field { get; }
        }

        public void AddISConnection(X3DNode node, connect isConnection)
        {
            if (ProtoInstance == null)
            {
                throw new InvalidOperationException();
            }

            var protoFieldName = isConnection.protoField.Value;
            var protoValue = GetProtoField(protoFieldName);

            var nodeFieldName = isConnection.nodeField.Value;

            switch (protoValue.accessType)
            {
                case "initializeOnly":
                    // only set value in parser
                    break;
                case "inputOnly":
                    var inputField = node.TryGetInputField(nodeFieldName);
                    if (inputField != null)
                    {
                        AddRouteInner(ProtoInstance, protoFieldName, protoValue.field, node, nodeFieldName, inputField);
                    }
                    else
                    {
                        throw new InvalidOperationException();
                    }
                    break;
                case "outputOnly":
                    var outputField = node.TryGetOutputField(nodeFieldName);
                    if (outputField != null)
                    {
                        AddRouteInner(node, nodeFieldName, outputField, ProtoInstance, protoFieldName, protoValue.field);
                    }
                    else
                    {
                        throw new InvalidOperationException();
                    }
                    break;
                case "inputOutput":
                    var inputOutputField = node.TryGetInputOutputField(nodeFieldName);
                    if (inputOutputField != null)
                    {
                        AddRouteInner(ProtoInstance, protoFieldName, protoValue.field, node, nodeFieldName, inputOutputField);
                        AddRouteInner(node, nodeFieldName, inputOutputField, ProtoInstance, protoFieldName, protoValue.field);
                    }
                    else
                    {
                        throw new InvalidOperationException();
                    }
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        private void AddRouteInner(X3DNode fromNode,string fromFieldName, IX3DField fromField, X3DNode toNode, string toFieldName, IX3DField toField)
        {
            if (fromField == null)
            {
                throw new InvalidOperationException();
            }
            if (toField == null)
            {
                throw new InvalidOperationException();
            }

            if (!routesFrom.TryGetValue((fromNode, fromField), out var list))
            {
                list = new List<(X3DNode node, IX3DField field)>();
                routesFrom[(fromNode, fromField)] = list;
            }

            list.Add((toNode, toField));

            toField.SetValue(fromField);
        }

        public void OnFieldChanged(X3DNode node, string fieldName, IX3DField field)
        {
            if (routesFrom.TryGetValue((node, field), out var toRoutes))
            {
                foreach (var toRoute in toRoutes)
                {
                    toRoute.field.SetValue(field);
                }
            }

            // find IS connections
            // find ROUTE connections
        }


        public void TriggerNextFrame(float deltaTime)
        {
            if (parentContext != null)
            {

                throw new InvalidOperationException();
            }

            _currentTime += deltaTime;
            timeChanged?.Invoke(_currentTime);
        }
        public void SetTime(float time)
        {
            if (parentContext != null)
            {

                throw new InvalidOperationException();
            }

            _currentTime = time;
            timeChanged?.Invoke(_currentTime);
        }
        public void SetTimeWithoutNotification(float time)
        {
            if (parentContext != null)
            {

                throw new InvalidOperationException();
            }

            _currentTime = time;
            //timeChanged?.Invoke(_currentTime);
        }

        private float _currentTime;
        public float CurrentTime { get => parentContext?.CurrentTime ?? _currentTime; }

        private event Action<float> timeChanged;

        public event Action<float> TimeChanged {
            add
            {
                if (parentContext == null)
                {
                    timeChanged += value;
                }
                else
                {
                    parentContext.TimeChanged += value;
                }
            }
            remove
            {
                if (parentContext == null)
                {
                    timeChanged -= value;
                }
                else
                {
                    parentContext.TimeChanged -= value;
                }
            }
        }
    }
}