using System.Collections.Generic;

namespace MyX3DParser.Model.Builders
{
    internal interface IElementBuilder : INodeTypeBuilder
    {
        string CleanSingleTypeName { get; }

        // string CleanArrayTypeName { get; }

        IReadOnlyList<(IFieldBuilder type, string name, string cleanName, string accessType, string defaultValue)> Fields { get; }

        IReadOnlyList<(StatementBuilder type, bool isArray, string name, string cleanName)> Statements { get; }

        bool IsRouteOnlyField((IFieldBuilder type, string name, string cleanName, string accessType, string defaultValue) f);
        string? DefaultContainerField { get; }
    }
}