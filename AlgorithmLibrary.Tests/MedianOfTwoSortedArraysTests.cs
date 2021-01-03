using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgorithmLibrary.Tests
{
    [TestClass]
    public class MedianOfTwoSortedArraysTests
    {
        [TestMethod]
        [DynamicData(nameof(FindMedianOfTwoSortedArraysTestData))]
        public void FindMedianOfTwoSortedArraysTests_Returns_ExpectedResult(int[] nums1, int[] nums2, double expected)
        {
            var actual = MedianOfTwoSortedArrays.FindMedianSortedArrays(nums1, nums2);

            Assert.AreEqual(expected, actual);
        }

        private static IEnumerable<object[]> FindMedianOfTwoSortedArraysTestData
        {
            get
            {
                yield return new object[] { new[] { 1 }, new[] { 2, 3, 4 }, 2.5 };
                yield return new object[] { new[] { 2, 3, 4 }, new[] { 1 }, 2.5 };
                yield return new object[] { new[] { 1, 2, 3 }, new[] { 4, 5 }, 3 };
                yield return new object[] { new[] { 4, 5, 6 }, new[] { 1, 2, 3 }, 3.5 };
                yield return new object[] { new[] { 1, 3 }, new[] { 2 }, 2 };
                yield return new object[] { new[] { 1, 2 }, new[] { 3, 4 }, 2.5 };
                yield return new object[] { new[] { 0, 0 }, new[] { 0, 0 }, 0 };
                yield return new object[] { Array.Empty<int>(), new[] { 1 }, 1 };
                yield return new object[] { new[] { 2 }, Array.Empty<int>(), 2 };
            }
        }

    }
}

