using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Day4Utils
{
    public struct Bingo
    {
        public IEnumerable<int> Draws { get; }
        public Board[] Boards { get; }

        public Bingo(string input)
        {
            var sections = input.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            Draws = sections[0].Split(',').Select(draw => int.Parse(draw));
            Boards = new List<string>(sections).GetRange(1, sections.Length - 1).Select(section => new Board(section)).ToArray();
        }
    }

    public class Board
    {
        public BoardNumber[][] Numbers { get; } // [y - row][x - column]
        public bool IsWinner { get; private set; }

        public Board(string section)
        {
            var lines = section.Split('\n');
            Numbers = section.Split('\n').Select(line => line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(val => new BoardNumber(val))
                            .ToArray())
                      .ToArray();
            IsWinner = false;
        }

        public bool Mark(int draw)
        {
            for (var y = 0; y < Numbers.Length; y++)
            {
                for (var x = 0; x < Numbers.Length; x++)
                {
                    if (Numbers[y][x].Value == draw)
                    {
                        Numbers[y][x].IsMarked = true;
                        IsWinner = IsWinner || Numbers[y].All(num => num.IsMarked) || Numbers.All(row => row[x].IsMarked);
                        goto end;
                    }
                }
            }

        end:
            return IsWinner;
        }

        public int GetUnmarkedSum()
        {
            return Numbers.Select(row => row.Where(num => !num.IsMarked).Select(num => num.Value).Sum()).Sum();
        }

        public override string ToString()
        {
            return string.Join("\n", Numbers.Select(row => string.Join(" ", row)));
        }
    }

    public struct BoardNumber
    {
        public int Value { get; }
        public bool IsMarked { get; set; }

        public BoardNumber(string value)
        {
            Value = int.Parse(value);
            IsMarked = false;
        }

        public override string ToString()
        {
            return $"{Value}({IsMarked})";
        }
    }
}
