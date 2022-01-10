using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advent.Day13Utils;

namespace Advent
{
    public class Day13 : IAdventDay
    {
        public void Solve()
        {
            var manual = new Manual(Utils.ReadFileRaw(13));
            Console.WriteLine("Part 1: {0}", CountPoints(manual));
            Console.WriteLine("Part 2: \n{0}", GetCode(manual));
        }

        private int CountPoints(Manual manual)
        {
            return Fold(manual.Points, manual.Folds.First()).Count;
        }

        private string GetCode(Manual manual)
        {
            var set = new HashSet<Point>(manual.Points);
            manual.Folds.ForEach(fold => set = Fold(set, fold));   

            var sizeX = Utils.Max(set.Select(p => p.X)) + 1;
            var sizeY = Utils.Max(set.Select(p => p.Y)) + 1;
            var grid = new FixedGrid<char>(sizeX, sizeY, () => '.');
            set.ForEach(p => grid[p] = '#');

            return grid.ContentsToString(char.ToString);
        }

        private HashSet<Point> Fold(IEnumerable<Point> points, Fold fold)
        {
            if (fold.IsHorizontal)
            {
                return points.Where(p => p.Y != fold.Value)
                        .Select(p => p.Y < fold.Value ? p : new Point(p.X, p.Y - 2 * (p.Y - fold.Value)))
                        .ToHashSet();
            }
            else
            {
                return points.Where(p => p.X != fold.Value)
                        .Select(p => p.X < fold.Value ? p : new Point(p.X - 2 * (p.X - fold.Value), p.Y))
                        .ToHashSet();
            }
        }
    }
}
