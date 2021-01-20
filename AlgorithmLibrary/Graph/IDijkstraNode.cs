using System;

namespace AlgorithmLibrary.Graph
{
    public interface IDijkstraNode : IComparable<DijkstraNode>
    {
        int Vertex { get; }
        double Distance { get; }
    }
}
