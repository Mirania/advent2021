using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent
{
    class Program
    {
        static void Main(string[] args)
        {
            List<IAdventDay> days = new List<IAdventDay> { new Day1(), new Day2(), new Day3(),
                                                           new Day4(), new Day5(), new Day6(),
                                                           new Day7(), new Day8(), new Day9(),
                                                           new Day10(), new Day11(), new Day12(),
                                                           new Day13(), new Day14(), new Day15(),
                                                           new Day16(), new Day17(), new Day18()
                                                           /* no time for more */ };

            days.ForEach((day, i) =>
            {
                Console.WriteLine($"Day {i + 1}");
                day.Solve();
            });

            Utils.Pause();
        }
    }
}
