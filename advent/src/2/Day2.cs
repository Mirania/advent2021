using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advent.Day2Utils;

namespace Advent
{
    public class Day2 : IAdventDay
    {
        public void Solve()
        {
            var commands = Utils.ReadFile(2).Select(cmd => new Command(cmd));
            Console.WriteLine("Part 1: {0}", CalculatePosition(commands));
            Console.WriteLine("Part 2: {0}", CalculatePositionWithAim(commands));
        }

        private int CalculatePosition(IEnumerable<Command> commands)
        {
            int h = 0, d = 0;
            foreach (var cmd in commands)
            {
                switch (cmd.Type)
                {
                    case CommandType.FORWARD: h += cmd.Value; break;
                    case CommandType.UP: d -= cmd.Value; break;
                    case CommandType.DOWN: d += cmd.Value; break;
                }
            }
            return h * d;
        }

        private int CalculatePositionWithAim(IEnumerable<Command> commands)
        {
            int h = 0, d = 0, aim = 0;
            foreach (var cmd in commands)
            {
                switch (cmd.Type)
                {
                    case CommandType.FORWARD: h += cmd.Value; d += aim * cmd.Value; break;
                    case CommandType.UP: aim -= cmd.Value; break;
                    case CommandType.DOWN: aim += cmd.Value; break;
                }
            }
            return h * d;
        }
    }
}
