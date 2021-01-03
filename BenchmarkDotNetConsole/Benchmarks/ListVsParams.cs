using System.Collections.Generic;
using AlgorithmLibrary;
using BenchmarkDotNet.Attributes;

namespace BenchmarkDotNetConsole.Benchmarks
{
    public class ListVsParams : BenchmarkBase
    {
        private static double GetMedian(IReadOnlyList<int> nums)
        {
            int mid = nums.Count / 2;
            return nums.Count.IsEven() ? (nums[mid] + nums[mid - 1]) / 2.0 : nums[mid];
        }

        private static double GetMedian(params int[] nums)
        {
            int mid = nums.Length / 2;
            return nums.Length.IsEven() ? (nums[mid] + nums[mid - 1]) / 2.0 : nums[mid];
        }

        [Benchmark]
        public double GetMedianIReadOnlyList() => GetMedian(new List<int> {1, 2, 3, 4, 5, 6, 7, 8, 9, 10});

        [Benchmark]
        public double GetMedianParams() => GetMedian(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
    }
}
