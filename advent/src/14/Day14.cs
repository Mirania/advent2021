using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advent.Day14Utils;

namespace Advent
{
    public class Day14 : IAdventDay
    {
        public void Solve()
        {
            var polymer = new Polymer(Utils.ReadFileRaw(14));
            Console.WriteLine("Part 1: {0}", RunPairInsertion(polymer, 10));
            Console.WriteLine("Part 2: {0}", RunPairInsertion(polymer, 40));
        }

        private long RunPairInsertion(Polymer polymer, int steps)
        {
            var data = new PairInsertionData();

            for (var i = 0; i < polymer.Template.Length; i++)
            {
                if (i < polymer.Template.Length - 1)
                    data.Pairs[$"{polymer.Template[i]}{polymer.Template[i + 1]}"]++;
                data.Elements[polymer.Template[i]]++;
            }

            for (var step = 1; step <= steps; step++)
            {
                InsertPairs(data, polymer);
            }

            return Utils.Max(data.Elements.Select(elem => elem.Value)) - Utils.Min(data.Elements.Select(elem => elem.Value));
        }

        private void InsertPairs(PairInsertionData data, Polymer polymer)
        {
            var newPairs = new KeySafeDictionary<string, long>();

            foreach (var pair in data.Pairs)
            {
                var insertion = polymer.Rules[pair.Key];
                if (insertion == default(char))
                {
                    newPairs[pair.Key] += pair.Value;
                }
                else
                {
                    newPairs[$"{pair.Key[0]}{insertion}"] += pair.Value;
                    newPairs[$"{insertion}{pair.Key[1]}"] += pair.Value;
                    data.Elements[insertion] += pair.Value;
                }
            }

            data.Pairs = newPairs;
        }
    }
}
