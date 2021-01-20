namespace AlgorithmLibrary.Graph
{
    public sealed class DijkstraNode : IDijkstraNode
    {
        public int Vertex { get; }
        public double Distance { get; }

        public DijkstraNode(int vertex, double distance)
        {
            Vertex = vertex;
            Distance = distance;
        }
        
        public int CompareTo(DijkstraNode other) => Distance.CompareTo(other?.Distance);
    }
}
