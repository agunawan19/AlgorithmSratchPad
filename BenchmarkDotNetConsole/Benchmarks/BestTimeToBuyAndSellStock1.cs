using BenchmarkDotNet.Attributes;
using AlgorithmLibrary;

namespace BenchmarkDotNetConsole.Benchmarks
{
    public class BestTimeToBuyAndSellStock1Benchmark : BenchmarkBase
    {
        private int[] Prices { get; } = { 7, 1, 5, 3, 6, 4 };

        [Benchmark]
        public int MaxProfitBruteForce() => BestTimeToBuyAndSellStock1.MaxProfitBruteForce(Prices);

        [Benchmark]
        public int MaxProfitOnePass() => BestTimeToBuyAndSellStock1.MaxProfitOnePass(Prices);
    }
}
