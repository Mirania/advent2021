using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Day11Utils
{
    public class Tile
    {
        public int X { get; }
        public int Y { get; }
        public int Energy { get; set; }
        public bool Flashed { get; set; }

        public Tile(int x, int y, int energy)
        {
            X = x;
            Y = y;
            Energy = energy;
            Flashed = false;
        }

        public override string ToString()
        {
            return $"Tile[pos=({X},{Y}), energy={Energy}, flashed={Flashed}]";
        }
    }
}
