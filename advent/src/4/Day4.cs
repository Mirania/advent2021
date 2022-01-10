using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advent.Day4Utils;

namespace Advent
{
    public class Day4 : IAdventDay
    {
        public void Solve()
        {
            var input = Utils.ReadFileRaw(4);
            Console.WriteLine("Part 1: {0}", GetFirstWinningScore(input));
            Console.WriteLine("Part 2: {0}", GetLastWinningScore(input));
        }

        private int GetFirstWinningScore(string input)
        {
            var bingo = new Bingo(input);
            foreach (var draw in bingo.Draws)
            {
                foreach (var board in bingo.Boards)
                {
                    if (board.Mark(draw))
                    {
                        return board.GetUnmarkedSum() * draw;
                    }
                }
            }
            return -1;
        }

        private int GetLastWinningScore(string input)
        {
            var bingo = new Bingo(input);
            foreach (var draw in bingo.Draws)
            {
                foreach (var board in bingo.Boards)
                {
                    if (!board.IsWinner)
                    {
                        board.Mark(draw);
                    }

                    if (bingo.Boards.All(b => b.IsWinner))
                    {
                        return board.GetUnmarkedSum() * draw;
                    }

                }
            }
            return -1;
        }


    }
}
