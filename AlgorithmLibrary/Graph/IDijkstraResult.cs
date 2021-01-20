using System.Collections.Generic;

namespace AlgorithmLibrary.Graph
{
    public interface IDijkstraResult
    {
        public double[] Distances { get; }
        public Dictionary<int, WeightedEdge> PathMap { get; }
    }
}
