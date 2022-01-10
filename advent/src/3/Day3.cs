using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent
{
    public class Day3 : IAdventDay
    {
        public void Solve()
        {
            var report = Utils.ReadFile(3);
            Console.WriteLine("Part 1: {0}", GetPowerConsumption(report));
            Console.WriteLine("Part 2: {0}", GetLifeSupportRating(report));
        }

        private int GetPowerConsumption(string[] report)
        {
            var bits = report[0].Length;
            var gammaBits = new char[bits];
            for (int i = 0; i < bits; i++)
            {
                var count1 = Count1s(report, i);
                gammaBits[i] = count1 > (report.Length - count1) ? '1' : '0';
            }
            var gamma = Convert.ToUInt32(new string(gammaBits), 2);
            return (int) (gamma * (~gamma % Math.Pow(2, bits)));
        }

        private int GetLifeSupportRating(string[] report)
        {
            var oxygenRating = GetRating(report, true);
            var scrubberRating = GetRating(report, false);
            return (int) (oxygenRating * scrubberRating);
        }

        private int Count1s(IEnumerable<string> entries, int bit)
        {
            var count = 0;
            foreach (var entry in entries)
            {
                count += entry[bit] == '1' ? 1 : 0;
            }
            return count;
        }

        private uint GetRating(string[] report, bool useMostCommonBits)
        {
            var filtered = new List<string>(report);
            var bit = 0;
            while (filtered.Count > 1)
            {
                var count1 = Count1s(filtered, bit);
                var criteria = (count1 >= (filtered.Count - count1) == useMostCommonBits) ? '1' : '0';
                filtered = new List<string>(filtered.Where(r => r[bit] == criteria));
                bit++;
            }
            return Convert.ToUInt32(filtered[0], 2);
        }
    }
}
