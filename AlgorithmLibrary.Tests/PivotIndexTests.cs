using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static AlgorithmLibrary.PivotIndex;

namespace AlgorithmLibrary.Tests
{
    [TestClass]
    public class PivotIndexTests
    {
        [DataTestMethod]
        [DynamicData(nameof(PivotIndexesTestData))]
        public void GetPivotIndexes_Returns_Correct_Indexes(int[] arr, int[] expected)
        {
            var actual = GetPivotIndexes(arr);

            Assert.AreEqual(string.Join(",", expected), string.Join(",", actual));
            CollectionAssert.AreEqual(expected, actual);
        }

        private static IEnumerable<object[]> PivotIndexesTestData
        {
            get
            {
                yield return new object[] { null, new[] { -1 } };
                yield return new object[] { new[] { 1, 1 }, new[] { -1 } };
                yield return new object[] { new[] { 1, 3, 2 }, new[] { -1 } };
                yield return new object[] { new[] { 1, 2, 3, 1, 1, 1, 2, 2, 1 }, new[] { -1 } };
                yield return new object[] { new[] { 1 }, new[] { 0 } };
                yield return new object[] { new[] { 3, 2, 1, 2 }, new[] { 1 } };
                yield return new object[] { new[] { 1, 2, 0, 3 }, new[] { 2 } };
                yield return new object[] { new[] { 1, 2, 1, 2, 1, 6 }, new[] { 4 } };
                yield return new object[] { new[] { 2, 3, 4, 1, 4, 5 }, new[] { 3 } };
                yield return new object[] { new[] { 1, 2, 6, 4, 0, -1 }, new[] { 2 } };
                yield return new object[] { new[] { 2, 1, 3, 1, 9, 2, 2, 1, 2 }, new[] { 4 } };
                yield return new object[] { new[] { 1, 2, 3, 0, 0, 3, 2, 1 }, new[] { 3, 4 } };
            }
        }

    }
}
