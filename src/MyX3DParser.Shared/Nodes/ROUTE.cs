
using System;
using System.Collections.Generic;
using System.Text;
using MyX3DParser.Generated.Model.AbstractNodes;

namespace MyX3DParser.Generated.Model.Statements
{
    public partial class ROUTE
    {

        partial void Initialize()
        {
            ParentContext.AddRoute(this);
        }
    }
}