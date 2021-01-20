using System.Collections.Generic;

namespace AlgorithmLibrary.Utility
{
    public interface IPriorityQueue<T>
    {
        void Enqueue(T item);
        void EnqueueRange(IEnumerable<T> items);
        T Dequeue();
        List<T> Dequeue(int numberOfDequeue);
        T Peek();
        int Count { get; }
        void Clear();
        bool Contains(T item);
        T[] ToArray();
        List<T> ToList();
        bool IsEmpty();
    }
}
