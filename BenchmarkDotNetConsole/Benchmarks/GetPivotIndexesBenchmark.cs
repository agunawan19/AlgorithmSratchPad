using BenchmarkDotNet.Attributes;

using AlgorithmLibrary;

namespace BenchmarkDotNetConsole.Benchmarks
{
    
    public class GetPivotIndexesBenchmark : BenchmarkBase
    {
        private readonly int[] _numbers = {2, 1, 3, 1, 9, 2, 2, 1, 2, 2, 1, 3, 1, 9, 2, 2, 1, 2, 2, 1, 3, 1, 9, 2, 2, 1, 2, 2, 1, 3, 1, 9, 2, 2, 1, 2};
        
        [Benchmark]
        public void PivotIndexSum()
        {
            PivotIndex.GetPivotIndexes(_numbers);
        }

        [Benchmark]
        public void PivotIndexForLoop()
        {
            PivotIndex.GetPivotIndexesForLoop(_numbers);
        }

        [Benchmark]
        public void PivotIndexForEachLoop()
        {
            PivotIndex.GetPivotIndexesForEachLoop(_numbers);
        }

        [Benchmark]
        public void PivotIndexAggregate()
        {
            PivotIndex.GetPivotIndexesAggregate(_numbers);
        }
        
    }
}
