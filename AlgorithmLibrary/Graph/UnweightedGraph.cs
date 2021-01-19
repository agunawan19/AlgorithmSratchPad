using System.Collections.Generic;

namespace AlgorithmLibrary.Graph
{
    public class UnweightedGraph<TVertex> : Graph<TVertex, Edge>
    {
        public UnweightedGraph(IEnumerable<TVertex> vertices) : base(vertices) { }

        public void AddEdge(Edge edge)
        {
            Edges[edge.U].Add(edge);
            Edges[edge.V].Add(edge.Reversed());
        }

        public void AddEdge(int u, int v) => AddEdge(new Edge(u, v));

        public void AddEdge(TVertex firstVertex, TVertex secondVertex) =>
            AddEdge(new Edge(IndexOf(firstVertex), IndexOf(secondVertex)));

        public void AddEdges(IEnumerable<(TVertex firstVertex, TVertex secondVertex)> edges)
        {
            foreach (var (firstVertex, secondVertex) in edges) 
                AddEdge(firstVertex, secondVertex);
        }
    }
}
