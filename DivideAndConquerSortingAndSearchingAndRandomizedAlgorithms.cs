using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Algorithms_Specialization
{
    public class DivideAndConquerSortingAndSearchingAndRandomizedAlgorithms
    {
        private static readonly BigInteger TEN = new BigInteger(10);

        public static BigInteger KaratsubaMutiply(BigInteger lhs, BigInteger rhs)
        {
            if (lhs < 10 || rhs < 10)
                return lhs * rhs;

            var m = Math.Min(lhs.ToString().Length, rhs.ToString().Length);
            var m2 = m / 2;

            var x = BigInteger.Pow(TEN, m2);
            var h1 = lhs / x;
            var l1 = lhs % x;
            var h2 = rhs / x;
            var l2 = rhs % x;

            var z0 = KaratsubaMutiply(l1, l2);
            var z1 = KaratsubaMutiply((l1 + h1), (l2 + h2));
            var z2 = KaratsubaMutiply(h1, h2);

            return (z2 * BigInteger.Pow(TEN, m2 * 2)) + ((z1 - z2 - z0) * BigInteger.Pow(TEN, m2)) + z0;
        }

        public static long CountInversion(int[] arr, int[] temp, int left, int right)
        {
            var c = 0L;
            if (left < right)
            {
                var mid = (left + right) / 2;
                c += CountInversion(arr, temp, left, mid);
                c += CountInversion(arr, temp, mid + 1, right);
                c += MergeAndCountSplitInversion(arr, temp, left, mid + 1, right);
            }
            return c;
        }

        private static long MergeAndCountSplitInversion(int[] arr, int[] temp, int left, int mid, int right)
        {
            var i = left; var j = mid; var k = left; var c = 0L;

            while ((i <= mid - 1) && j <= right)
            {
                if (arr[i] <= arr[j])
                {
                    temp[k++] = arr[i++];
                }
                else
                {
                    temp[k++] = arr[j++];
                    c += (mid - i);
                }
            }

            while (i <= mid - 1)
                temp[k++] = arr[i++];

            while (j <= right)
                temp[k++] = arr[j++];

            for (i = left; i <= right; i++)
                arr[i] = temp[i];

            return c;
        }

        public static int QuickSort(int[] arr, int left, int right)
        {
            if (left >= right) return 0;
            var c = right - left;
            // var p = PivotPartitionUsingFirstElement(arr, left, right); 
            // var p = PivotPartitionUsingLastElement(arr, left, right); 
            var p = PivotPartitionUsingMedianOfThreeElement(arr, left, right);
            c += QuickSort(arr, left, p - 1);
            c += QuickSort(arr, p + 1, right);
            return c;
        }

        private static int PivotPartitionUsingMedianOfThreeElement(int[] arr, int left, int right)
        {
            var mid = (right + left) / 2;
            var median = -1;

            // Reference link: https://stackoverflow.com/questions/1582356/fastest-way-of-finding-the-middle-value-of-a-triple/14676309#14676309 

            if ((arr[left] - arr[mid]) * (arr[right] - arr[left]) >= 0)
                median = left;
            else if ((arr[mid] - arr[left]) * (arr[right] - arr[mid]) >= 0)
                median = mid;
            else
                median = right;

            Swap(ref arr[left], ref arr[median]);
            var p = arr[left];
            var i = left + 1;
            for (var j = left + 1; j <= right; j++)
            {
                if (arr[j] < p)
                {
                    Swap(ref arr[j], ref arr[i]);
                    ++i;
                }
            }
            Swap(ref arr[left], ref arr[i - 1]);
            return i - 1;
        }

        private static int PivotPartitionUsingLastElement(int[] arr, int left, int right)
        {
            Swap(ref arr[left], ref arr[right]);
            var p = arr[left];
            var i = left + 1;
            for (var j = left + 1; j <= right; j++)
            {
                if (arr[j] < p)
                {
                    Swap(ref arr[j], ref arr[i]);
                    ++i;
                }
            }
            Swap(ref arr[left], ref arr[i - 1]);
            return i - 1;
        }

        private static int PivotPartitionUsingFirstElement(int[] arr, int left, int right)
        {
            var p = arr[left];
            var i = left + 1;
            for (var j = left + 1; j <= right; j++)
            {
                if (arr[j] < p)
                {
                    Swap(ref arr[j], ref arr[i]);
                    ++i;
                }
            }
            Swap(ref arr[left], ref arr[i - 1]);
            return i - 1;
        }

        private static void Swap(ref int a, ref int b)
        {
            var temp = a;
            a = b;
            b = temp;
        }

        /// <summary>
        /// Compute minimum cut of a connected graph by using the randomized contraction algorithm <br/>
        /// aka : <see href="https://en.wikipedia.org/wiki/Karger%27s_algorithm">Karger's algorithm</see>
        /// </summary>
        /// <returns>A minimum cut of a connected graph.</returns>
        public static int ComputeMinCut(Dictionary<int, List<int>> graph)
        {
            var random = new Random();
            var graphCopy = graph.ToDictionary(item => item.Key, item => item.Value.ToList());

            while (graphCopy.Count > 2)
            {
                var u = graphCopy.Keys.ElementAt(random.Next(0, graphCopy.Keys.Count));
                var v = graphCopy[u].ElementAt(random.Next(0, graphCopy[u].Count));
                if (!graphCopy.ContainsKey(v))
                    continue;
                Contract(graphCopy, u, v);
            }
            return graphCopy.First().Value.Count;
        }

        private static void Contract(Dictionary<int, List<int>> graph, int u, int v)
        {
            //Merge
            foreach (var edge in graph[v])
            {
                if (!graph[u].Contains(edge) && edge != u)
                    graph[u].Add(edge);
            }

            foreach (var vertex in graph)
            {
                if (vertex.Value.Contains(v))
                {
                    vertex.Value.Remove(v);
                    vertex.Value.Add(u);
                }
            }
            
            graph.Remove(v);
        }
    }
}