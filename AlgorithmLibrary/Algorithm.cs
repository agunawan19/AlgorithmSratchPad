using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmLibrary
{
    public static class Algorithm
    {
        public static int[] GetPivotIndexes(in int[] arr)
        {
            var pivotIndexes = new List<int>();

            if (arr.Length == 1)
            {
                pivotIndexes.Add(0);
                return pivotIndexes.ToArray();
            }

            if (arr.Length < 3)
            {
                pivotIndexes.Add(-1);
                return pivotIndexes.ToArray();
            }

            var leftSum = 0;
            var rightSum = 0;

            for (int i = 1; i < arr.Length; i++)
            {
                rightSum += arr[i];
            }

            for (int i = 0, j = 1; j < arr.Length; i++, j++)
            {
                rightSum -= arr[j];
                leftSum += arr[i];

                if (leftSum == rightSum)
                {
                    pivotIndexes.Add(j);
                }
            }

            if (pivotIndexes.Count == 0)
            {
                pivotIndexes.Add(-1);
            }

            return pivotIndexes.ToArray();
        }

        public static bool IntersectWith(this int[] reference, in int[] other) =>
            reference[0] < other[1] && reference[1] > other[0];

        public static bool IntersectWith(
            this in ValueTuple<int, int> reference,
            in ValueTuple<int, int> other) =>
            reference.Item1 < other.Item2 && reference.Item2 > other.Item1;

        public static bool IntersectWith(
            this in ValueTuple<decimal, decimal> reference,
            ValueTuple<decimal, decimal> other) =>
            reference.Item1 < other.Item2 && reference.Item2 > other.Item1;

        public static bool IntersectWith(
            this in ValueTuple<TimeSpan, TimeSpan> reference,
            in ValueTuple<TimeSpan, TimeSpan> other) =>
            reference.Item1 < other.Item2 && reference.Item2 > other.Item1;

        public static int[] GetIntersectionValuePair(in int[] reference, in int[] other)
        {
            if (!reference.IntersectWith(other)) return new int[] { };

            var (start, end) = (
                reference[0].CompareTo(other[0]) == -1 ? other[0] : reference[0],
                reference[1].CompareTo(other[1]) == -1 ? reference[1] : other[1]
            );

            return new[] { start, end };
        }

        public static TimeSpan[] GetIntersectionValuePair(
            ValueTuple<TimeSpan, TimeSpan> reference,
            ValueTuple<TimeSpan, TimeSpan> other)
        {
            if (!reference.IntersectWith(other)) return new TimeSpan[] { };

            var (from, to) = (
                reference.Item1.CompareTo(other.Item1) == -1 ? other.Item1 : reference.Item1,
                reference.Item2.CompareTo(other.Item2) == -1 ? reference.Item2 : other.Item2
            );

            return new[] { from, to };
        }

        public static int[][] GetIntersections(in int[][][] arr)
        {
            if (arr.Length == 1) return arr[0];

            int[][] intersections = null;

            for (int first = 0, next = 1; next < arr.Length; next++)
            {
                var references = intersections ?? arr[first];
                var others = arr[next];

                intersections = GetReferencesAndOthersIntersections(references, others);

                if (intersections.Length == 0) break;
            }

            return intersections ?? new int[][] { };
        }

        private static int[][] GetReferencesAndOthersIntersections(
            IEnumerable<int[]> references, IEnumerable<int[]> others) =>
            (from reference in references
             from other in others
             where reference.IntersectWith(other)
             select GetIntersectionValuePair(reference, other)
            ).ToArray();
    }
}