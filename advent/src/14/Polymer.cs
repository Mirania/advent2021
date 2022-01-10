using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Day14Utils
{
    public class Polymer
    {
        public string Template { get; set; }
        public KeySafeDictionary<string, char> Rules { get; }

        public Polymer(string input)
        {
            var sections = input.Split("\r\n\r\n");
            Template = sections[0];
            Rules = new KeySafeDictionary<string, char>();
            sections[1].Split("\r\n").Select(line => line.Split(" -> ")).ForEach(values => Rules[values[0]] = values[1][0]);
        }
    }

    public class PairInsertionData
    {
        public KeySafeDictionary<string, long> Pairs { get; set; }
        public KeySafeDictionary<char, long> Elements { get; set; }

        public PairInsertionData()
        {
            Pairs = new KeySafeDictionary<string, long>();
            Elements = new KeySafeDictionary<char, long>();
        }
    }
}
