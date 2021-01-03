using BenchmarkDotNet.Attributes;

namespace BenchmarkDotNetConsole.Benchmarks
{
    public class IsEvenBenchmark : BenchmarkBase
    {
        private const int Number = 1_000_001;

        private bool IsEven1(int n) => n % 2 == 0;
        private bool IsEven2(int n) => (n & 1) == 0;
        private bool IsEven3(int n) => (n | 1) > n;
        private bool IsEven4(int n) => (n ^ 1) == n + 1;

        [Benchmark]
        public bool IsEvenRemainder() => IsEven1(Number);

        [Benchmark]
        public bool IsEvenBitwiseAnd() => IsEven2(Number);

        [Benchmark]
        public bool IsEvenBitwiseOr() => IsEven3(Number);

        [Benchmark]
        public bool IsEvenXorOperator() => IsEven4(Number);
    }
}
