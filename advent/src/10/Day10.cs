using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advent.Day10Utils;

namespace Advent
{
    public class Day10 : IAdventDay
    {
        public void Solve()
        {
            var syntaxCheck = new SyntaxChecker(Utils.ReadFile(10));
            Console.WriteLine("Part 1: {0}", GetCorruptionScore(syntaxCheck.Corruptions));
            Console.WriteLine("Part 2: {0}", GetRepairScore(syntaxCheck.Repairs));
        }

        private int GetCorruptionScore(KeySafeDictionary<char, int> corruptions)
        {
            return corruptions[')'] * 3 + corruptions[']'] * 57 + corruptions['}'] * 1197 + corruptions['>'] * 25137;
        }

        private long GetRepairScore(List<List<char>> repairs)
        {
            var scores = new List<long>();

            foreach (var repair in repairs)
            {
                scores.Add(repair.Aggregate(0L, (prev, cur) => prev * 5 + (cur == ')' ? 1 : cur == ']' ? 2 : cur == '}' ? 3 : 4)));
            }

            scores.Sort();
            return scores[scores.Count / 2];
        }
    }
}
