using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Day16Utils
{
    public class Packet
    {
        private string Binary { get; }

        protected string ParsedBits { get; }

        public int Version { get; } = -1;
        public int Type { get; } = -1;
        public int LengthType { get; } = -1;
        public long Literal { get; }
        public List<Packet> Subpackets { get; }
        public int VersionSum { get; }   

        public Packet(string binary)
        {
            Binary = binary;
            Version = BitsToInt(0, 3);
            Type = BitsToInt(3, 6);
            
            if (Type == 4)
            {
                var sb = new StringBuilder();
                var index = 1;
                do
                {
                    index += 5;
                    sb.Append(binary.Substring(index + 1, 4));
                } while (binary[index] != '0');
                    
                Literal = Convert.ToInt64(sb.ToString(), 2);
                ParsedBits = Binary.Substring(0, index + 5);
                VersionSum = Version;
            }
            else
            {
                LengthType = BitsToInt(6, 7);

                if (LengthType == 0)
                {
                    var subpacketBits = BitsToInt(7, 22);
                    Subpackets = new List<Packet>();
                    var index = 22;
                    while (index < 22 + subpacketBits)
                    {
                        var subpacket = new Packet(Binary.Substring(index));
                        Subpackets.Add(subpacket);
                        index += subpacket.ParsedBits.Length;
                    }
                    ParsedBits = Binary.Substring(0, index);
                }
                else
                {
                    var subpacketCount = BitsToInt(7, 18);
                    Subpackets = new List<Packet>(subpacketCount);
                    var index = 18;
                    for (var counter = 0; counter < subpacketCount; counter++)
                    {
                        var subpacket = new Packet(Binary.Substring(index));
                        Subpackets.Add(subpacket);
                        index += subpacket.ParsedBits.Length;
                    }
                    ParsedBits = Binary.Substring(0, index);
                }

                switch (Type)
                {
                    case 0: Literal = Subpackets.Aggregate(0L, (p, c) => p + c.Literal); break;
                    case 1: Literal = Subpackets.Aggregate(1L, (p, c) => p * c.Literal); break;
                    case 2: Literal = Utils.Min(Subpackets.Select(sp => sp.Literal)); break;
                    case 3: Literal = Utils.Max(Subpackets.Select(sp => sp.Literal)); break;
                    case 5: Literal = Subpackets[0].Literal > Subpackets[1].Literal ? 1 : 0; break;
                    case 6: Literal = Subpackets[0].Literal < Subpackets[1].Literal ? 1 : 0; break;
                    case 7: Literal = Subpackets[0].Literal == Subpackets[1].Literal ? 1 : 0; break;
                }

                VersionSum = Version + Subpackets.Sum(sp => sp.VersionSum);
            }
        }

        // endIndex is exclusive
        private int BitsToInt(int startIndex, int endIndex) => Convert.ToInt32(Binary.Substring(startIndex, endIndex - startIndex), 2);
    }
}
