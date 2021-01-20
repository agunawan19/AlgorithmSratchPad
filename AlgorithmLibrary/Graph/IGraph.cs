using System.Collections.Generic;

namespace AlgorithmLibrary.Graph
{
    public interface IGraph<TVertex, TEdge> where TEdge : Edge
    {
        List<List<TEdge>> Edges { get; }
        int VertexCount { get; }
        int EdgeCount { get; }
        int AddVertex(TVertex vertex);
        TVertex VertexAt(int index);
        int IndexOf(TVertex vertex);
        List<TVertex> NeighborsOf(int index);
        List<TVertex> NeighborsOf(TVertex vertex);
        List<TEdge> EdgesOf(int index);
        List<TEdge> EdgesOf(TVertex vertex);
    }
}
