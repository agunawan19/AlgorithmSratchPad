using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgorithmLibrary.Tests
{
    [TestClass]
    public class BestTimeToBuyAndSellStock1Tests
    {
        [TestMethod]
        [DynamicData(nameof(MaxProfitTestData))]
        public void MaxProfitBruteForce_Returns_ExpectedResults(int[] prices, int expected)
        {
            var actual = BestTimeToBuyAndSellStock1.MaxProfitBruteForce(prices);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DynamicData(nameof(MaxProfitTestData))]
        public void MaxProfitOnePass_Returns_ExpectedResults(int[] prices, int expected)
        {
            var actual = BestTimeToBuyAndSellStock1.MaxProfitOnePass(prices);
            Assert.AreEqual(expected, actual);
        }

        private static IEnumerable<object[]> MaxProfitTestData
        {
            get
            {
                yield return new object[] { new[] { 7, 1, 5, 3, 6, 4 }, 5 };
                yield return new object[] { new[] { 7, 6, 4, 3, 1 }, 0 };
            }
        }
    }
}
