using System.Collections.Generic;

namespace AlgorithmLibrary
{
    public static class MedianOfTwoSortedArrays
    {
        public static double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            var numberCount = nums1.Length + nums2.Length;
            var mid = numberCount / 2;
            var i = 0;
            var j = 0;
            var median = 0;
            var prevMedian = 0;

            for (var k = 0; k <= mid; k++)
            {
                prevMedian = median;
                if (j == nums2.Length)
                    median = nums1[i++];
                else if (i == nums1.Length)
                    median = nums2[j++];
                else
                    median = nums1[i] > nums2[j] ? nums2[j++] : nums1[i++];
            }

            return numberCount.IsEven() ? (median + prevMedian) / 2.0 : median;
        }

        public static double GetMedian(IReadOnlyList<int> nums)
        {
            int mid = nums.Count / 2;
            return nums.Count.IsEven() ? (nums[mid] + nums[mid - 1]) / 2.0 : nums[mid];
        }

        public static double GetMedian(params int[] nums)
        {
            int mid = nums.Length / 2;
            return nums.Length.IsEven() ? (nums[mid] + nums[mid - 1]) / 2.0 : nums[mid];
        }

        public static bool IsEven(this int number) => number % 2 == 0;

    }
}
