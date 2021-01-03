using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static AlgorithmLibrary.Algorithm;
// ReSharper disable ExpressionIsAlwaysNull

namespace AlgorithmLibrary.Tests
{
    [TestClass]
    public class AlgorithmTests
    {
        private const string Format = @"hh\:mm";
        private static readonly CultureInfo Culture = CultureInfo.CurrentCulture;

        [DynamicData(nameof(IntersectWithTestData))]
        public void IntersectWith_Returns_Correct_Result(int[] reference, int[] other, bool expected)
        {
            var actual = reference.IntersectWith(other);
            Assert.AreEqual(expected, actual);
        }

        private static IEnumerable<object[]> IntersectWithTestData
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
        [DynamicData(nameof(IntersectWithTimeSpanTestData))]
        public void IntersectWith_TimeSpan_Returns_Correct_Result(TimeSpan[] reference, TimeSpan[] other, bool expected)
        {
            var (referenceFrom, referenceTo) = (reference[0], reference[1]);
            var (otherFrom, otherTo) = (other[0], other[1]);

            var actual = (referenceFrom, referenceTo).IntersectWith((otherFrom, otherTo), false);
            Assert.AreEqual(expected, actual);
        }

        private static IEnumerable<object[]> IntersectWithTimeSpanTestData
        {
            get
            {
                yield return new object[]
                {
                    new[]
                    {
                        TimeSpan.ParseExact("01:30", Format, Culture),
                        TimeSpan.ParseExact("03:30", Format, Culture)
                    },
                    new[]
                    {
                        TimeSpan.ParseExact("03:00", Format, Culture),
                        TimeSpan.ParseExact("04:30", Format, Culture)
                    },
                    true
                };

                yield return new object[]
                {
                    new[]
                    {
                        TimeSpan.ParseExact("01:30", Format, Culture),
                        TimeSpan.ParseExact("03:30", Format, Culture)
                    },
                    new[]
                    {
                        TimeSpan.ParseExact("03:30", Format, Culture),
                        TimeSpan.ParseExact("04:30", Format, Culture)
                    },
                    false
                };
            }
        }

        [DataTestMethod]
        [DynamicData(nameof(IntersectWithGenericIntTestData))]
        public void IntersectWith_Generic_Int_Returns_Correct_Result(string testCaseDescription,
            (int From, int To) reference, (int From, int To) other, bool isInclusive, bool expected)
        {
            var actual = reference.IntersectWith(other, isInclusive);
            Assert.AreEqual(expected, actual, testCaseDescription);
        }

        private static IEnumerable<object[]> IntersectWithGenericIntTestData
        {
            get
            {
                yield return new object[] { "#1. Should intersect", (1, 2), (1, 2), false, true };
                yield return new object[] { "#2. Should intersect", (1, 3), (2, 3), false, true };
                yield return new object[] { "#3. Should intersect", (1, 5), (2, 3), false, true };
                yield return new object[] { "#4. Should intersect", (2, 3), (1, 5), false, true };
                yield return new object[] { "#5. Should not intersect", (1, 2), (2, 3), false, false };
                yield return new object[] { "#6. Should not intersect", (1, 2), (4, 5), false, false };
            }
        }

        [DataTestMethod]
        [DynamicData(nameof(IntersectWithGenericDateTimeTestData))]
        public void IntersectWith_Generic_DateTime_Returns_Correct_Result(string testCaseDescription,
            (string From, string To) reference, (string From, string To) other, bool isInclusive, bool expected)
        {
            var referenceDateTime = (Convert.ToDateTime(reference.From), Convert.ToDateTime(reference.To));
            var otherDateTime = (Convert.ToDateTime(other.From), Convert.ToDateTime(other.To));
            var actual = referenceDateTime.IntersectWith(otherDateTime, isInclusive);
            Assert.AreEqual(expected, actual, testCaseDescription);
        }

        private static IEnumerable<object[]> IntersectWithGenericDateTimeTestData
        {
            get
            {
                yield return new object[]
                {
                    "#1. Should intersect", ("1/1/2000", "1/31/2000"), ("1/31/2000", "2/28/2000"), true, true
                };
                yield return new object[]
                {
                    "#2. Should not intersect", ("1/1/2000", "1/31/2000"), ("1/31/2000", "2/28/2000"), false, false
                };
                yield return new object[]
                {
                    "#3. Should intersect", ("1/31/2000", "2/28/2000"), ("1/1/2000", "1/31/2000"), true, true
                };
                yield return new object[]
                {
                    "#4. Should not intersect", ("1/31/2000", "2/28/2000"), ("1/1/2000", "1/31/2000"), false, false
                };
            }
        }

        //[DataTestMethod]
        //[DynamicData(nameof(GetIntersectionTimeSpanValuePairTestData), DynamicDataSourceType.Property)]
        //public void GetIntersectionTimeSpanValuePair_Returns_Correct_Result(TimeSpan[] reference, TimeSpan[] other, TimeSpan[] expected)
        //{
        //    var actual = GetIntersectionValuePair((reference[0], reference[1]), (other[0], other[1]));

        //    var jsonString = JsonSerializer.Serialize(actual);

        //    Assert.AreEqual(string.Join(",", expected), string.Join(",", actual), $"{string.Join(",", reference)} <-> {string.Join(",", other)}");
        //}

        //public static IEnumerable<object[]> GetIntersectionTimeSpanValuePairTestData
        //{
        //    get
        //    {
        //        yield return new object[]
        //        {
        //            new[]
        //            {
        //                TimeSpan.ParseExact("01:30", Format, Culture), TimeSpan.ParseExact("03:30", Format, Culture)
        //            },
        //            new []
        //            {
        //                TimeSpan.ParseExact("03:00", Format, Culture), TimeSpan.ParseExact("04:30", Format, Culture)
        //            },
        //            new []
        //            {
        //                TimeSpan.ParseExact("03:00", Format, Culture), TimeSpan.ParseExact("03:30", Format, Culture)
        //            }
        //        };

        //        yield return new object[]
        //        {
        //            new[]
        //            {
        //                TimeSpan.ParseExact("01:30", Format, Culture), TimeSpan.ParseExact("03:30", Format, Culture)
        //            },
        //            new []
        //            {
        //                TimeSpan.ParseExact("03:30", Format, Culture), TimeSpan.ParseExact("04:30", Format, Culture)
        //            },
        //            Array.Empty<TimeSpan>()
        //        };
        //    }
        //}

        [DataTestMethod]
        [DynamicData(nameof(IntersectionValuePairTestData))]
        public void GetIntersectionValuePair_Returns_Correct_Result(int[] reference, int[] other, int[] expected)
        {
            var actual = GetIntersectionValuePair(reference, other);

            Assert.AreEqual(string.Join(",", expected), string.Join(",", actual),
                $"{string.Join(",", reference)} <-> {string.Join(",", other)}");
        }

        private static IEnumerable<object[]> IntersectionValuePairTestData
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
        [DynamicData(nameof(IntersectionValuePairOfIntegerTestData))]
        public void GetIntersectionValuePair_Of_Integer_Returns_Correct_Result((int From, int To) reference,
            (int From, int To) other, (int From, int To)? expected)
        {
            var actual = GetIntersectionValuePair(reference, other);

            Assert.AreEqual(expected, actual);
        }

        public static IEnumerable<object[]> IntersectionValuePairOfIntegerTestData
        {
            get
            {
                yield return new object[] { (1, 2), (1, 2), (1, 2) };
                yield return new object[] { (1, 3), (2, 3), (2, 3) };
                yield return new object[] { (1, 5), (2, 3), (2, 3) };
                yield return new object[] { (2, 5), (1, 5), (2, 5) };
                yield return new object[] { (1, 2), (2, 3), null };
                yield return new object[] { (1, 2), (4, 5), null };
            }
        }

        [DataTestMethod]
        [DynamicData(nameof(IntersectionValuePairOfTimeSpanTestData))]
        public void GetIntersectionValuePair_Of_TimeSpan_Returns_Correct_Result((TimeSpan From, TimeSpan To) reference,
            (TimeSpan From, TimeSpan To) other, (TimeSpan From, TimeSpan To)? expected)
        {
            var actual = GetIntersectionValuePair(reference, other);

            Assert.AreEqual(expected, actual);
        }

        private static IEnumerable<object[]> IntersectionValuePairOfTimeSpanTestData
        {
            get
            {
                yield return new object[]
                {
                    GetTimeSpanTuple(("01:00", "02:00")), GetTimeSpanTuple(("01:00", "02:00")),
                    GetTimeSpanTuple(("01:00", "02:00"))
                };

                yield return new object[]
                {
                    GetTimeSpanTuple(("01:00", "03:00")), GetTimeSpanTuple(("02:30", "03:00")),
                    GetTimeSpanTuple(("02:30", "03:00"))
                };

                yield return new object[]
                {
                    GetTimeSpanTuple(("01:00", "05:00")), GetTimeSpanTuple(("02:00", "03:00")),
                    GetTimeSpanTuple(("02:00", "03:00"))
                };

                yield return new object[]
                {
                    GetTimeSpanTuple(("02:00", "05:00")), GetTimeSpanTuple(("01:00", "05:00")),
                    GetTimeSpanTuple(("02:00", "05:00"))
                };

                yield return new object[]
                {
                    GetTimeSpanTuple(("01:00", "02:30")), GetTimeSpanTuple(("02:30", "05:00")), null
                };

                yield return new object[]
                {
                    GetTimeSpanTuple(("01:00", "02:00")), GetTimeSpanTuple(("04:00", "05:00")), null
                };
            }
        }

        private static (TimeSpan From, TimeSpan To) GetTimeSpanTuple((string From, string To) time) =>
            (TimeSpan.ParseExact(time.From, Format, Culture), TimeSpan.ParseExact(time.To, Format, Culture));

        [DataTestMethod]
        [DynamicData(nameof(IntersectionsTestData))]
        public void GetIntersections_Returns_Correct_Result(int[][][] arr, int[][] expected)
        {
            var actual = GetIntersections(arr);

            var processedActual = actual.Aggregate(string.Empty, (acc, x) => $"{acc}[{string.Join(",", x)}],",
                allStr => allStr.TrimEnd(','));
            var processedExpected = expected.Aggregate(string.Empty, (acc, x) => $"{acc}[{string.Join(",", x)}],",
                allStr => allStr.TrimEnd(','));

            Assert.AreEqual(processedExpected, processedActual);
        }

        private static IEnumerable<object[]> IntersectionsTestData
        {
            get
            {
                yield return new object[]
                {
                    new[]
                    {
                        new[] { new[] { 6, 10 }, new[] { 11, 13 } }, new[] { new[] { 7, 14 } },
                        new[] { new[] { 7, 8 }, new[] { 10, 12 } },
                    },
                    new[] { new[] { 7, 8 }, new[] { 11, 12 } }
                };

                yield return new object[]
                {
                    new[]
                    {
                        new[] { new[] { 7, 14 } }, new[] { new[] { 6, 10 }, new[] { 11, 13 } },
                        new[] { new[] { 7, 8 }, new[] { 10, 12 } },
                    },
                    new[] { new[] { 7, 8 }, new[] { 11, 12 } }
                };

                yield return new object[]
                {
                    new[]
                    {
                        new[] { new[] { 7, 14 } }, new[] { new[] { 6, 10 }, new[] { 11, 13 } },
                        new[] { new[] { 7, 8 }, new[] { 10, 12 } }, new[] { new[] { 11, 12 } }
                    },
                    new[] { new[] { 11, 12 } }
                };

                yield return new object[]
                {
                    new[]
                    {
                        new[] { new[] { 7, 14 } }, new[] { new[] { 6, 10 }, new[] { 11, 13 } },
                        new[] { new[] { 7, 8 }, new[] { 10, 12 } }, new[] { new[] { 15, 17 } }
                    },
                    new int[][] { }
                };

                yield return new object[]
                {
                    new[]
                    {
                        new[] { new[] { 7, 14 } }, new[] { new[] { 6, 10 }, new[] { 11, 13 } },
                        new[] { new[] { 15, 17 } }, new[] { new[] { 7, 8 }, new[] { 10, 12 } },
                    },
                    new int[][] { }
                };

                yield return new object[]
                {
                    new[]
                    {
                        new[] { new[] { 7, 14 } }, new[] { new[] { 6, 10 }, new[] { 11, 13 } }, new int[][] { },
                    },
                    new int[][] { }
                };

                yield return new object[]
                {
                    new[]
                    {
                        new int[][] { }, new[] { new[] { 7, 14 } }, new[] { new[] { 6, 10 }, new[] { 11, 13 } },
                    },
                    new int[][] { }
                };

                yield return new object[]
                {
                    new[] { new[] { new[] { 7, 14 } }, new[] { new[] { 6, 10 }, new[] { 11, 13 } }, },
                    new[] { new[] { 7, 10 }, new[] { 11, 13 } }
                };

                yield return new object[] { new[] { new[] { new[] { 7, 14 } }, }, new[] { new[] { 7, 14 }, } };
            }
        }

        [DataTestMethod]
        [DynamicData(nameof(OpenTimeFramesTestData))]
        public void GetOpenTimeFrames_Returns_Correct_Result(IEnumerable<(TimeSpan, TimeSpan)> dailyEvents,
            (TimeSpan, TimeSpan) dailySchedule, TimeSpan allocatedTime, IEnumerable<(TimeSpan, TimeSpan)> expected)
        {
            var actual = GetOpenTimeFrames(dailyEvents, dailySchedule, allocatedTime).ToList();

            var expectedList = expected.ToList();
            Assert.AreEqual(string.Join(",", expectedList), string.Join(",", actual));
            CollectionAssert.AreEqual(expectedList.ToList(), actual);
        }

        private static IEnumerable<object[]> OpenTimeFramesTestData
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
                        (TimeSpan.ParseExact("07:00", Format, Culture),
                            TimeSpan.ParseExact("16:00", Format, Culture))
                    }
                };

                yield return new object[]
                {
                    new[]
                    {
                        (TimeSpan.ParseExact("07:00", Format, Culture),
                            TimeSpan.ParseExact("16:00", Format, Culture))
                    },
                    (TimeSpan.ParseExact("07:00", Format, Culture), TimeSpan.ParseExact("15:30", Format, Culture)),
                    TimeSpan.ParseExact("1", "%h", Culture), Array.Empty<(TimeSpan, TimeSpan)>()
                };

                yield return new object[]
                {
                    new[]
                    {
                        (TimeSpan.ParseExact("08:00", Format, Culture),
                            TimeSpan.ParseExact("10:00", Format, Culture)),
                        (TimeSpan.ParseExact("13:00", Format, Culture),
                            TimeSpan.ParseExact("14:00", Format, Culture))
                    },
                    (TimeSpan.ParseExact("07:00", Format, Culture), TimeSpan.ParseExact("16:00", Format, Culture)),
                    TimeSpan.ParseExact("0", "%m", Culture),
                    new[]
                    {
                        (TimeSpan.ParseExact("07:00", Format, Culture),
                            TimeSpan.ParseExact("08:00", Format, Culture)),
                        (TimeSpan.ParseExact("10:00", Format, Culture),
                            TimeSpan.ParseExact("13:00", Format, Culture)),
                        (TimeSpan.ParseExact("14:00", Format, Culture),
                            TimeSpan.ParseExact("16:00", Format, Culture))
                    }
                };

                yield return new object[]
                {
                    new[]
                    {
                        (TimeSpan.ParseExact("05:00", Format, Culture),
                            TimeSpan.ParseExact("12:30", Format, Culture)),
                        (TimeSpan.ParseExact("13:00", Format, Culture),
                            TimeSpan.ParseExact("14:00", Format, Culture)),
                        (TimeSpan.ParseExact("15:00", Format, Culture),
                            TimeSpan.ParseExact("18:30", Format, Culture))
                    },
                    (TimeSpan.ParseExact("07:00", Format, Culture), TimeSpan.ParseExact("16:00", Format, Culture)),
                    TimeSpan.ParseExact("1", "%h", Culture),
                    new[]
                    {
                        (TimeSpan.ParseExact("14:00", Format, Culture),
                            TimeSpan.ParseExact("15:00", Format, Culture))
                    }
                };
            }
        }

        [TestMethod]
        [DataRow("#1. Should return true", 2, 1, 3, true, true)]
        [DataRow("#2. Should return true", 2, 1, 3, false, true)]
        [DataRow("#3. Should return true", 2, 1, 2, true, true)]
        [DataRow("#4. Should return false", 2, 1, 2, false, false)]
        public void IsBetweenTest(string testCaseDescription, int valueToCheck, int start, int end, bool isInclusive,
            bool expected)
        {
            var actual = valueToCheck.IsBetween(start, end, isInclusive);
            Assert.AreEqual(expected, actual);
        }

        [TestCategory("Merge")]
        [TestMethod]
        [DynamicData(nameof(MergeTestData))]
        public void Merge_Returns_ExpectedResult(int[] nums1, int[] nums2, bool isDistinct, int[] expected)
        {
            var actual = nums1.Merge(nums2, isDistinct).ToArray();
            CollectionAssert.AreEqual(expected, actual);
        }

        private static IEnumerable<object[]> MergeTestData
        {
            get
            {
                yield return new object[] { new[] { 1, 2, 3 }, new[] { 4, 5, 6 }, false, new[] { 1, 2, 3, 4, 5, 6 } };
                yield return new object[] { new[] { 1, 2, 3, 4 }, new[] { 4, 5, 6, 3 }, true, new[] { 1, 2, 3, 4, 5, 6 } };
            }
        }

        [TestCategory("Merge")]
        [TestMethod]
        public void Merge_Throws_Source_ArgumentNullException()
        {
            int[] source = null;
            var other = new[] { 3, 4, 5 };

            Assert.ThrowsException<ArgumentNullException>(() => source.Merge<int>(other));
        }

        [TestCategory("Merge")]
        [TestMethod]
        public void Merge_Throws_Other_ArgumentNullException()
        {
            var source = new[] { 1, 2, 3 };
            int[] other = null;

            Assert.ThrowsException<ArgumentNullException>(() => source.Merge<int>(other));
        }
    }
}
