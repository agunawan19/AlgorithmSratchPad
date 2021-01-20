using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmLibrary.Graph
{
    public abstract class Graph<TVertex, TEdge> : IGraph<TVertex, TEdge> where TEdge : Edge
    {
        private readonly List<TVertex> _vertices = new();
        
        public List<List<TEdge>> Edges { get; } = new();
        
        protected Graph() { }

        protected Graph(IEnumerable<TVertex> vertices)
        {
            _vertices.AddRange(vertices);
            Edges.AddRange(_vertices.Select(_ => new List<TEdge>()));
        }

        public int VertexCount => _vertices.Count;

        public int EdgeCount => Edges.Sum(es => es.Count);

        public int AddVertex(TVertex vertex)
        {
            _vertices.Add(vertex);
            Edges.Add(new List<TEdge>());

            return VertexCount - 1;
        }

        public TVertex VertexAt(int index) => _vertices[index];

        public int IndexOf(TVertex vertex) => _vertices.IndexOf(vertex);

        public List<TVertex> NeighborsOf(int index) => Edges[index].Select(edge => VertexAt(edge.V)).ToList();

        public List<TVertex> NeighborsOf(TVertex vertex) => NeighborsOf(IndexOf(vertex));

        public List<TEdge> EdgesOf(int index) => Edges[index];

        public List<TEdge> EdgesOf(TVertex vertex) => EdgesOf(IndexOf(vertex));

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
