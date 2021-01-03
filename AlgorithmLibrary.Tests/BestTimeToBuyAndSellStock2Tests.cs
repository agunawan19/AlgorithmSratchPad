using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgorithmLibrary.Tests
{
    [TestClass]
    public class BestTimeToBuyAndSellStock2Tests
    {
        [TestMethod]
        [DynamicData(nameof(MaxProfitTestData))]
        public void MaxProfitBruteForce_Returns_ExpectedResults(int[] prices, int expected)
        {
            var actual = BestTimeToBuyAndSellStock2.MaxProfitBruteForce(prices);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DynamicData(nameof(MaxProfitTestData))]
        public void MaxProfitPeakValleyApproach_Returns_ExpectedResults(int[] prices, int expected)
        {
            var actual = BestTimeToBuyAndSellStock2.MaxProfitPeakValleyApproach(prices);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DynamicData(nameof(MaxProfitTestData))]
        public void MaxProfitOnePass_Returns_ExpectedResults(int[] prices, int expected)
        {
            var actual = BestTimeToBuyAndSellStock2.MaxProfitOnePass(prices);
            Assert.AreEqual(expected, actual);
        }

        private static IEnumerable<object[]> MaxProfitTestData
        {
            get
            {
                yield return new object[] { new[] { 7, 1, 5, 3, 6, 4 }, 7 };
                yield return new object[] { new[] { 1, 2, 3, 4, 5 }, 4 };
                yield return new object[] { new[] { 7, 6, 4, 3, 1 }, 0 };
                yield return new object[] { Array.Empty<int>(), 0 };
                yield return new object[] { null, 0 };
            }
        }
    }
}
