using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyX3DParser.Model.Builders
{
    [System.AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    sealed class BuilderCategoryAttribute : Attribute
    {
        public BuilderCategoryAttribute(string category)
        {
            this.Category = category;
        }

        public string Category { get; }

    }

    internal static class BuilderCategoryAttributeUtils
    {
        public static string GetCategory<T>(this T builder)
            where T : IFileBuilder
        {
            return builder.GetType().GetCategory();
        }

        public static string GetCategory(this Type builderType)
        {
            return builderType.GetCustomAttribute<BuilderCategoryAttribute>()!.Category;
        }

    }
}
