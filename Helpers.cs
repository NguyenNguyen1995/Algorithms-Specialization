using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Algorithms_Specialization
{
    public class Helpers
    {
        public static Dictionary<int, List<int>> LoadGraphFromFile(string filename)
        {
            var result = new Dictionary<int, List<int>>();
            using (var file = new StreamReader(filename))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    var vertices = line.Trim().Split().Select(n => int.Parse(n)).ToList();
                    result[vertices[0]] = vertices.Skip(1).ToList();
                }
            }
            return result;
        }

        public static void PrintGraph(Dictionary<int, List<int>> graph)
        {
            foreach (var item in graph)
            {
                Console.Write($"{item.Key} => ");
                Console.Write("( ");
                foreach (var n in item.Value)
                {
                    Console.Write($"{n} ");
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