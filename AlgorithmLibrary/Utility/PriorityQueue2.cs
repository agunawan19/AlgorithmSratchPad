using System.Collections.Generic;
using System.Linq;

namespace AlgorithmLibrary.Utility
{
    public class PriorityQueue2<T> : PriorityQueueBase<T> where T : IPrioritizable<T>
    {
        private LinkedList<T> PrioritizableItems { get; }

        public PriorityQueue2()
        {
            PrioritizableItems = new LinkedList<T>();
            Items = PrioritizableItems;
        }

        public override T Peek() => PrioritizableItems.First();

        public override int Count => PrioritizableItems.Count;

        public override void Clear() => PrioritizableItems.Clear();
        
        public override bool Contains(T item) => PrioritizableItems.Contains(item);

        public override void EnqueueRange(IEnumerable<T> items)
        {
            foreach (var item in items) Enqueue(item);
        }

        public override T Dequeue()
        {
            if (!PrioritizableItems.Any()) return default;
            var removedItem = PrioritizableItems.First.Value;
            PrioritizableItems.RemoveFirst();
            return removedItem;
        }

        public override void Enqueue(T item)
        {
            var newNode = new LinkedListNode<T>(item);

            switch (PrioritizableItems.First)
            {
                case { } pointerNode:
                    while (pointerNode.Next != null && pointerNode.Value.Priority < item.Priority)
                        pointerNode = pointerNode.Next;

                    if (pointerNode.Value.Priority <= item.Priority)
                        PrioritizableItems.AddAfter(pointerNode, newNode);
                    else
                        PrioritizableItems.AddBefore(pointerNode, newNode);
                    break;
                default:
                    PrioritizableItems.AddFirst(newNode);
                    break;
            }
        }
    }
}
