using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlgorithmLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AlgorithmLibrary.Algorithm;
using System.Text.Json;
using System.Globalization;

namespace AlgorithmLibrary.Tests
{
    [TestClass]
    public class AlgorithmTests
    {
        private const string Format = @"hh\:mm";
        private static readonly CultureInfo Culture = System.Globalization.CultureInfo.CurrentCulture;

        [DataTestMethod]
        [DynamicData(nameof(PivotIndexesTestData), DynamicDataSourceType.Property)]
        public void GetPivotIndexes_Returns_Correct_Indexes(in int[] arr, in int[] expected)
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

        [DataTestMethod]
        [DynamicData(nameof(IntersectWithTestData), DynamicDataSourceType.Property)]
        public void IntersectWith_Returns_Correct_Result(in int[] reference, in int[] other, in bool expected)
        {
            var actual = reference.IntersectWith(other);
            Assert.AreEqual(expected, actual);
        }

        public static IEnumerable<object[]> IntersectWithTestData
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

        [DataTestMethod]
        [DynamicData(nameof(IntersectWithTimeSpanTestData), DynamicDataSourceType.Property)]
        public void IntersectWith_TimeSpan_Returns_Correct_Result(in TimeSpan[] reference, in TimeSpan[] other, in bool expected)
        {
            var (referenceFrom, referenceTo) = (reference[0], reference[1]);
            var (otherFrom, otherTo) = (other[0], other[1]);

            var actual = (referenceFrom, referenceTo).IntersectWith((otherFrom, otherTo));
            Assert.AreEqual(expected, actual);
        }

        public static IEnumerable<object[]> IntersectWithTimeSpanTestData
        {
            get
            {
                yield return new object[]
                {
                    new[]
                    {
                        TimeSpan.ParseExact("01:30", Format, Culture), TimeSpan.ParseExact("03:30", Format, Culture)
                    },
                    new []
                    {
                        TimeSpan.ParseExact("03:00", Format, Culture), TimeSpan.ParseExact("04:30", Format, Culture)
                    },
                    true
                };

                yield return new object[]
                {
                    new[]
                    {
                        TimeSpan.ParseExact("01:30", Format, Culture), TimeSpan.ParseExact("03:30", Format, Culture)
                    },
                    new []
                    {
                        TimeSpan.ParseExact("03:30", Format, Culture), TimeSpan.ParseExact("04:30", Format, Culture)
                    },
                    false
                };
            }
        }

        [DataTestMethod]
        [DynamicData(nameof(GetIntersectionTimeSpanValuePairTestData), DynamicDataSourceType.Property)]
        public void GetIntersectionTimeSpanValuePair_Returns_Correct_Result(in TimeSpan[] reference, in TimeSpan[] other, in TimeSpan[] expected)
        {
            var actual = GetIntersectionValuePair((reference[0], reference[1]), (other[0], other[1]));

            var jsonString = JsonSerializer.Serialize(actual);

            Assert.AreEqual(string.Join(",", expected), string.Join(",", actual), $"{string.Join(",", reference)} <-> {string.Join(",", other)}");
        }

        public static IEnumerable<object[]> GetIntersectionTimeSpanValuePairTestData
        {
            get
            {
                yield return new object[]
                {
                    new[]
                    {
                        TimeSpan.ParseExact("01:30", Format, Culture), TimeSpan.ParseExact("03:30", Format, Culture)
                    },
                    new []
                    {
                        TimeSpan.ParseExact("03:00", Format, Culture), TimeSpan.ParseExact("04:30", Format, Culture)
                    },
                    new []
                    {
                        TimeSpan.ParseExact("03:00", Format, Culture), TimeSpan.ParseExact("03:30", Format, Culture)
                    }
                };

                yield return new object[]
                {
                    new[]
                    {
                        TimeSpan.ParseExact("01:30", Format, Culture), TimeSpan.ParseExact("03:30", Format, Culture)
                    },
                    new []
                    {
                        TimeSpan.ParseExact("03:30", Format, Culture), TimeSpan.ParseExact("04:30", Format, Culture)
                    },
                    Array.Empty<TimeSpan>()
                };
            }
        }

        [DataTestMethod]
        [DynamicData(nameof(GetIntersectionValuePairTestData), DynamicDataSourceType.Property)]
        public void GetIntersectionValuePair_Returns_Correct_Result(in int[] reference, in int[] other, in int[] expected)
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

        [DataTestMethod]
        [DynamicData(nameof(GetIntersectionsTestData), DynamicDataSourceType.Property)]
        public void GetIntersections_Returns_Correct_Result(in int[][][] arr, in int[][] expected)
        {
            var actual = GetIntersections(arr);

            var processedActual = actual.Aggregate(
                string.Empty,
                (acc, x) => $"{acc}[{string.Join(",", x)}],",
                allStr => allStr.TrimEnd(','));
            var processedExpected = expected.Aggregate(
                string.Empty,
                (acc, x) => $"{acc}[{string.Join(",", x)}],",
                allStr => allStr.TrimEnd(','));

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
                        new int[][] { },
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
                    new[]
                    {
                        new[] { 7, 14 },
                    }
                };
            }
        }

        [DataTestMethod]
        [DynamicData(nameof(GetOpenTimeFramesTestData), DynamicDataSourceType.Property)]
        public void GetOpenTimeFrames_Returns_Correct_Result(
            in IEnumerable<(TimeSpan, TimeSpan)> dailyEvents,
            in (TimeSpan, TimeSpan) dailySchedule,
            in TimeSpan allocatedTime,
            in IEnumerable<(TimeSpan, TimeSpan)> expected)
        {
            var actual = GetOpenTimeFrames(dailyEvents, dailySchedule, allocatedTime).ToList();

            Assert.AreEqual(string.Join(",", expected), string.Join(",", actual));
            CollectionAssert.AreEqual(expected.ToList(), actual);
        }

        public static IEnumerable<object[]> GetOpenTimeFramesTestData
        {
            get
            {
                yield return new object[]
                {
                    Array.Empty<(TimeSpan, TimeSpan)>(),
                    (TimeSpan.ParseExact("07:00", Format, Culture), TimeSpan.ParseExact("16:00", Format, Culture)),
                    TimeSpan.ParseExact("0", "%m", Culture),
                    new[]
                    {
                        (TimeSpan.ParseExact("07:00", Format, Culture), TimeSpan.ParseExact("16:00", Format, Culture))
                    }
                };

                yield return new object[]
                {
                    new[]
                    {
                        (TimeSpan.ParseExact("07:00", Format, Culture), TimeSpan.ParseExact("16:00", Format, Culture))
                    },
                    (TimeSpan.ParseExact("07:00", Format, Culture), TimeSpan.ParseExact("15:30", Format, Culture)),
                    TimeSpan.ParseExact("1", "%h", Culture),
                    Array.Empty<(TimeSpan, TimeSpan)>()
                };

                yield return new object[]
                {
                    new[]
                    {
                        (TimeSpan.ParseExact("08:00", Format, Culture), TimeSpan.ParseExact("10:00", Format, Culture)),
                        (TimeSpan.ParseExact("13:00", Format, Culture), TimeSpan.ParseExact("14:00", Format, Culture))
                    },
                    (TimeSpan.ParseExact("07:00", Format, Culture), TimeSpan.ParseExact("16:00", Format, Culture)),
                    TimeSpan.ParseExact("0", "%m", Culture),
                    new[]
                    {
                        (TimeSpan.ParseExact("07:00", Format, Culture), TimeSpan.ParseExact("08:00", Format, Culture)),
                        (TimeSpan.ParseExact("10:00", Format, Culture), TimeSpan.ParseExact("13:00", Format, Culture)),
                        (TimeSpan.ParseExact("14:00", Format, Culture), TimeSpan.ParseExact("16:00", Format, Culture))
                    }
                };

                yield return new object[]
                {
                    new[]
                    {
                        (TimeSpan.ParseExact("05:00", Format, Culture), TimeSpan.ParseExact("12:30", Format, Culture)),
                        (TimeSpan.ParseExact("13:00", Format, Culture), TimeSpan.ParseExact("14:00", Format, Culture)),
                        (TimeSpan.ParseExact("15:00", Format, Culture), TimeSpan.ParseExact("18:30", Format, Culture))
                    },
                    (TimeSpan.ParseExact("07:00", Format, Culture), TimeSpan.ParseExact("16:00", Format, Culture)),
                    TimeSpan.ParseExact("1", "%h", Culture),
                    new[]
                    {
                        (TimeSpan.ParseExact("14:00", Format, Culture), TimeSpan.ParseExact("15:00", Format, Culture))
                    }
                };
            }
        }
    }
}