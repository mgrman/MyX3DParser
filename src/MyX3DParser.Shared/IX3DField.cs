using System;
using System.Collections.Generic;
using MyX3DParser.Generated.Model.AbstractNodes;
using MyX3DParser.Generated.Model.Fields;

namespace MyX3DParser.Generated.Model
{
    public enum AccessTypes
    {
        InitializeOnly,
        InputOnly,
        OutputOnly,
        InputOutput
    }

    public interface IX3DField
    {
        string X3DName { get; }

        void SetValue(IX3DField sourceField);

        event Action<IX3DField> OnChange;
    }
}