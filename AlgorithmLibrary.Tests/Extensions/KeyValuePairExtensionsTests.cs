using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlgorithmLibrary.Extensions;

namespace AlgorithmLibrary.Tests.Extensions
{
    [TestClass]
    public class KeyValuePairExtensionsTests
    {
        [TestMethod]
        public void Deconstruct_Returns_CorrectFirstValue()
        {
            var map = new Dictionary<string, string> {["1"] = "100", ["2"] = "200", ["3"] = "300"};
            const string expectedGroupLevel = "1";
            const string expectedGroupValue = "100";

            var (actualGroupLevel, actualGroupValue) = map.First();
            
            Assert.AreEqual(expectedGroupLevel, actualGroupLevel);
            Assert.AreEqual(expectedGroupValue, actualGroupValue);
        }
        
        [TestMethod]
        public void Deconstruct_Returns_CorrectLastValue()
        {
            var map = new Dictionary<string, string> {["1"] = "100", ["2"] = "200", ["3"] = "300"};
            const string expectedGroupLevel = "3";
            const string expectedGroupValue = "300";

            var (actualGroupLevel, actualGroupValue) = map.Last();
            
            Assert.AreEqual(expectedGroupLevel, actualGroupLevel);
            Assert.AreEqual(expectedGroupValue, actualGroupValue);
        }
        
    }
}
