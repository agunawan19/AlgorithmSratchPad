using System.Collections.Generic;

namespace AlgorithmLibrary.Graph
{
    public sealed class DijkstraResult : IDijkstraResult
    {
        public double[] Distances { get; }
        public Dictionary<int, WeightedEdge> PathMap { get; }

        public DijkstraResult(double[] distances, Dictionary<int, WeightedEdge> pathMap)
        {
            Distances = distances;
            PathMap = pathMap;
        }
    }
}
