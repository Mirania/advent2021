using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advent.Day12Utils;

namespace Advent
{
    public class Day12 : IAdventDay
    {
        public void Solve()
        {
            var graph = CreateGraph(Utils.ReadFile(12));
            Console.WriteLine("Part 1: {0}", CountPaths(graph, false));
            Console.WriteLine("Part 2: {0}", CountPaths(graph, true));
        }

        private int CountPaths(KeySafeDictionary<string, List<Cave>> graph, bool canRevisitSmall)
        {
            var counter = new Counter();
            FindPaths(Cave.Start, graph, new KeySafeDictionary<string, int>(), new List<string>() { Cave.Start }, canRevisitSmall, counter);
            return counter.Value;
        }

        // currentPath only exists for debugging purposes
        // https://www.geeksforgeeks.org/find-paths-given-source-destination/
        private void FindPaths(string cave, KeySafeDictionary<string, List<Cave>> graph, 
             KeySafeDictionary<string, int> visited, List<string> currentPath, bool canRevisitSmall, Counter counter)
        {
            if (cave == Cave.End)
            {
                counter.Value++;
                return;
            }

            visited[cave]++;

            foreach (var next in graph[cave])
            {
                var isBigOrNew = !next.IsSmall || visited[next.Name] < 1;
                if (!next.IsStart && (canRevisitSmall || isBigOrNew))
                {
                    currentPath.Add(next.Name);
                    FindPaths(next.Name, graph, visited, currentPath, canRevisitSmall && isBigOrNew, counter);
                    currentPath.RemoveAt(currentPath.Count - 1);
                }
            }

            visited[cave]--;
        }

        private KeySafeDictionary<string, List<Cave>> CreateGraph(string[] inputs)
        {
            var cache = new KeySafeDictionary<string, Cave>();
            var graph = new KeySafeDictionary<string, List<Cave>>();
            foreach (var input in inputs)
            {
                var path = input.Split('-');

                if (cache[path[0]] == null) cache[path[0]] = new Cave(path[0]);
                if (cache[path[1]] == null) cache[path[1]] = new Cave(path[1]);

                if (graph[path[0]] == null) graph[path[0]] = new List<Cave>();
                if (graph[path[1]] == null) graph[path[1]] = new List<Cave>();

                graph[path[0]].Add(cache[path[1]]);
                graph[path[1]].Add(cache[path[0]]);
            }
            return graph;
        }
    }
}
