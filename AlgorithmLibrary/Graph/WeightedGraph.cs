using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlgorithmLibrary.Utility;

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
        
        public void AddEdges(IEnumerable<(TVertex firstVertex, TVertex secondVertex, float weight)> edges)
        {
            foreach (var (firstVertex, secondVertex, weight) in edges) 
                AddEdge(firstVertex, secondVertex, weight);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < VertexCount; i++)
            {
                sb.AppendFormat("{0} -> [{1}]", VertexAt(i),
                    string.Join(", ", EdgesOf(i).Select(edge => $"({VertexAt(edge.V)}, {edge.Weight:F1})")));
                sb.Append(Environment.NewLine);
            }
            
            return sb.ToString();
        }

        public static double TotalWeight(IEnumerable<WeightedEdge> path) => path.Sum(edge => edge.Weight);

        /// <summary>
        /// Find the minimum-spanning tree of this graph using Jarnik's algorithm
        /// </summary>
        /// <param name="start">the vertex index to start the search at</param>
        /// <returns>List of WeightedEdge</returns>
        public List<WeightedEdge> GetMinimumSpanningTree(int start)
        {
            var result = new List<WeightedEdge>();
           
            if (start < 0 || start > (VertexCount - 1)) return result;

            var pq = new PriorityQueue<WeightedEdge>();
            
            var visited = new bool[VertexCount];

            Visit(start);
            while (!pq.IsEmpty())
            {
                WeightedEdge edge = pq.Dequeue();
                
                if (visited[edge.V]) continue;
                
                result.Add(edge);
                Visit(edge.V);
            }
            
            return result;
            
            void Visit(int index)
            {
                visited[index] = true;
                foreach (var edge in EdgesOf(index).Where(edge => !visited[edge.V])) pq.Enqueue(edge);
            }
        }

        public void PrintWeightedPath(List<WeightedEdge> weightedEdges)
        {
            foreach (var edge in weightedEdges)
                Console.WriteLine($"{VertexAt(edge.U)} {edge.Weight:F1}> {VertexAt(edge.V)}");

            Console.WriteLine($"Total Weight: {TotalWeight(weightedEdges):F1}");
        }

        public DijkstraResult GetDijkstraResult(TVertex root)
        {
            var first = IndexOf(root);
            var distances = new double[VertexCount];
            distances[first] = 0;
            var visited = new bool[VertexCount];
            visited[first] = true;
            var pathMap = new Dictionary<int, WeightedEdge>();
            var pq = new PriorityQueue<DijkstraNode>();
            pq.Enqueue(new DijkstraNode(first, 0));

            while (!pq.IsEmpty())
            {
                var u = pq.Dequeue().Vertex;
                var distU = distances[u];

                foreach (var edge in EdgesOf(u))
                {
                    var distV = distances[edge.V];
                    var pathWeight = edge.Weight + distU;
                    if (visited[edge.V] && (distV < pathWeight)) continue;

                    visited[edge.V] = true;
                    distances[edge.V] = pathWeight;
                    pathMap[edge.V] = edge;
                    pq.Enqueue(new DijkstraNode(edge.V, pathWeight));
                }
            }
                
            return new DijkstraResult(distances, pathMap);
        }

        public Dictionary<TVertex, double> DistanceArrayToDistanceMap(double[] distances)
        {
            var distanceMap = new Dictionary<TVertex, double>();
           
            for (var i = 0; i < distances.Length; i++) 
                distanceMap[VertexAt(i)] = distances[i];

            return distanceMap;
        }

        public static List<WeightedEdge> PathMapToPath(int start, int end, Dictionary<int, WeightedEdge> pathMap)
        {
            if (!pathMap.Any()) return new List<WeightedEdge>();
            var path = new List<WeightedEdge>();
            var edge = pathMap[end];
            path.Add(edge);

            while (edge.U != start)
            {
                edge = pathMap[edge.U];
                path.Add(edge);
            }

            path.Reverse();
            
            return path;
        }
    }
}
