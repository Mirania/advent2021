using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advent.Day9Utils;

namespace Advent
{
    public class Day9 : IAdventDay
    {
        public void Solve()
        {
            var heightmap = CreateHeightmap(Utils.ReadFile(9));
            var lowPoints = FindLowPoints(heightmap);
            Console.WriteLine("Part 1: {0}", CalculateRisk(lowPoints));
            Console.WriteLine("Part 2: {0}", CalculateBasins(lowPoints, heightmap));
        }

        private int CalculateRisk(List<Tile> lowPoints)
        {
            return lowPoints.Aggregate(0, (prev, cur) => prev + cur.Height + 1);
        }

        private int CalculateBasins(List<Tile> lowPoints, FixedGrid<Tile> heightmap)
        {
            var sizes = new List<int>();
            var stack = new Stack<Tile>();

            foreach (var lowPoint in lowPoints)
            {
                var size = 0;
                lowPoint.Checked = true;
                stack.Push(lowPoint);

                do
                {
                    size++;
                    var current = stack.Pop();
                    foreach (var tile in GetAdjacent(current, heightmap).Where(tile => !tile.Checked && tile.Height < 9))
                    {
                        tile.Checked = true;
                        stack.Push(tile);
                    }
                } while (stack.Count > 0);

                sizes.Add(size);
            }

            sizes.Sort((a, b) => b - a); // no priority queue in this .NET version :(
            return sizes[0] * sizes[1] * sizes[2];
        }

        private FixedGrid<Tile> CreateHeightmap(string[] inputs)
        {
            var heightmap = new FixedGrid<Tile>(inputs[0].Length, inputs.Length);
            for (int x = 0; x < heightmap.SizeX; x++)
                for (int y = 0; y < heightmap.SizeY; y++)
                    heightmap[x, y] = new Tile(x, y, int.Parse(inputs[y][x].ToString()));
            return heightmap;
        }

        private List<Tile> FindLowPoints(FixedGrid<Tile> heightmap)
        {
            var lowPoints = new List<Tile>();
            for (int x = 0; x < heightmap.SizeX; x++)
                for (int y = 0; y < heightmap.SizeY; y++)
                    if (GetAdjacent(heightmap[x, y], heightmap).All(tile => tile.Height > heightmap[x, y].Height))
                        lowPoints.Add(heightmap[x, y]);
            return lowPoints;
        }

        private IEnumerable<Tile> GetAdjacent(Tile tile, FixedGrid<Tile> heightmap)
        {
            if (tile.X - 1 >= 0) yield return heightmap[tile.X - 1, tile.Y];
            if (tile.Y - 1 >= 0) yield return heightmap[tile.X, tile.Y - 1];
            if (tile.X + 1 < heightmap.SizeX) yield return heightmap[tile.X + 1, tile.Y];
            if (tile.Y + 1 < heightmap.SizeY) yield return heightmap[tile.X, tile.Y + 1];
        }
    }
}
