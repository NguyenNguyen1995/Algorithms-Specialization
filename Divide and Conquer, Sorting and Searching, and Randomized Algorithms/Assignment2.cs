using System;

namespace Algorithms_Specialization.Divide_and_Conquer_Sorting_and_Searching_and_Randomized_Algorithms
{
    public class Assignment2
    {
        public static void SolveProblem()
        {
            var arr = Helpers.GetArrayIntFromFile(@"./IntegerArray.txt");
            var result = new Assignment2().CountInversion(arr, new int[arr.Length], 0, arr.Length - 1);
            Console.WriteLine($"Total inversion pair in file IntegerArray.txt: {result}");
        }

        public long CountInversion(int[] arr, int[] temp, int left, int right)
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

        private long MergeAndCountSplitInversion(int[] arr, int[] temp, int left, int mid, int right)
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
    }
}