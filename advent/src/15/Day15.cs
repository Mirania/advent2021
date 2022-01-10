using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advent.Day15Utils;

namespace Advent
{
    public class Day15 : IAdventDay
    {
        public void Solve()
        {
            var grid = CreateGrid(Utils.ReadFile(15));
            Console.WriteLine("Part 1: {0}", GetBestPath(grid, false));
            Console.WriteLine("Part 2: {0}", GetBestPath(grid, true));
        }
        
        private int GetBestPath(FixedGrid<int> grid, bool expandGrid)
        {
            if (expandGrid)
                grid = ExpandGrid(grid);

            return FindBestPathFromTo(grid, new Point(0, 0), new Point(grid.SizeX - 1, grid.SizeY - 1));
        }

        // adapted from https://github.com/schovanec/AdventOfCode/blob/master/2021/Day15/Program.cs
        private int FindBestPathFromTo(FixedGrid<int> grid, Point start, Point goal)
        {
            var totalRisks = new KeySafeDictionary<Point, int>();
            var visited = new FixedGrid<bool>(grid.SizeX, grid.SizeY, () => false);
            var queue = new PriorityQueue<Node>();

            totalRisks[start] = 0;
            queue.Enqueue(new Node(start, 0));

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                if (visited[node.Point])
                    continue;

                visited[node.Point] = true;

                if (goal.X == node.Point.X && goal.Y == node.Point.Y)
                    return node.Risk;

                foreach (var next in GetAdjacent(node.Point, grid).Where(x => !visited[x]))
                {
                    var nextRisk = node.Risk + grid[next];
                    if (nextRisk < totalRisks[next, int.MaxValue])
                        queue.Enqueue(new Node(next, nextRisk));
                }
            }

            throw new ArgumentException($"Grid has no solution from {start} to {goal}");
        }

        private FixedGrid<int> CreateGrid(string[] inputs)
        {
            var grid = new FixedGrid<int>(inputs[0].Length, inputs.Length);
            for (int x = 0; x < grid.SizeX; x++)
                for (int y = 0; y < grid.SizeY; y++)
                    grid[x, y] = int.Parse(inputs[y][x].ToString());
            return grid;
        }

        private FixedGrid<int> ExpandGrid(FixedGrid<int> original)
        {
            var grid = new FixedGrid<int>(original.SizeX * 5, original.SizeY * 5);
            for (int x = 0; x < grid.SizeX; x++)
                for (int y = 0; y < grid.SizeY; y++)
                {
                    var xRepeats = x / original.SizeX; //0-4
                    var yRepeats = y / original.SizeY; //0-4
                    var totalValue = original[x % original.SizeX, y % original.SizeY] + xRepeats + yRepeats;
                    var realValue = totalValue > 9 ? (totalValue % 9) : totalValue;
                    grid[x, y] = realValue;
                }
            return grid;
        }

        private IEnumerable<Point> GetAdjacent(Point tile, FixedGrid<int> grid)
        {
            if (tile.X - 1 >= 0) yield return new Point(tile.X - 1, tile.Y);
            if (tile.Y - 1 >= 0) yield return new Point(tile.X, tile.Y - 1);
            if (tile.X + 1 < grid.SizeX) yield return new Point(tile.X + 1, tile.Y);
            if (tile.Y + 1 < grid.SizeY) yield return new Point(tile.X, tile.Y + 1);
        }
    }
}
