using System.Collections.Generic;
using System.Linq;

namespace AlgorithmLibrary
{
    public static class PivotIndex
    {
        public static int[] GetPivotIndexes(int[] numbers)
        {
            var notFoundResult = new[] { -1 };
            if (numbers is null) return notFoundResult;

            switch (numbers.Length)
            {
                case 1:
                    return new int[1];
                case < 3:
                    return notFoundResult;
            }

            var pivotIndexes = new List<int>();
            var leftSum = 0;
            var total = numbers.Sum();

            for (var i = 0; i < numbers.Length; i++)
            {
                if (leftSum == total - leftSum - numbers[i])
                    pivotIndexes.Add(i);

                leftSum += numbers[i];
            }

            return pivotIndexes.Count == 0 ? notFoundResult : pivotIndexes.ToArray();
        }

    }
}
