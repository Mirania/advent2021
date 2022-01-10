using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advent.Day8Utils;

namespace Advent
{
    public class Day8 : IAdventDay
    {
        public void Solve()
        {
            var notes = Utils.ReadFile(8).Select(line => new Digits(line));
            Console.WriteLine("Part 1: {0}", CountEasyDigits(notes));
            Console.WriteLine("Part 2: {0}", SumOutputValues(notes));
        }

        private int CountEasyDigits(IEnumerable<Digits> notes)
        {
            return notes.Select(digits => digits.Outputs.Where(o => o.Length == 2 || o.Length == 3 || o.Length == 4 || o.Length == 7).Count()).Sum();
        }

        private int SumOutputValues(IEnumerable<Digits> notes)
        {
            return notes.Select(digits =>
            {
                var value = 0;
                for (int i = digits.Outputs.Length - 1, mult = 1; i >= 0; i--, mult *= 10)
                {
                    var wires = digits.Outputs[i];
                    value += digits.Mappings.Where(pair => wires.Length == pair.Value.Length && wires.All(wire => pair.Value.Contains(wire)))
                                            .Select(pair => pair.Key)
                                            .First() * mult;
                }
                return value;
            }).Sum();
        }
    }
}
