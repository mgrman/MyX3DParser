using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace MyX3DParser.Utils
{
    internal static class StringUtils
    {
        public static bool IsNullOrEmpty([NotNullWhen(false)] this string? text)
        {
            return string.IsNullOrEmpty(text);
        }

        public static bool IsNotNullOrEmpty([NotNullWhen(true)] this string? text)
        {
            return !string.IsNullOrEmpty(text);
        }

        public static string ClipIfLonger(this string text, int limit)
        {
            return text.Length <= limit ? text : text.Substring(0, limit);
        }

        public static string ClipIfLonger(this string text, bool clip, int limit)
        {
            if (!clip)
            {
                return text;
            }

            return text.Length <= limit ? text : text.Substring(0, limit);
        }

        public static Stream ToUTF8Stream(this string text)
        {
            var ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(text));
            return ms;
        }

        public static void Beautify(this XmlDocument doc, Stream stream)
        {
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\r\n",
                NewLineHandling = NewLineHandling.Replace,
            };
            using XmlWriter writer = XmlWriter.Create(stream, settings);
            doc.Save(writer);
        }

        public static string BeautifyXml(this string doc)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(doc);
            return xmlDoc.Beautify();
        }

        public static string Beautify(this XmlDocument doc)
        {
            var sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\r\n",
                NewLineHandling = NewLineHandling.Replace,
            };
            using (XmlWriter writer = XmlWriter.Create(sb, settings))
            {
                doc.Save(writer);
            }

            return sb.ToString();
        }

        public static string StringJoin<T>(this IEnumerable<T> items, string separator)
        {
            return string.Join(separator, items);
        }

        public static IEnumerable<string> WrapInQuotes(this IEnumerable<string> items)
        {
            return items.Select(o => $"\"{o}\"");
        }

        public static string EmptyIfNull(this string? text)
        {
            return text ?? string.Empty;
        }

        public static string? NullIfEmpty(this string text)
        {
            return text.IsNullOrEmpty() ? null : text;
        }

        public static string CleanFileName(this string name)
        {
            return Path.GetInvalidFileNameChars()
                .Aggregate(name, (n, c) => n.Replace(c, '_')).Replace(':', '_');
        }

        public static string ToInvariantString(this float value)
        {

            return value.ToString(System.Globalization.CultureInfo.InvariantCulture);
        }

        public static string ToInvariantString(this double value)
        {

            return value.ToString(System.Globalization.CultureInfo.InvariantCulture);
        }

        public static float ParseInvariantFloat(this string value)
        {

            return float.Parse(value, System.Globalization.CultureInfo.InvariantCulture);
        }

        public static double ParseInvariantDouble(this string value)
        {

            return double.Parse(value, System.Globalization.CultureInfo.InvariantCulture);
        }

        public static IEnumerable<string> SplitBySpace(this string value)
        {
            return value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static void ParseFloats(this string value, out float val1, out float val2)
        {
            value.SplitBySpace().ParseFloats(out val1, out val2);
        }
        public static void ParseFloats(this IEnumerable<string> value, out float val1, out float val2)
        {

            var enumerator = value.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                val1 = 0;
                val2 = 0;
                return;
            }
            val1 = enumerator.Current.ParseInvariantFloat();
            if (!enumerator.MoveNext())
            {
                val2 = val1;
                return;
            }
            val2 = enumerator.Current.ParseInvariantFloat();
            if (enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }
            return;
        }

        public static void ParseFloats(this string value, out float val1, out float val2, out float val3)
        {
            value.SplitBySpace().ParseFloats(out val1, out val2, out val3);
        }
        public static void ParseFloats(this IEnumerable<string> value, out float val1, out float val2, out float val3)
        {

            var enumerator = value.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                val1 = 0;
                val2 = 0;
                val3 = 0;
                return;
            }
            val1 = enumerator.Current.ParseInvariantFloat();
            if (!enumerator.MoveNext())
            {
                val2 = val1;
                val3 = val1;
                return;
            }
            val2 = enumerator.Current.ParseInvariantFloat();
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }
            val3 = enumerator.Current.ParseInvariantFloat();
            if (enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }
            return;
        }

        public static void ParseFloats(this string value, out float val1, out float val2, out float val3, out float val4)
        {
            value.SplitBySpace().ParseFloats(out val1, out val2, out val3, out val4);
        }
        public static void ParseFloats(this IEnumerable<string> value, out float val1, out float val2, out float val3, out float val4)
        {
            var enumerator = value.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                val1 = 0;
                val2 = 0;
                val3 = 0;
                val4 = 0;
                return;
            }
            val1 = enumerator.Current.ParseInvariantFloat();
            if (!enumerator.MoveNext())
            {
                val2 = val1;
                val3 = val1;
                val4 = val1;
                return;
            }
            val2 = enumerator.Current.ParseInvariantFloat();
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }
            val3 = enumerator.Current.ParseInvariantFloat();
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }
            val4 = enumerator.Current.ParseInvariantFloat();
            if (enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }
            return;
        }
    }
}