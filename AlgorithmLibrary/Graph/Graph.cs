using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmLibrary.Graph
{
    public abstract class Graph<TVertex, TEdge> where TEdge : Edge
    {
        private readonly List<TVertex> _vertices = new();
        protected List<List<TEdge>> Edges { get; } = new();
        protected Graph() { }

        protected Graph(IEnumerable<TVertex> vertices)
        {
            _vertices.AddRange(vertices);
            Edges.AddRange(_vertices.Select(_ => new List<TEdge>()));
        }

        protected int VertexCount => _vertices.Count;

        protected int EdgeCount => Edges.Sum(es => es.Count);

        protected int AddVertex(TVertex vertex)
        {
            _vertices.Add(vertex);
            Edges.Add(new List<TEdge>());

            return VertexCount - 1;
        }

        protected TVertex VertexAt(int index) => _vertices[index];

        protected int IndexOf(TVertex vertex) => _vertices.IndexOf(vertex);

        protected List<TVertex> NeighborsOf(int index) => Edges[index].Select(edge => VertexAt(edge.V)).ToList();

        protected List<TVertex> NeighborsOf(TVertex vertex) => NeighborsOf(IndexOf(vertex));

        protected List<TEdge> EdgesOf(int index) => Edges[index];

        protected List<TEdge> EdgesOf(TVertex vertex) => EdgesOf(IndexOf(vertex));

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < VertexCount; i++)
            {
                sb.AppendFormat("{0} -> [{1}]", VertexAt(i), string.Join(", ", NeighborsOf(i)));
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }
    }
}
