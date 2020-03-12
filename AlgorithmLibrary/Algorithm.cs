using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmLibrary
{
    public static class Algorithm
    {
        public static int[] GetPivotIndexes(int[] arr)
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

            //var total = 0;
            //var sumAt = new List<int>();
            //var leftSum = 0;
            //var rightSum = 1;

            //for (int i = 0; i < arr.Length; i++)
            //{
            //    total += arr[i];
            //    sumAt.Add(total);
            //}

            //for (int i = 0; i < arr.Length; i++)
            //{
            //    leftSum = sumAt[i] - arr[i];
            //    rightSum = total - sumAt[i];

            //    if (leftSum == rightSum)
            //    {
            //        pivotIndexes.Add(i);
            //    }
            //}

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

        public static bool IntersecWith(this int[] reference, int[] other) =>
            reference[0] < other[1] && reference[1] > other[0];

        public static bool IntersecWith(this ValueTuple<int, int> reference, ValueTuple<int, int> other) =>
            reference.Item1 < other.Item2 && reference.Item2 > other.Item1;

        public static bool IntersecWith(this ValueTuple<decimal, decimal> reference, ValueTuple<decimal, decimal> other) =>
            reference.Item1 < other.Item2 && reference.Item2 > other.Item1;

        public static int[] GetIntersectionValuePair(int[] reference, int[] other)
        {
            if (!reference.IntersecWith(other)) return new int[] { };

            var (start, end) = (
                reference[0].CompareTo(other[0]) == -1 ? other[0] : reference[0],
                reference[1].CompareTo(other[1]) == -1 ? reference[1] : other[1]
            );

            return new[] { start, end };
        }

        public static int[][] GetIntersections(int[][][] arr)
        {
            List<int[]> intersections = null;

            for (int first = 0, next = 1; next < arr.Length; first++, next++)
            {
                var references = intersections?.ToArray() ?? arr[first];
                intersections = new List<int[]>();
                var others = arr[next];

                foreach (var reference in references)
                {
                    foreach (var other in others)
                    {
                        if (reference.IntersecWith(other))
                        {
                            intersections.Add(GetIntersectionValuePair(reference, other));
                        }
                    }
                }

                if (intersections.Count == 0)
                {
                    break;
                }
            }

            return intersections?.ToArray() ?? new int[][] { };
        }
    }
}
