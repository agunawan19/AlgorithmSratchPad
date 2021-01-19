using System.Collections.Generic;
using System.Text;

namespace AlgorithmLibrary.Utility
{
    public abstract class PriorityQueueBase<T> : IPriorityQueue<T>
    {
        private protected ICollection<T> Items { get; set; }
        public abstract void Enqueue(T item);
        public abstract void EnqueueRange(IEnumerable<T> items);
        public abstract T Dequeue();
        public abstract T Peek();
        public abstract int Count { get; }
        public abstract void Clear();
        public abstract bool Contains(T item);

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var item in Items) sb.AppendFormat("{0} ", item);
            sb.Append($"count = {Items.Count}");
            return sb.ToString();
        }
    }
}
