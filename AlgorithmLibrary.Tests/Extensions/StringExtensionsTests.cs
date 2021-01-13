using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlgorithmLibrary.Extensions;

namespace AlgorithmLibrary.Tests.Extensions
{
    [TestClass]
    public class StringExtensionsUnitTests
    {
        [TestCategory("SplitToKeyValuePair")]
        [TestMethod]
        [DynamicData(nameof(SplitToKeyValuePairTestData))]
        public void SplitToKeyValuePair_Returns_ExpectedResult(string text, string separator,
            (string, string) expected)
        {
            var actual = text.SplitToKeyValuePair(separator);

            Assert.AreEqual(expected, actual);
        }

        private static IEnumerable<object[]> SplitToKeyValuePairTestData
        {
            get
            {
                yield return new object[] {"2:Group2Value", ":", ("2", "Group2Value")};
                yield return new object[] {"2||Group2Value", "||", ("2", "Group2Value")};
                yield return new object[] {"2|:|Group2Value", "|:|", ("2", "Group2Value")};
                yield return new object[] {"|:|", "|:|", ("", "")};
                yield return new object[] {"2|:|", "|:|", ("2", "")};
                yield return new object[] {"|:|100", "|:|", ("", "100")};
            }
        }

        [TestCategory("SplitToKeyValuePair")]
        [TestMethod]
        [DataRow("abc", ":")]
        [DataRow("a:b:c", ":")]
        [DataRow("a::b::c", "::")]
        [DataRow("", ":")]
        [DataRow("   ", ":")]
        [DataRow(null, ":")]
        [DataRow("1:100", "")]
        [DataRow("1:100", "=")]
        [DataRow("1:100", null)]
        public void SplitToKeyValuePair_Throws_ArgumentException(string text, string separator)
        {
            Assert.ThrowsException<ArgumentException>(() => text.SplitToKeyValuePair(separator));
        }

        [TestMethod]
        [DynamicData(nameof(ToDictionaryTestData))]
        public void ToDictionary_Returns_ExpectedResult(string text, string keyValueSeparator, string itemSeparator,
            Dictionary<string, string> expected)
        {
            var actual = text.ToDictionary(keyValueSeparator, itemSeparator);

            CollectionAssert.AreEqual(expected, actual);
        }

        public static IEnumerable<object[]> ToDictionaryTestData
        {
            get
            {
                yield return new object[]
                {
                    "1::100", "::", "|", new Dictionary<string, string> { ["1"] = "100" }
                };
                yield return new object[]
                {
                    "1::100||2::200", "::", "||", new Dictionary<string, string> { ["1"] = "100", ["2"] = "200" }
                };
                yield return new object[]
                {
                    "1:100", ":", string.Empty, new Dictionary<string, string> { ["1"] = "100" }
                };
                yield return new object[]
                {
                    "1:100,2:200", ":", ",", new Dictionary<string, string> { ["1"] = "100", ["2"] = "200" }
                };
                yield return new object[]
                {
                    "1:,2:200", ":", ",", new Dictionary<string, string> { ["1"] = string.Empty, ["2"] = "200" }
                };
                yield return new object[]
                {
                    ":100,2:200", ":", ",", new Dictionary<string, string> { ["2"] = "200" }
                };
                yield return new object[]
                {
                    "1:100,2:200,", ":", ",", new Dictionary<string, string> { ["1"] = "100", ["2"] = "200" }
                };
                yield return new object[]
                {
                    ",2:200,3:300,", ":", ",", new Dictionary<string, string> { ["2"] = "200", ["3"] = "300" }
                };
                yield return new object[]
                {
                    string.Empty, ":", ",", new Dictionary<string, string>()
                };
                yield return new object[]
                {
                    null, ":", ",", new Dictionary<string, string>()
                };
                yield return new object[]
                {
                    string.Empty, string.Empty, string.Empty, new Dictionary<string, string>()
                };
                yield return new object[]
                {
                    null, null, string.Empty, new Dictionary<string, string>()
                };
                yield return new object[]
                {
                    null, null, null, new Dictionary<string, string>()
                };
                yield return new object[]
                {
                    "1::100::101,2::200", "::", ",", new Dictionary<string, string> { ["1"] = "100::101", ["2"] = "200" }
                };                
            }
        }

    }
}
