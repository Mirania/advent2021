using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent
{
    public class Day7 : IAdventDay
    {
        public void Solve()
        {
            var positions = Utils.ExtractInts(Utils.ReadFileRaw(7));
            Console.WriteLine("Part 1: {0}", CheckBestAlignment(positions, dist => dist));
            Console.WriteLine("Part 2: {0}", CheckBestAlignment(positions, dist => dist * (dist + 1) / 2));
        }

        private int CheckBestAlignment(List<int> positions, Func<int, int> distanceToFuel)
        {
            int min = Utils.Min(positions), max = Utils.Max(positions);
            return Utils.Min(
                    Enumerable.Range(min, max - min + 1)
                    .Select(alignment => positions.Aggregate(0, (prev, cur) => prev += distanceToFuel.Invoke(Math.Abs(cur - alignment))))
            );
        }
    }
}
