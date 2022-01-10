using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent
{
    public class Day6 : IAdventDay
    {
        public void Solve()
        {
            var initialState = Utils.ExtractInts(Utils.ReadFileRaw(6));
            Console.WriteLine("Part 1: {0}", CountTotalFish(initialState, 80));
            Console.WriteLine("Part 2: {0}", CountTotalFish(initialState, 256));
        }

        private long CountTotalFish(List<int> initialState, int day)
        {
            var currentState = new KeySafeDictionary<int, long>(); // <timer value, count of entities with that timer value>
            foreach (var state in initialState)
            {
                currentState[state, 0]++;
            }

            for (int currentDay = 1; currentDay <= day; currentDay++)
            {
                var previousState = currentState;
                currentState = new KeySafeDictionary<int, long>();
                for (int i = 1; i <= 8; i++)
                {
                    currentState[i - 1] = previousState[i, 0];
                }
                currentState[8] = previousState[0];
                currentState[6] += previousState[0];
            }

            return currentState.Select(p => p.Value).Sum();
        }
    }
}
