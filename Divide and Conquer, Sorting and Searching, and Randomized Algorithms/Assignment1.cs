using System;
using System.Numerics;

namespace Algorithms_Specialization.Divide_and_Conquer_Sorting_and_Searching_and_Randomized_Algorithms
{
    public class Assignment1
    {
        public static void SolveProblem()
        {
            var a = BigInteger.Parse("3141592653589793238462643383279502884197169399375105820974944592");
            var b = BigInteger.Parse("2718281828459045235360287471352662497757247093699959574966967627");
            Console.WriteLine(new Assignment1().KaratsubaMutiply(a, b));
        }

        private readonly BigInteger TEN = new BigInteger(10);

        public BigInteger KaratsubaMutiply(BigInteger lhs, BigInteger rhs)
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
    }
}