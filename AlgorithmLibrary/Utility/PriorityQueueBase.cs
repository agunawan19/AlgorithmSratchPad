using System.Collections.Generic;
using System.Text;

namespace AlgorithmLibrary.Utility
{
    public abstract class PriorityQueueBase<T> : IPriorityQueue<T>
    {
        private protected ICollection<T> Items { get; set; }
        public abstract void Enqueue(T item);

        public virtual void EnqueueRange(IEnumerable<T> items)
        {
            foreach (var item in items) Enqueue(item);
        }

        public abstract T Dequeue();

        public virtual List<T> Dequeue(int numberOfDequeue)
        {
            var result = new List<T>();

            for (var i = 0; i < numberOfDequeue; i++)
            {
                var removedItem = Dequeue();
                if (removedItem is not null) result.Add(Dequeue());
            }

            return result;
        }

        public abstract T Peek();
        public abstract int Count { get; }
        public abstract void Clear();
        public abstract bool Contains(T item);
        public abstract T[] ToArray();

        public abstract List<T> ToList();

        public abstract bool IsEmpty();

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var item in Items) sb.AppendFormat("{0} ", item);
            sb.Append($"count = {Items.Count}");
            return sb.ToString();
        }
    }
}
