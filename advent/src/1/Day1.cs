using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent
{
    public class Day1 : IAdventDay
    {
        public void Solve()
        {
            var depths = Utils.ReadFile(1).Select(val => int.Parse(val));
            Console.WriteLine("Part 1: {0}", CountIncreases(depths));
            Console.WriteLine("Part 2: {0}", CountWindowIncreases(depths));
        }

        private int CountIncreases(IEnumerable<int> depths)
        {
            var count = 0;
            depths.Aggregate((prev, cur) => { count += cur > prev ? 1 : 0; return cur; });
            return count;
        }

        private int CountWindowIncreases(IEnumerable<int> depths)
        {
            var list = new List<int>(depths);
            var count = 0;
            for (int i = 1; i < list.Count - 2; i++)
            {
                count += list.GetRange(i, 3).Sum() > list.GetRange(i-1, 3).Sum() ? 1 : 0;
            }
            return count;
        }
    }
}
