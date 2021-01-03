using BenchmarkDotNet.Attributes;
using AlgorithmLibrary;

namespace BenchmarkDotNetConsole.Benchmarks
{
    public class BestTimeToBuyAndSellStock2Benchmark : BenchmarkBase
    {
        private int[] Prices { get; } = { 7, 1, 5, 3, 6, 4 };

        [Benchmark]
        public int MaxProfitBruteForce() => BestTimeToBuyAndSellStock2.MaxProfitBruteForce(Prices);

        [Benchmark]
        public int MaxProfitPeakValleyApproach() => BestTimeToBuyAndSellStock2.MaxProfitPeakValleyApproach(Prices);

        [Benchmark]
        public int MaxProfitOnePass() => BestTimeToBuyAndSellStock2.MaxProfitOnePass(Prices);
    }
}
