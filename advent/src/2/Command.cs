using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Day2Utils
{
    public struct Command
    {
        public CommandType Type { get; }
        public int Value { get; }

        public Command(string cmd)
        {
            var args = cmd.Split(' ');
            Type = args[0] == "forward" ? CommandType.FORWARD : args[0] == "up" ? CommandType.UP : CommandType.DOWN;
            Value = int.Parse(args[1]);
        }
    }

    public enum CommandType { FORWARD, UP, DOWN }
}
