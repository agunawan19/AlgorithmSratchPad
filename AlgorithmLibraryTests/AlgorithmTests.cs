using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlgorithmLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AlgorithmLibrary.Algorithm;

namespace AlgorithmLibrary.Tests
{
    [TestClass()]
    public class AlgorithmTests
    {
        [DataTestMethod()]
        [DynamicData(nameof(PivotIndexesTestData), DynamicDataSourceType.Property)]
        public void GetPivotIndexesTest(int[] arr, int[] expected)
        {
            var actual = GetPivotIndexes(arr);

            Assert.AreEqual(string.Join(",", expected), string.Join(",", actual));
            CollectionAssert.AreEqual(expected, actual);
        }

        public static IEnumerable<object[]> PivotIndexesTestData
        {
            get
            {
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

        [DataTestMethod()]
        [DynamicData(nameof(IntersecWithTestData), DynamicDataSourceType.Property)]
        public void IntersecWith_Returns_Correct_Result(int[] reference, int[] other, bool expected)
        {
            var actual = reference.IntersecWith(other);
            Assert.AreEqual(expected, actual);
        }

        public static IEnumerable<object[]> IntersecWithTestData
        {
            get
            {
                yield return new object[] { new[] { 1, 2 }, new[] { 1, 2 }, true };
                yield return new object[] { new[] { 1, 3 }, new[] { 2, 3 }, true };
                yield return new object[] { new[] { 1, 5 }, new[] { 2, 3 }, true };
                yield return new object[] { new[] { 2, 3 }, new[] { 1, 5 }, true };
                yield return new object[] { new[] { 1, 2 }, new[] { 2, 3 }, false };
                yield return new object[] { new[] { 1, 2 }, new[] { 4, 5 }, false };
            }
        }

        [DataTestMethod()]
        [DynamicData(nameof(GetIntersectionValuePairTestData), DynamicDataSourceType.Property)]
        public void GetIntersectionValuePair_Returns_Correct_Result(int[] reference, int[] other, int[] expected)
        {
            var actual = GetIntersectionValuePair(reference, other);
            Assert.AreEqual(string.Join(",", expected), string.Join(",", actual), $"{string.Join(",", reference)} <-> {string.Join(",", other)}");
        }

        public static IEnumerable<object[]> GetIntersectionValuePairTestData
        {
            get
            {
                yield return new object[] { new[] { 1, 2 }, new[] { 1, 2 }, new[] { 1, 2 } };
                yield return new object[] { new[] { 1, 3 }, new[] { 2, 3 }, new[] { 2, 3 } };
                yield return new object[] { new[] { 1, 5 }, new[] { 2, 3 }, new[] { 2, 3 } };
                yield return new object[] { new[] { 2, 5 }, new[] { 1, 5 }, new[] { 2, 5 } };
                yield return new object[] { new[] { 1, 2 }, new[] { 2, 3 }, new int[] { } };
                yield return new object[] { new[] { 1, 2 }, new[] { 4, 5 }, new int[] { } };
            }
        }

        [DataTestMethod()]
        [DynamicData(nameof(GetIntersectionsTestData), DynamicDataSourceType.Property)]
        public void GetIntersections_Returns_Correct_Result(int[][][] arr, int[][] expected)
        {
            var actual = GetIntersections(arr);

            var processedActual = actual.Aggregate(
                string.Empty,
                (acc, x) => $"{acc}[{string.Join(",", x)}],",
                allStr => allStr.TrimEnd(new[] { ',' }));
            var processedExpected = expected.Aggregate(
                string.Empty,
                (acc, x) => $"{acc}[{string.Join(",", x)}],",
                allStr => allStr.TrimEnd(new[] { ',' }));

            Assert.AreEqual(processedExpected, processedActual);
        }

        public static IEnumerable<object[]> GetIntersectionsTestData
        {
            get
            {
                yield return new object[]
                {
                    new[]
                    {
                        new[] { new[] { 6, 10 }, new[] { 11, 13 } },
                        new[] { new[] { 7, 14 } },
                        new[] { new[] { 7, 8 }, new[] { 10, 12 } },
                    },
                    new[]
                    {
                        new[] { 7, 8 }, new[] { 11, 12 }
                    }
                };

                yield return new object[]
                {
                    new[]
                    {
                        new[] { new[] { 7, 14 } },
                        new[] { new[] { 6, 10 }, new[] { 11, 13 } },
                        new[] { new[] { 7, 8 }, new[] { 10, 12 } },
                    },
                    new[]
                    {
                        new[] { 7, 8 }, new[] { 11, 12 }
                    }
                };

                yield return new object[]
                {
                    new[]
                    {
                        new[] { new[] { 7, 14 } },
                        new[] { new[] { 6, 10 }, new[] { 11, 13 } },
                        new[] { new[] { 7, 8 }, new[] { 10, 12 } },
                        new[] { new[] { 11, 12} }
                    },
                    new[]
                    {
                        new[] { 11, 12 }
                    }
                };

                yield return new object[]
                {
                    new[]
                    {
                        new[] { new[] { 7, 14 } },
                        new[] { new[] { 6, 10 }, new[] { 11, 13 } },
                        new[] { new[] { 7, 8 }, new[] { 10, 12 } },
                        new[] { new[] { 15, 17} }
                    },
                    new int[][] { }
                };


                yield return new object[]
                {
                    new[]
                    {
                        new[] { new[] { 7, 14 } },
                        new[] { new[] { 6, 10 }, new[] { 11, 13 } },
                        new[] { new[] { 15, 17} },
                        new[] { new[] { 7, 8 }, new[] { 10, 12 } },
                    },
                    new int[][] { }
                };

                yield return new object[]
                {
                    new[]
                    {
                        new[] { new[] { 7, 14 } },
                        new[] { new[] { 6, 10 }, new[] { 11, 13 } },
                        new int[][] { },
                    },
                    new int[][] { }
                };

                yield return new object[]
                {
                    new[]
                    {
                        new int[][] {  },
                        new[] { new[] { 7, 14 } },
                        new[] { new[] { 6, 10 }, new[] { 11, 13 } },
                    },
                    new int[][] { }
                };

                yield return new object[]
                {
                    new[]
                    {
                        new[] { new[] { 7, 14 } },
                        new[] { new[] { 6, 10 }, new[] { 11, 13 } },
                    },
                    new[]
                    {
                        new[] { 7, 10 }, new[] {11, 13 }
                    }
                };

                yield return new object[]
                {
                    new[]
                    {
                        new[] { new[] { 7, 14 } },
                    },
                    new int[][] { }
                };
            }
        }
    }
}