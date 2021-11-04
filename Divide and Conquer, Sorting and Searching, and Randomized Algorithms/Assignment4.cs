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
            var graph = Helpers.LoadGraphFromFile(@"./kargerMinCut.txt");
            // Helpers.PrintGraph(graph);

            var cuts = new List<int>();
            for (var i = 0; i < 200; i++)
            {
                var result = new Assignment4().ComputeMinCut(graph);
                cuts.Add(result);
            }

            Console.WriteLine($"Min cut is {cuts.Min()}");
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
                var keyVertices = graphCopy.Keys.ToList();
                var baseVertex = keyVertices[_random.Next(keyVertices.Count)];
                var mergeVertex = graphCopy[baseVertex].ElementAt(_random.Next(graphCopy[baseVertex].Count));

                if (baseVertex == mergeVertex)
                    continue;

                Contract(graphCopy, baseVertex, mergeVertex);
            }
            // They are symmetric so use both case is ok
            return graphCopy.First().Value.Count;
        }

        private void Contract(Dictionary<int, List<int>> graph, int baseVertex, int mergeVertex)
        {
            // contract between base and vertex wanted to merge
            graph[baseVertex].AddRange(graph[mergeVertex].Where(v => v != baseVertex));
            // Remove self loop
            graph[baseVertex].RemoveAll(v => v == mergeVertex);

            foreach (var vertex in graph)
            {
                // skip base and merge vertex
                if(vertex.Key == mergeVertex || vertex.Key == baseVertex)
                    continue;

                var index = -1;
                do
                {
                    index = vertex.Value.IndexOf(mergeVertex);
                    if (index != -1)
                        vertex.Value[index] = baseVertex;
                } while (index != -1);
            }

            // remove vertex after merge
            graph.Remove(mergeVertex);
        }
    }
}