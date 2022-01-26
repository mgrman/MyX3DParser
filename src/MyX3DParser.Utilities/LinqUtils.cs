using System;
using System.Collections.Generic;
using System.Linq;

namespace MyX3DParser.Utils
{
    internal static class LinqUtils
    {
        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> items) where T : class
        {
            return items.Where(o => o != null)
                .Select(o => o!);
        }

        public static IReadOnlyList<T> ToListNotNull<T>(this IEnumerable<T?> items, out bool anyNull) where T : class
        {
            var list = new List<T>((items as ICollection<T>)?.Count ?? (items as IReadOnlyCollection<T>)?.Count ?? 0);
            anyNull = false;
            foreach (var item in items)
            {
                if (item == null)
                {
                    anyNull = true;
                    continue;
                }

                list.Add(item);
            }

            return list;
        }

        public static TValue GetOrAddValue<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key) where TKey : notnull
            where TValue : new()
        {
            if (!dict.TryGetValue(key, out var value))
            {
                value = new TValue();
                dict[key] = value;
            }

            return value;
        }

        public static bool TryGetFirstOrDefault<T>(this IEnumerable<T> items, Func<T, bool> func, out T res)
        {
            using var enumerator = items.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (func(enumerator.Current))
                {
                    res = enumerator.Current;
                    return true;
                }
            }

            res = default!;
            return false;
        }

        public static IEnumerable<T> ToEnumerable<T>(this T item)
        {
            yield return item;
        }

        public static IEnumerable<T> ToEnumerableIfNotNull<T>(this T? item) where T : class
        {
            if (item != null)
            {
                yield return item;
            }
        }

        public static IEnumerable<(T1, T2)> EachWithEach<T1, T2>(this IEnumerable<T1> items1, IEnumerable<T2> items2)
        {
            foreach (var item1 in items1)
            {
                foreach (var item2 in items2)
                {
                    yield return (item1, item2);
                }
            }
        }

        public static IEnumerable<(T1, T2, T3)> EachWithEach<T1, T2, T3>(this IEnumerable<T1> items1, IEnumerable<T2> items2, IEnumerable<T3> items3)
        {
            foreach (var item1 in items1)
            {
                foreach (var item2 in items2)
                {
                    foreach (var item3 in items3)
                    {
                        yield return (item1, item2, item3);
                    }
                }
            }
        }

        public static T AddAndReturn<T>(this IList<T> list) where T : new()
        {
            return AddAndReturn<T>(list, () => new T());
        }

        public static T AddAndReturn<T>(this IList<T> list, Func<T> factory)
        {
            var instance = factory();
            list.Add(instance);
            return instance;
        }

        public static bool IsContainedIn<T>(this T item, IEnumerable<T> items)
        {
            return items.Contains(item);
        }

        public static bool TryGetBy<T>(this IEnumerable<T> items, Func<T, bool> condition, out T res)
        {
            foreach (var item in items)
            {
                if (condition(item))
                {
                    res = item;
                    return true;
                }
            }

            res = default!;
            return false;
        }

        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> key)
        {
            return items.GroupBy(key)
                .Select(o => o.First());
        }

        public static bool None<T>(this IEnumerable<T> items, Func<T, bool> key)
        {
            return !items.Any(key);
        }

        public static List<T> CreateEmptyListFromPrototype<T>(this T prototype)
        {
            return new List<T>();
        }

        public static string LineJoin(this IEnumerable<string> lines, int spacePadding = 0)
        {
            return lines.Select(o => spacePadding == 0 ? o : new string(' ', spacePadding) + o)
                .StringJoin(Environment.NewLine);
        }

        public static IEnumerable<IEnumerable<T>> GroupByIndex<T>(this IEnumerable<T> items, int count)
        {
            using var enumerator = items.GetEnumerator();

            while (enumerator.MoveNext())
            {
                yield return YieldByCount(enumerator, count);
            }
        }

        private static IEnumerable<T> YieldByCount<T>(this IEnumerator<T> items, int count)
        {
            int i = 0;
            do
            {
                yield return items.Current;
                i++;
                if (i >= count)
                {
                    yield break;
                }
            } while (items.MoveNext());
        }

        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T>? items)
        {
            return items ?? Array.Empty<T>();
        }

        public static IEnumerable<T> EnsureAllTheSame<T>(this IEnumerable<T> items)
        {
            using var enumerator = items.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                yield break;
            }

            var firstItem = enumerator.Current;

            yield return firstItem;

            while (enumerator.MoveNext())
            {
                if (!EqualityComparer<T>.Default.Equals(enumerator.Current, firstItem))
                {
                    throw new InvalidOperationException();
                }

                yield return enumerator.Current;
            }
        }

        public static bool IsSetEqual<T>(this IEnumerable<T>? a, IEnumerable<T>? b)
        {
            if (a == null && b == null)
            {
                return true;
            }
            if (a == null || b == null)
            {
                return false;
            }
            var hs = a.ToHashSet();
            foreach (var bItem in b)
            {
                if (!hs.Remove(bItem))
                {
                    return false;
                }
            }

            return hs.Count == 0;
        }

        public static int IndexOf<T>(this IEnumerable<T>? items, T value)
        {
            var index = 0;
            foreach(var item in items)
            {
                if (EqualityComparer<T>.Default.Equals(item , value))
                {
                    return index;
                }

                index++;
            }
            return -1;
        }

#if NETSTANDARD2_0
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> items)
        {
            return new HashSet<T>(items);
        }
#endif
    }
}