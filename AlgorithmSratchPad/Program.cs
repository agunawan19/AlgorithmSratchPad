using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlgorithmLibrary;
using AlgorithmLibrary.Extensions;

namespace AlgorithmSratchPad
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            //TimeSpan from = new TimeSpan(1, 0, 0);
            //TimeSpan to = new TimeSpan(23, 0, 0);
            //var result = from - to;
            //var fromDate = new DateTime() + from;
            //var toDate = new DateTime() + to;

            //var numbers = new List<int> { 1, 2, 3 };
            //ModifiedArray(numbers);

            //Console.WriteLine(string.Join(",", numbers));
            //TimeFrameTest();
            //LinqTest();
            var test = "a:b".SplitToKeyValuePair(null);
        }

        private static void ModifiedArray(List<int> numbers)
        {
            numbers.RemoveAt(0);
        }

        private static void TimeFrameTest()
        {
            var decRange1 = new Range<decimal>(1.50M, 3.50M);
            var decRange2 = new Range<decimal>(3.01M, 4.99M);
            var decRange3 = new Range<decimal>(3.01M, 3.99M);
            var decIntersection = decRange2.IntersectRange(decRange1);
            //var boolDec1 = decRange2 == decRange3;
            var boolDec2 = decRange1 <= decRange2;

            var testDateTime1 = new Range<DateTime>(
                new DateTime(2000, 1, 1, 1, 0, 0),
                new DateTime(2000, 1, 1, 13, 0, 0)
            );
            var testDateTime2 = new Range<DateTime>(
                new DateTime(2000, 1, 1, 1, 0, 0),
                new DateTime(2000, 1, 1, 13, 0, 0)
            );

            var boolTestDateTimeResult1 = testDateTime1 == testDateTime2;

            var testConstructor1 = new Range<DateTime>("08:00", "10:00");
            var testConstructor2 = new Range<TimeSpan>("08:00", "10:00");

            var timeFrame1 = new Range<TimeSpan>("01:00", "05:00");
            var timeFrame2 = new Range<TimeSpan>("05:00", "07:00");
            var timeFrame3 = new Range<TimeSpan>("06:00", "09:00");
            var timeFrame4 = new Range<TimeSpan>("03:00", "05:00");
            var timeFrame5 = new Range<TimeSpan>("01:00", "02:00");
            var timeFrame6 = new Range<TimeSpan>("01:00", "03:30");

            //var timeFrames = new List<IRange<int>>
            //{
            //    timeFrame1, timeFrame2, timeFrame3, timeFrame4, timeFrame5
            //};

            var enumerableTest1 = Enumerable.Range(1, 20).Intersect(Enumerable.Range(20, 25));

            var intResult1 = timeFrame3.IsIntersectRange(timeFrame4);
            var intersectResult = timeFrame3.IntersectRange(timeFrame4);
            var disjointResult1 = timeFrame1.DisjointRange(timeFrame2);
            var disjointResult2 = timeFrame3.DisjointRange(timeFrame1);
            var disjointResult3 = timeFrame1.DisjointRange(timeFrame3);

            var boolResult1 = timeFrame2 <= timeFrame4;
            var boolResult2 = timeFrame2 > timeFrame4;
            var boolResult3 = timeFrame2 == timeFrame4;

            //timeFrames.Sort();

            ////Console.WriteLine(
            ////    timeFrames.Aggregate(
            ////        string.Empty,
            ////        (acc, x) => $"{acc}[{x.From},{x.To}],",
            ////        x => x.TrimEnd(','))
            ////);

            var dt1 = new DateTime(2000, 1, 1);
            var dt2 = new DateTime(2000, 1, 1);

            var listA = new List<int> { 1, 3 };
            var listB = new List<int> { 2, 4 };

            var resultI = listA.Intersect(listB);

            Console.WriteLine(timeFrame6.ToString("TS", null));
        }

        private static void LinqTest()
        {
            var testClasses = new List<TestClass>
            {
                new TestClass(3)
                {
                    CGROUP1 = "CGROUP1",
                    CGROUP2 = "CGROUP2"
                },
                new TestClass(2)
                {
                    CGROUP1 = "ABC"
                },
                new TestClass(1),
            };

            var pcIPGroup1 = string.Empty;

            var query = testClasses.FirstOrDefault(t =>
                (pcIPGroup1 == string.Empty || string.IsNullOrWhiteSpace(t.CGROUP1)) &&
                string.IsNullOrWhiteSpace(t.CGROUP2));

            var test = query.ToString();
        }

        private class TestClass
        {
            public int ID { get; }
            public string CGROUP1 { get; set; } = string.Empty;
            public string CGROUP2 { get; set; } = string.Empty;

            public TestClass(int id)
            {
                ID = id;
            }

            public bool BooleanTest()
            {
                //if (true) return true else if (true) return true else return false;
                var conditionA = false;
                var conditionB = false;
                var conditionC = false;
                var boolResult = conditionA ? true : conditionB ? true : conditionC;
                return boolResult;
            }
        }
    }
}
