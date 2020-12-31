using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace BenchmarkDotNetConsole.Benchmarks
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public abstract class BenchmarkBase
    {
    }
}
