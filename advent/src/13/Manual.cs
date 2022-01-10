using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Day13Utils
{
    public class Manual
    {
        public IEnumerable<Point> Points { get; }
        public IEnumerable<Fold> Folds { get; }

        public Manual(string input)
        {
            var sections = input.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            Points = sections[0].Split('\n').Select(line => line.Split(',')).Select(values => new Point(int.Parse(values[0]), int.Parse(values[1])));
            Folds = sections[1].Split('\n').Select(line => new Fold(line));
        }
    }

    public struct Fold
    {
        public bool IsHorizontal { get; }
        public int Value { get; }

        public Fold(string input)
        {
            var tokens = input.Split(' ')[2].Split('=');
            IsHorizontal = tokens[0] == "y";
            Value = int.Parse(tokens[1]);
        }
    }
}
