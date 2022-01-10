using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Day10Utils
{
    public class SyntaxChecker
    {
        public KeySafeDictionary<char, int> Corruptions { get; }
        public List<List<char>> Repairs { get; }

        public SyntaxChecker(string[] inputs)
        {
            Corruptions = new KeySafeDictionary<char, int>();
            Repairs = new List<List<char>>();

            foreach (var input in inputs)
            {
                var error = ParseInput(input, out var state);
                if (error.HasValue)
                {
                    Corruptions[error.Value]++;
                }
                else if (state.Count > 0)
                {
                    Repairs.Add(RepairInput(state));
                }
            }
        }

        private char? ParseInput(string input, out Stack<char> state)
        {
            state = new Stack<char>();

            foreach (var token in input)
            {
                switch (token)
                {
                    case '(':
                    case '[':
                    case '{':
                    case '<':
                        state.Push(token);
                        break;
                    case ')':
                        if (state.Peek() != '(') return token;
                        else state.Pop();
                        break;
                    case ']':
                        if (state.Peek() != '[') return token;
                        else state.Pop();
                        break;
                    case '}':
                        if (state.Peek() != '{') return token;
                        else state.Pop();
                        break;
                    case '>':
                        if (state.Peek() != '<') return token;
                        else state.Pop();
                        break;
                }
            }

            return null;
        }

        private List<char> RepairInput(Stack<char> state)
        {
            var repairs = new List<char>();

            while (state.Count > 0)
            {
                switch (state.Pop())
                {
                    case '(': repairs.Add(')'); break;
                    case '[': repairs.Add(']'); break;
                    case '{': repairs.Add('}'); break;
                    case '<': repairs.Add('>'); break;
                }
            }

            return repairs;
        }
    }
}
