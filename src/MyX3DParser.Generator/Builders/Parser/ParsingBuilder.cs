using MyX3DParser.Model.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyX3DParser.Model.Builders
{
    [BuilderCategory("Parsing")]
    internal class ParsingBuilder : IFileBuilder
    {
        public ParsingBuilder(string name, string content)
        {
            Name = name;
            Content = content;
        }

        public string Name { get; set; }

        public string Content { get; set; }

        public override string ToString()
        {
            return Content;
        }
    }
}
