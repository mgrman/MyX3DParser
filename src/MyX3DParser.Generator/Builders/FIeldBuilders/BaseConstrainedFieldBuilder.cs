using System.Collections.Generic;
using System.Linq;
using MyX3DParser.Utils;

namespace MyX3DParser.Model.Builders
{
    [BuilderCategory("Fields")]
    internal class BaseConstrainedFieldBuilder 
    {
        public BaseConstrainedFieldBuilder(IFileBuilder builder, string baseName, string type, string extras, string cleanName, string constraint, string namespaces)
        {
            Builder = builder;
            CleanName = cleanName;
            Type = type;
            Extras = extras;
            Constraint = constraint;
            Namespaces = namespaces;
            BaseName = baseName;
        }

        public IFileBuilder Builder { get; }
        public string CleanName { get; }
        public string BaseName { get; }
        public string Type { get; }
        public string Extras { get; }
        public string Constraint { get; }
        public string Namespaces { get; }

        public override string ToString()
        {
            return @$"using System;
using System.Collections.Generic;
using System.Linq;
using MyX3DParser.Utils;
{Namespaces}
{BuilderHelper.Namespaces}

namespace {BuilderHelper.BaseNamespace}.{Builder.GetCategory()}
{{

    public partial class {CleanName} : {BaseName} 
    {{
        //public {CleanName}()
        //    :base()
        //{{
        //}}

        {Extras}

        public {CleanName}({Type} value)
            :base(value)
        {{
        }}
        protected override void Validate({Type} value)
        {{
            if({Constraint})
            {{
                return;
            }}

            // base.Validate(value); not called since it assumes the constraint is more restrictive than base
            throw new InvalidOperationException();
        }}
    }}
}}";
        }
    }
}