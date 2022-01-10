using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Day15Utils
{
    public class Node : IComparable<Node>
    {
        public Point Point { get; }
        public int Risk { get; }

        public Node(Point p, int risk)
        {
            Point = p;
            Risk = risk;
        }

        public override string ToString()
        {
            return $"Node[point={Point}, risk={Risk}]";
        }

        public int CompareTo(Node other)
        {
            return Risk - other.Risk;
        }
    }
}
