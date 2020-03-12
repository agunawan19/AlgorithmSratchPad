using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmSratchPad
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //TimeSpan from = new TimeSpan(1, 0, 0);
            //TimeSpan to = new TimeSpan(23, 0, 0);
            //var result = from - to;
            //var fromDate = new DateTime() + from;
            //var toDate = new DateTime() + to;

            var numbers = new List<int> { 1, 2, 3 };
            ModifiedArray(numbers);

            Console.WriteLine(string.Join(",", numbers));
        }

        private static void ModifiedArray(in List<int> numbers)
        {
            numbers.RemoveAt(0);
        }
    }
}