using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Day12Utils
{
    public class Cave
    {
        public static string Start = "start";
        public static string End = "end";

        public string Name { get; }
        public bool IsSmall { get; }
        public bool IsStart { get; }
        public bool IsEnd { get; }

        public Cave(string name)
        {
            Name = name;
            IsSmall = char.IsLower(name[0]);
            IsStart = name == Start;
            IsEnd = name == End;
        }
    }

    public class Counter
    {
        public int Value { get; set; }
    }

}
