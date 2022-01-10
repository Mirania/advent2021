﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advent.Day16Utils;

namespace Advent
{
    public class Day16 : IAdventDay
    {
        public void Solve()
        {
            var packet = new Packet(HexToBin(Utils.ReadFileRaw(16)));
            Console.WriteLine("Part 1: {0}", packet.VersionSum);
            Console.WriteLine("Part 2: {0}", packet.Literal);
        }

        private string HexToBin(string hex)
        {
            var sb = new StringBuilder(hex.Length * 4);
            foreach (var ch in hex)
            {
                switch (ch)
                {
                    case '0': sb.Append("0000"); break;
                    case '1': sb.Append("0001"); break;
                    case '2': sb.Append("0010"); break;
                    case '3': sb.Append("0011"); break;
                    case '4': sb.Append("0100"); break;
                    case '5': sb.Append("0101"); break;
                    case '6': sb.Append("0110"); break;
                    case '7': sb.Append("0111"); break;
                    case '8': sb.Append("1000"); break;
                    case '9': sb.Append("1001"); break;
                    case 'A': sb.Append("1010"); break;
                    case 'B': sb.Append("1011"); break;
                    case 'C': sb.Append("1100"); break;
                    case 'D': sb.Append("1101"); break;
                    case 'E': sb.Append("1110"); break;
                    case 'F': sb.Append("1111"); break;
                }
            }
            return sb.ToString();
        }
    }
}
