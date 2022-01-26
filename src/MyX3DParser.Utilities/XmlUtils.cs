using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace MyX3DParser.Utils
{
    internal static class XmlUtils
    {
        public static IEnumerable<XmlElement> ChildElements(this XmlElement element)
        {
            return element.ChildNodes.OfType<XmlElement>();
        }

        public static void ValidateChildrenAreOfType(this XmlElement xmlElement, params string[] allowedTypes)
        {
            foreach (var item in xmlElement.ChildElements())
            {
                if (!allowedTypes.Contains(item.LocalName, StringComparer.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException($"Children of type {item.LocalName} are not allowed in {xmlElement.LocalName}");
                }
            }
        }

        public static XmlElement GetSingleChildOfType(this XmlElement xmlElement, string type)
        {
            return xmlElement.ChildElements().Where(o => o.LocalName == type).Single();
        }

        public static XmlElement? TryGetSingleChildOfType(this XmlElement xmlElement, string type)
        {
            using var enumerator = xmlElement.ChildElements().Where(o => o.LocalName == type).GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return null;
            }
            var result = enumerator.Current;
            if (enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }
            return result;
        }

        public static IReadOnlyCollection<XmlElement> GetChildrenOfType(this XmlElement xmlElement, string type)
        {
            return xmlElement.ChildElements().Where(o => o.LocalName == type).ToList();
        }

        public static IReadOnlyCollection<XmlElement> GetChildrenOfTypes(this XmlElement xmlElement, params string[] types)
        {
            return xmlElement.ChildElements().Where(o => types.Contains(o.LocalName)).ToList();
        }
    }
}