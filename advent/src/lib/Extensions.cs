using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent
{
    public static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var element in source)
                action.Invoke(element);
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T,int> action)
        {
            int index = 0;
            foreach (var element in source)
                action.Invoke(element, index++);
        }

        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source, IEqualityComparer<T> comparer = null)
        {
            return new HashSet<T>(source, comparer);
        }

        public static string[] Split(this string source, string separator, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            return source.Split(new string[] { separator }, options);
        }
    }
}
