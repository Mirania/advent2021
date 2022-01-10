using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advent.Day11Utils;

namespace Advent
{
    public class Day11 : IAdventDay
    {
        public void Solve()
        {
            var lines = Utils.ReadFile(11);
            Console.WriteLine("Part 1: {0}", CountFlashes(lines, 100));
            Console.WriteLine("Part 2: {0}", FindSimultaneousFlash(lines));
        }

        private int CountFlashes(string[] lines, int steps)
        {
            var grid = CreateGrid(lines);
            var flashes = 0;

            for (var step = 0; step < steps; step++)
                flashes += RunStep(grid);
      
            return flashes;
        }

        private int FindSimultaneousFlash(string[] lines)
        {
            var grid = CreateGrid(lines);
            var step = 0;

            while (true)
            {
                step++;
                if (RunStep(grid) == grid.SizeX * grid.SizeY)
                    return step;
            }
        }

        private FixedGrid<Tile> CreateGrid(string[] inputs)
        {
            var grid = new FixedGrid<Tile>(inputs[0].Length, inputs.Length);
            for (int x = 0; x < grid.SizeX; x++)
                for (int y = 0; y < grid.SizeY; y++)
                    grid[x, y] = new Tile(x, y, int.Parse(inputs[y][x].ToString()));
            return grid;
        }

        // returns the amount of flashes that occurred during this step
        private int RunStep(FixedGrid<Tile> grid)
        {
            var flashes = 0;

            foreach (var tile in grid.Values)
            {
                tile.Energy++;
                tile.Flashed = false;
            }
            foreach (var tile in grid.Values)
            {
                flashes += CheckFlash(tile, grid);
            }

            return flashes;
        }

        // if this tile flashes, returns the amount of flashes that this flash caused, including itself
        private int CheckFlash(Tile tile, FixedGrid<Tile> grid)
        {
            var flashes = 0;

            if (tile.Energy > 9 && !tile.Flashed)
            {
                flashes++;
                tile.Energy = 0;
                tile.Flashed = true;

                foreach (var adj in GetAdjacent(tile, grid).Where(t => !t.Flashed))
                {
                    adj.Energy++;
                    if (adj.Energy > 9)
                        flashes += CheckFlash(adj, grid);
                }
            }

            return flashes;
        }

        private IEnumerable<Tile> GetAdjacent(Tile tile, FixedGrid<Tile> grid)
        {
            if (tile.X - 1 >= 0) yield return grid[tile.X - 1, tile.Y];
            if (tile.X - 1 >= 0 && tile.Y - 1 >= 0) yield return grid[tile.X - 1, tile.Y - 1];
            if (tile.Y - 1 >= 0) yield return grid[tile.X, tile.Y - 1];
            if (tile.X + 1 < grid.SizeX && tile.Y - 1 >= 0) yield return grid[tile.X + 1, tile.Y - 1];
            if (tile.X + 1 < grid.SizeX) yield return grid[tile.X + 1, tile.Y];
            if (tile.X + 1 < grid.SizeX && tile.Y + 1 < grid.SizeY) yield return grid[tile.X + 1, tile.Y + 1];
            if (tile.Y + 1 < grid.SizeY) yield return grid[tile.X, tile.Y + 1];
            if (tile.X - 1 >= 0 && tile.Y + 1 < grid.SizeY) yield return grid[tile.X - 1, tile.Y + 1];
        }
    }
}
