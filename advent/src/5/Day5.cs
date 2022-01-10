using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advent.Day5Utils;

namespace Advent
{
    public class Day5 : IAdventDay
    {
        public void Solve()
        {
            var vents = Utils.ReadFile(5).Select(Utils.ExtractInts).Select(n => new Vent(n[0], n[1], n[2], n[3]));
            Console.WriteLine("Part 1: {0}", CountOverlaps(vents, false));
            Console.WriteLine("Part 2: {0}", CountOverlaps(vents, true));
        }

        private int CountOverlaps(IEnumerable<Vent> vents, bool includeDiagonals)
        {
            var diagram = new FixedGrid<int>(1000, 1000);
            foreach (var vent in vents)
            {
                if (vent.Start.X == vent.End.X)
                {
                    int start = Math.Min(vent.Start.Y, vent.End.Y), end = Math.Max(vent.Start.Y, vent.End.Y);
                    for (int y = start; y <= end; y++)
                    {
                        diagram[vent.Start.X, y]++;
                    }
                }
                else if (vent.Start.Y == vent.End.Y)
                {
                    int start = Math.Min(vent.Start.X, vent.End.X), end = Math.Max(vent.Start.X, vent.End.X);
                    for (int x = start; x <= end; x++)
                    {
                        diagram[x, vent.Start.Y]++;
                    }
                }
                else if (includeDiagonals)
                {
                    int x = vent.Start.X, y = vent.Start.Y;
                    bool incrementX = vent.Start.X < vent.End.X, incrementY = vent.Start.Y < vent.End.Y;
                    do
                    {
                        diagram[x, y]++;
                        x += incrementX ? 1 : -1;
                        y += incrementY ? 1 : -1;
                    }
                    while ((incrementX ? x <= vent.End.X : x >= vent.End.X) && (incrementY ? y <= vent.End.Y : y >= vent.End.Y));
                }
            }
            return diagram.Values.Where(num => num > 1).Count();
        }
    }
}
