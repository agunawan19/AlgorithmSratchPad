namespace AlgorithmLibrary.Graph
{
    public interface IEdge
    {
        int U { get; }
        int V { get; }
        IEdge Reversed();
    }
}
