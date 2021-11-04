using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms_Specialization.Divide_and_Conquer_Sorting_and_Searching_and_Randomized_Algorithms
{
    public class Assignment4
    {
        private readonly Random _random = new Random();

        public static void SolveProblem()
        {
            var graph = Helpers.LoadGraphFromFile(@"./kargerMinCut1.txt");
            // Helpers.PrintGraph(graph);
            var min = int.MaxValue;

            for (var i = 0; i < 10; i++)
            {
                var result = new Assignment4().ComputeMinCut(graph);
                min = Math.Min(result, min);
            }
            
            Console.WriteLine($"min cut = {min}");
        }

        /// <summary>
        /// Compute minimum cut of a connected graph by using the randomized contraction algorithm <br/>
        /// aka : <see href="https://www.geeksforgeeks.org/kargers-algorithm-for-minimum-cut-set-1-introduction-and-implementation/">Karger's algorithm</see>
        /// </summary>
        /// <returns>A minimum cut of a connected graph.</returns>
        public int ComputeMinCut(Dictionary<int, List<int>> graph)
        {
            var graphCopy = graph.ToDictionary(item => item.Key, item => item.Value.ToList());

            while (graphCopy.Count > 2)
            {
                var u = graphCopy.Keys.ElementAt(_random.Next(0, graphCopy.Keys.Count));
                var v = graphCopy[u].ElementAt(_random.Next(0, graphCopy[u].Count));
                Contract(graphCopy, u, v);
            }
            // TODO:
            return Math.Max(graphCopy.First().Value.Count, graphCopy.Last().Value.Count);
        }

        private void Contract(Dictionary<int, List<int>> graph, int u, int v)
        {
            foreach (var edge in graph[v])
            {
                if (!graph[u].Contains(edge))
                    graph[u].Add(edge);
            }

            foreach (var vertex in graph)
            {
                var index = -1;
                do
                {
                    index = vertex.Value.IndexOf(v);
                    if (index != -1)
                        vertex.Value[index] = u;
                } while (index != -1);
            }

            //Remove Self Loops
            graph[u].RemoveAll(_ => _ == u);
            graph.Remove(v);
        }
    }
}