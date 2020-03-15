using System;

namespace AlgorithmLibrary
{
    public interface IRange<T> : IComparable, IEquatable<T>, IFormattable
        where T : IComparable, IComparable<T>, IEquatable<T>, IFormattable
    {
        T From { get; }

        T To { get; }
    }
}