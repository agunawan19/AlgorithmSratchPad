using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmLibrary.Graph
{
    public class WeightedGraph<TVertex> : Graph<TVertex, WeightedEdge>
    {
        public WeightedGraph(IEnumerable<TVertex> vertices) : base(vertices) { }

        public void AddEdge(WeightedEdge edge)
        {
            Edges[edge.U].Add(edge);
            Edges[edge.V].Add(edge.Reversed());
        }

        public void AddEdge(int u, int v, float weight) => AddEdge(new WeightedEdge(u, v, weight));

        public void AddEdge(TVertex firstVertex, TVertex secondVertex, float weight) =>
            AddEdge(IndexOf(firstVertex), IndexOf(secondVertex), weight);

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < VertexCount; i++)
            {
                sb.AppendFormat("{0} -> {1}", VertexAt(i), EdgesOf(i).Select(edge => $"({VertexAt(edge.V)}, {edge.Weight})"));
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }

        public static double TotalWeight(List<WeightedEdge> path) => path.Sum(edge => edge.Weight);

        /// <summary>
        /// Find the minimum-spanning tree of this graph using Jarnik's algorithm
        /// </summary>
        /// <param name="start">the vertex index to start the search at</param>
        /// <returns>List of WeightedEdge</returns>
        // public List<WeightedEdge> GetMinimumSpanningTree(int start)
        // {
        //     // var result = new LinkedList<WeightedEdge>();
        //     //
        //     // if (start < 0 || start > (VertexCount - 1)) return result;
        //     //
        //     // var queue = new Queue<WeightedEdge>();
        //     //
        //     // var visited = new bool[VertexCount];
        // }
    }
}
