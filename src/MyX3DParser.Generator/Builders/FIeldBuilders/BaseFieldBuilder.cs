using System.Collections.Generic;
using System.Linq;
using MyX3DParser.Utils;

namespace MyX3DParser.Model.Builders
{
    [BuilderCategory("Fields")]
    internal class BaseFieldBuilder
    {
        public BaseFieldBuilder(IFileBuilder builder, string cleanName, string type, string extras,  string toX3DString)
        {
            Builder = builder;
            CleanName = cleanName;
            Type = type;
            Extras = extras;
            ToX3DString = toX3DString;
        }

        public IFileBuilder Builder { get; }
        public string CleanName { get; }
        public string Type { get; }
        public string Extras { get; }

        public string ToX3DString { get; }

        public override string ToString()
        {
            return @$"using System;
using System.Collections.Generic;
using System.Linq;
using MyX3DParser.Utils;
{BuilderHelper.Namespaces}

namespace {BuilderHelper.BaseNamespace}.{Builder.GetCategory()}
{{
    public partial class {CleanName} : IX3DField
    {{
        string IX3DField.X3DName => ""{CleanName}"";

        private {Type} _value;

        //public {CleanName}()
        //{{
        //    _value = {(CleanName=="MFNode"?"Array.Empty<X3DNode>()":"default")};
        //}}

        public {CleanName}({Type} value)
        {{
            Validate(value);
            _value = value;
        }}

        {Extras}

        public AccessTypes AccessType=> AccessTypes.InputOutput;

        public {Type} Value 
        {{
            get
            {{
                return _value;
            }}
            set
            {{
                if(_value==value)
                {{
                    return;
                }}
                Validate(value);
                _value = value;
                OnChange?.Invoke(this);
                OnValueChange?.Invoke(_value);
            }}
        }}

        public void SetValue(IX3DField sourceField)
        {{
            if(sourceField is {CleanName} castedField)
            {{
                Value=castedField.Value;
            }}
            else
            {{
                throw new InvalidOperationException();
            }}
        }}
        
        public event Action<IX3DField>? OnChange;
        
        public event Action<{Type}>? OnValueChange;

        protected virtual void Validate({Type} value)
        {{
            
        }}

        public string ToX3DString(string containerField,HashSet<X3DNode> alreadySerializedNodes)
        {{
            return {ToX3DString};
        }}
    }}
}}";
        }
    }
}