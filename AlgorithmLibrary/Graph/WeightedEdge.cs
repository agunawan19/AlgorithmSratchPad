using System;

namespace AlgorithmLibrary.Graph
{
    public class WeightedEdge : Edge, IComparable<WeightedEdge>
    {
        public double Weight { get; }
        
        public WeightedEdge(int u, int v, double weight) : base(u, v) => Weight = weight;

        public new WeightedEdge Reversed() => new(V, U, Weight);

        public int CompareTo(WeightedEdge other) => Weight.CompareTo(other?.Weight);

        public override string ToString() => $"{U} {Weight}> {V}";
    }
}
