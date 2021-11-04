using System;

namespace Algorithms_Specialization.Divide_and_Conquer_Sorting_and_Searching_and_Randomized_Algorithms
{
    public enum PivotPartitionEnum
    {
        FirstElement,
        LastElement,
        MedianOfThree
    }

    public class Assignment3
    {
        public static void SolveProblem(PivotPartitionEnum pivotPartition = PivotPartitionEnum.MedianOfThree)
        {
            var arr1 = Helpers.GetArrayIntFromFile(@"./QuickSort.txt");
            var c = new Assignment3().QuickSort(arr1, 0, arr1.Length - 1, PivotPartitionEnum.MedianOfThree);
            Console.WriteLine($"Total comparisions = {c}");
        }

        public int QuickSort(int[] arr, int left, int right, PivotPartitionEnum pivotPartition = PivotPartitionEnum.MedianOfThree)
        {
            if (left >= right) return 0;
            var c = right - left;
            var p = pivotPartition switch
            {
                PivotPartitionEnum.FirstElement => PivotPartitionUsingFirstElement(arr, left, right),
                PivotPartitionEnum.LastElement => PivotPartitionUsingLastElement(arr, left, right),
                _ => PivotPartitionUsingMedianOfThreeElement(arr, left, right)
            };
            c += QuickSort(arr, left, p - 1, pivotPartition);
            c += QuickSort(arr, p + 1, right, pivotPartition);
            return c;
        }

        private int PivotPartitionUsingMedianOfThreeElement(int[] arr, int left, int right)
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

        private int PivotPartitionUsingLastElement(int[] arr, int left, int right)
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

        private int PivotPartitionUsingFirstElement(int[] arr, int left, int right)
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

    }
}