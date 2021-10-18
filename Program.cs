using System;
using System.Numerics;

namespace Algorithms_Specialization
{
    class Program
    {
        static void Main(string[] args)
        {
            // Karatsuba algorithm test
            // var a = BigInteger.Parse("3141592653589793238462643383279502884197169399375105820974944592");
            // var b = BigInteger.Parse("2718281828459045235360287471352662497757247093699959574966967627");
            // Console.WriteLine(DivideAndConquerSortingAndSearchingAndRandomizedAlgorithms.KaratsubaMutiply(a, b));

            // Count Inversion Test
            // var arr = Helpers.GetArrayIntFromFile(@"./IntegerArray.txt");
            // var result = DivideAndConquerSortingAndSearchingAndRandomizedAlgorithms
            //     .CountInversion(arr, new int[arr.Length], 0, arr.Length - 1);
            // Console.WriteLine($"Total inversion pair in file IntegerArray.txt: {result}");

            // Quick sort total comparasions 
            // var arr1 = Helpers.GetArrayIntFromFile(@"./QuickSort.txt");
            // var c = DivideAndConquerSortingAndSearchingAndRandomizedAlgorithms.QuickSort(arr1, 0, arr1.Length - 1);
            // Console.WriteLine($"Total comparisions = {c}");

            // Compute Min cuts of given graph
            var graph = Helpers.LoadGraphFromFile(@"./KargerMinCut.txt");
            Helpers.PrintGraph(graph);
            DivideAndConquerSortingAndSearchingAndRandomizedAlgorithms.ComputeMinCut(graph);
        }
    }
}
