using BenchmarkDotNet.Running;
using BenchmarkDotNetConsole.Benchmarks;

namespace BenchmarkDotNetConsole
{
    static class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<StringContainsVsHashSetBenchmark>();
        }
    }
}
