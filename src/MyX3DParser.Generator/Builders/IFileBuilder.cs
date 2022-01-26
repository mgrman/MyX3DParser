using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyX3DParser.Model.Builders
{
    internal interface IFileBuilder
    {
        string Name { get; }
        string CleanName => Name;
    }
}
