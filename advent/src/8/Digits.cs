using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Day8Utils
{
    public class Digits
    {
        public string[] Signals { get; }
        public string[] Outputs { get; }
        public IDictionary<int, string> Mappings { get; }

        public Digits(string line)
        {
            var sections = line.Split(new string[] { " | " }, StringSplitOptions.RemoveEmptyEntries);
            Signals = sections[0].Split(' ');
            Outputs = sections[1].Split(' ');
            Mappings = Decode();
        }

        ///
        /// 1, 4, 7 and 8 are already known
        /// compare 6-signal numbers to 7, only 6 will not contain all signals from 7. find 6
        /// compare 0 and 9 to 4, only 9 will contain all signals from 4. find 9
        /// 0 is the last 6-signal number. find 0
        /// compare 5-signal numbers to 6, only 6 will contain all signals from 5. find 5
        /// compare 2 and 3 to 5, only a union of 2 and 5 will contain every signal. find 2
        /// 3 is the last 5-signal number. find 3
        ///
        private IDictionary<int, string> Decode()
        {
            var decoded = new Dictionary<int, string>
            {
                [1] = Signals.Where(s => s.Length == 2).First(),
                [4] = Signals.Where(s => s.Length == 4).First(),
                [7] = Signals.Where(s => s.Length == 3).First(),
                [8] = Signals.Where(s => s.Length == 7).First()
            };

            var fiveSignalNumbers = Signals.Where(s => s.Length == 5).ToList();
            var sixSignalNumbers = Signals.Where(s => s.Length == 6).ToList();

            decoded[6] = sixSignalNumbers.Where(s => decoded[7].Any(wire => !s.Contains(wire))).First();
            sixSignalNumbers.Remove(decoded[6]);
            decoded[9] = sixSignalNumbers.Where(s => decoded[4].All(wire => s.Contains(wire))).First();
            sixSignalNumbers.Remove(decoded[9]);
            decoded[0] = sixSignalNumbers.First();

            decoded[5] = fiveSignalNumbers.Where(s => s.All(wire => decoded[6].Contains(wire))).First();
            fiveSignalNumbers.Remove(decoded[5]);
            decoded[2] = fiveSignalNumbers.Where(s => "abcdefg".All(wire => (s + decoded[5]).Contains(wire))).First();
            fiveSignalNumbers.Remove(decoded[2]);
            decoded[3] = fiveSignalNumbers.First();

            return decoded;
        }
    }
}
