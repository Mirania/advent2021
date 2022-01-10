using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advent.Day18Utils;

namespace Advent
{
    public class Day18 : IAdventDay
    {
        public void Solve()
        {
            var inputs = Utils.ReadFile(18);
            Console.WriteLine("Part 1: {0}", GetMagnitude(inputs));
            Console.WriteLine("Part 2: {0}", GetHighestMagnitude(inputs));
        }

        private long GetMagnitude(string[] inputs)
        {
            return inputs.Select(Snailfish.Parse).Aggregate((prev, cur) => (prev + cur).Reduce()).Magnitude();
        }

        private long GetHighestMagnitude(string[] inputs)
        {
            var magnitudes = new List<long>();

            for (int a = 0; a < inputs.Length; a++)
                for (int b = a; b < inputs.Length; b++)
                {
                    magnitudes.Add((Snailfish.Parse(inputs[a]) + Snailfish.Parse(inputs[b])).Reduce().Magnitude());
                    magnitudes.Add((Snailfish.Parse(inputs[b]) + Snailfish.Parse(inputs[a])).Reduce().Magnitude());
                }

            return Utils.Max(magnitudes);
        }
    }
}
