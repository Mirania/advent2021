using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent
{
    public static class Utils
    {
        public static void Pause()
        {
            Console.WriteLine("\nPress any key to close...");
            Console.ReadKey(true);
        }

        public static string[] ReadFile(int day)
        {
            return File.ReadAllLines($@"..\..\src\{day}\input.txt");
        }

        public static string ReadFileRaw(int day)
        {
            return File.ReadAllText($@"..\..\src\{day}\input.txt");
        }

        public static string NullableToString(object any)
        {
            return any == null ? "null" : any.ToString();
        }

        public static void PrintEnumerable<T>(IEnumerable<T> enumerable)
        {
            Console.WriteLine($"[{string.Join(", ", enumerable)}]");
        }

        public static long Max(IEnumerable<long> values)
        {
            return values.Aggregate(long.MinValue, (prev, cur) => cur > prev ? cur : prev);
        }

        public static long Min(IEnumerable<long> values)
        {
            return values.Aggregate(long.MaxValue, (prev, cur) => cur < prev ? cur : prev);
        }

        public static int Max(IEnumerable<int> values)
        {
            return values.Aggregate(int.MinValue, (prev, cur) => cur > prev ? cur : prev);
        }

        public static int Min(IEnumerable<int> values)
        {
            return values.Aggregate(int.MaxValue, (prev, cur) => cur < prev ? cur : prev);
        }

        public static List<int> ExtractInts(string input)
        {
            return ExtractTokens(input, @"([+-]?\d+)", int.Parse);
        }

        private static List<T> ExtractTokens<T>(string input, string regex, Func<string, T> tokenTransformer)
        {
            var matches = Regex.Matches(input, regex);
            var tokens = new List<T>();
            foreach (Match match in matches)
            {
                tokens.Add(tokenTransformer.Invoke(match.Groups[1].Value));
            }
            return tokens;
        }
    }
}
