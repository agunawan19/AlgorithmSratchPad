namespace AlgorithmLibrary.Graph
{
    public class Edge
    {
        public int U { get; }
        public int V { get; }
    
        public Edge(int u, int v)
        {
            U = u;
            V = v;
        }
    
        public Edge Reversed() => new(V, U);
        
        public override string ToString() => $"{U} -> {V}";
    }
}
