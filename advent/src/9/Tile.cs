using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Day9Utils
{
    public class Tile
    {
        public int X { get; }
        public int Y { get; }
        public int Height { get; }
        public bool Checked { get; set; }

        public Tile(int x, int y, int height)
        {
            X = x;
            Y = y;
            Height = height;
            Checked = false;
        }

        public override string ToString()
        {
            return $"Tile[pos=({X},{Y}), height={Height}, checked={Checked}]";
        }
    }
}
