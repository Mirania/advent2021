using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Day5Utils
{
    public struct Vent
    {
        public Point Start { get; }
        public Point End { get; }

        public Vent(int x1, int y1, int x2, int y2)
        {
            Start = new Point(x1, y1);
            End = new Point(x2, y2);
        }
    }
}
