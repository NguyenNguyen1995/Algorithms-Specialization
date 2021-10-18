using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Algorithms_Specialization
{
    public class Helpers
    {
        public static List<List<int>> LoadGraphFromFile(string filename)
        {
            var result = new List<List<int>>();
            using (var file = new StreamReader(filename))
            {
                string line;
                int counter = 0;
                while ((line = file.ReadLine()) != null)
                {
                    result.Add(line.Trim().Split("\t").Skip(1).Select(n => int.Parse(n)).ToList());
                    counter++;
                }
            }
            return result;
        }

        public static void PrintGraph(List<List<int>> graph)
        {
            for (var i = 0; i < graph.Count; i++)
            {
                Console.Write($"{i + 1} => ");
                Console.Write("( ");
                foreach (var item in graph[i])
                {
                    Console.Write($"{item} ");
                }
                Console.WriteLine(")");
            }
        }

        public static int[] GetArrayIntFromFile(string filename)
        {
            return Array.ConvertAll(File.ReadAllLines(filename), int.Parse);
        }
    }
}