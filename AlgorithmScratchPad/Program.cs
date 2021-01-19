using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlgorithmLibrary;
using AlgorithmLibrary.Extensions;
using AlgorithmLibrary.Graph;
using AlgorithmLibrary.Utility;
using FSharp;

namespace AlgorithmScratchPad
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

            GraphUsageDemo();
            //PriorityQueueDemo();
        }

        private static void PriorityQueueDemo()
        {
            Console.WriteLine("Begin Priority Queue demo\n");
            Console.WriteLine("Creating priority queue of Employee items\n");
            IPriorityQueue<Employee> pq = new PriorityQueue2<Employee>();

            var employees = new List<Employee>
            {
                new("Eason", 5.0),
                new("Chung", 3.0),
                new("Flynn", 6.0),
                new("Dunne", 4.0),
                new("Aiden", 1.0),
                new("Baker", 2.0),
                new("Aiden Jr.", 1.0)
            };

            foreach (var employee in employees)
            {
                Console.WriteLine($"Adding {employee} to priority queue");
                pq.Enqueue(employee);
                Console.WriteLine($"Peek front item: {pq.Peek()}");
                Console.WriteLine(pq.ToString());
                Console.WriteLine();                
            }
            
            Console.WriteLine("\nPriory queue is: ");
            Console.WriteLine(pq.ToString());
            Console.WriteLine("\n");
            
            Console.WriteLine($"\nPriority Count: {pq.Count}");

            for (var i = 1; i <= 4; i++)
            {
                Console.WriteLine($"Removing employee #{i} from priority queue");
                Employee e = pq.Dequeue();
                Console.WriteLine($"Removed employee is {e}");
                Console.WriteLine("\nPriory queue is now: ");
                Console.WriteLine(pq.ToString());
                Console.WriteLine("\n");
            }

            var test = pq.ToList();
#if DEBUG
            Console.WriteLine("Testing the priority queue");
            TestPriorityQueue(50000);
#endif

            Console.WriteLine("\nEnd Priority Queue demo");
            Console.ReadLine();   
        }

#if DEBUG
        private static void TestPriorityQueue(int numOperations)
        {
            Random rand = new Random(0);
            PriorityQueue<Employee> pq = new PriorityQueue<Employee>();
            for (var op = 0; op < numOperations; ++op)
            {
                int opType = rand.Next(0, 2);

                if (opType == 0) // enqueue
                {
                    string lastName = $"{op}man";
                    double priority = (100.0 - 1.0) * rand.NextDouble() + 1.0;
                    pq.Enqueue(new Employee(lastName, priority));

                    if (pq.IsConsistent() == false) Console.WriteLine($"Test fails after enqueue operation # {op}");
                }
                else // dequeue
                {
                    if (pq.Count <= 0) continue;

                    Employee e = pq.Dequeue();
                    if (pq.IsConsistent() == false)
                        Console.WriteLine("Test fails after dequeue operation # " + op);
                }
            }
            Console.WriteLine("\nAll tests passed");
        }
#endif        

        // ReSharper disable once UnusedMember.Local
        private static void GraphUsageDemo()
        {
            var cities = new[]
            {
                "Seattle", "San Francisco", "Los Angeles", "Riverside", "Phoenix", "Chicago", "Boston", "New York",
                "Atlanta", "Miami", "Dallas", "Houston", "Detroit", "Philadelphia", "Washington"
            };

            var values = Enum.GetValues(typeof(City)).Cast<City>().ToList();
            var unweightedCityGraph = new UnweightedGraph<City>(values);

            var weightedCityGraph = new WeightedGraph<City>(values);
            weightedCityGraph.GetMinimumSpanningTree(0);
            
            unweightedCityGraph.AddEdges(new List<(City first, City second)>
            {
                (City.Seattle, City.Chicago),
                (City.Seattle, City.SanFrancisco),
                (City.SanFrancisco, City.Riverside),
                (City.SanFrancisco, City.LosAngeles),
                (City.LosAngeles, City.Riverside),
                (City.LosAngeles, City.Phoenix),
                (City.Riverside, City.Philadelphia),
                (City.Riverside, City.Chicago),
                (City.Phoenix, City.Dallas),
                (City.Phoenix, City.Houston),
                (City.Dallas, City.Chicago),
                (City.Dallas, City.Atlanta),
                (City.Dallas, City.Houston),
                (City.Houston, City.Atlanta),
                (City.Houston, City.Miami),
                (City.Atlanta, City.Chicago),
                (City.Atlanta, City.Washington),
                (City.Atlanta, City.Miami),
                (City.Miami, City.Washington),
                (City.Chicago, City.Detroit),
                (City.Detroit, City.Boston),
                (City.Detroit, City.Washington),
                (City.Detroit, City.NewYork),
                (City.Boston, City.NewYork),
                (City.NewYork, City.Philadelphia),
                (City.Philadelphia, City.Washington)
            });
            
            weightedCityGraph.AddEdges(new List<(City first, City second, float weight)>
            {
                (City.Seattle, City.Chicago, 1737),
                (City.Seattle, City.SanFrancisco, 678),
                (City.SanFrancisco, City.Riverside, 386),
                (City.SanFrancisco, City.LosAngeles, 348),
                (City.LosAngeles, City.Riverside, 50),
                (City.LosAngeles, City.Phoenix, 357),
                (City.Riverside, City.Philadelphia, 307),
                (City.Riverside, City.Chicago, 1704),
                (City.Phoenix, City.Dallas, 887),
                (City.Phoenix, City.Houston, 1015),
                (City.Dallas, City.Chicago, 805),
                (City.Dallas, City.Atlanta, 721),
                (City.Dallas, City.Houston, 225),
                (City.Houston, City.Atlanta, 702),
                (City.Houston, City.Miami, 968),
                (City.Atlanta, City.Chicago, 588),
                (City.Atlanta, City.Washington, 543),
                (City.Atlanta, City.Miami, 604),
                (City.Miami, City.Washington, 923),
                (City.Chicago, City.Detroit, 238),
                (City.Detroit, City.Boston, 613),
                (City.Detroit, City.Washington, 396),
                (City.Detroit, City.NewYork, 482),
                (City.Boston, City.NewYork, 190),
                (City.NewYork, City.Philadelphia, 81),
                (City.Philadelphia, City.Washington, 123)
            });


            Console.WriteLine(unweightedCityGraph.ToString());
            
            Console.WriteLine(weightedCityGraph.ToString());
        }

        private static void ModifiedArray(List<int> numbers) => numbers.RemoveAt(0);

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

        private enum City
        {
            Seattle,
            [Description("San Francisco")]
            SanFrancisco,
            [Description("Los Angeles")]
            LosAngeles,
            Riverside,
            Phoenix,
            Chicago,
            Boston,
            [Description("New York")]
            NewYork,
            Atlanta,
            Miami,
            Dallas,
            Houston,
            Detroit,
            Philadelphia,
            Washington
        }
    }
}
