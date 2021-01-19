using System;
using System.Collections.Generic;

namespace AlgorithmLibrary.Utility
{
    public class PriorityQueue<T> : PriorityQueueBase<T> where T : IComparable<T>
    {
        private List<T> PrioritizableItems { get; }

        public PriorityQueue()
        {
            PrioritizableItems = new List<T>();
            Items = PrioritizableItems;
        }

        public override void Enqueue(T item)
        {
            PrioritizableItems.Add(item);

            var childIndex = PrioritizableItems.Count - 1;

            while (childIndex > 0)
            {
                var parentIndex = (childIndex - 1) / 2;
                var isChildItemGreaterOrEqualParentItem =
                    PrioritizableItems[childIndex].CompareTo(PrioritizableItems[parentIndex]) >= 0;
                if (isChildItemGreaterOrEqualParentItem) break;
                SwapParentAndChild(parentIndex, childIndex);
                childIndex = parentIndex;
            }
        }

        public override void EnqueueRange(IEnumerable<T> items)
        {
            foreach (var item in items) Enqueue(item);
        }

        public override T Dequeue()
        {
            var lastIndex = PrioritizableItems.Count - 1;
            T removedItem = PrioritizableItems[0];
            PrioritizableItems[0] = PrioritizableItems[lastIndex];
            PrioritizableItems.RemoveAt(lastIndex);
            lastIndex -= 1;
            var parentIndex = 0;

            while (true)
            {
                var childIndex = parentIndex * 2 + 1;
                
                if (childIndex > lastIndex) break;
                
                var rightChild = childIndex + 1;
                if (rightChild <= lastIndex && PrioritizableItems[rightChild].CompareTo(PrioritizableItems[childIndex]) < 0)
                    childIndex = rightChild;
                
                if (PrioritizableItems[parentIndex].CompareTo(PrioritizableItems[childIndex]) <= 0) break;
                
                SwapParentAndChild(parentIndex, childIndex);
                parentIndex = childIndex;
            }

            return removedItem;
        }

        private void SwapParentAndChild(int parentIndex, int childIndex) =>
            (PrioritizableItems[parentIndex], PrioritizableItems[childIndex]) =
            (PrioritizableItems[childIndex], PrioritizableItems[parentIndex]);

        public override T Peek() => PrioritizableItems[0];

        public override int Count => PrioritizableItems.Count;

        public override void Clear() => PrioritizableItems.Clear();
        
        public override bool Contains(T item) => PrioritizableItems.Contains(item);
        
        public override T[] ToArray() => PrioritizableItems.ToArray();
        
        public override List<T> ToList() => PrioritizableItems;
        
        public override bool IsEmpty() => (PrioritizableItems?.Count ?? 0) == 0;
        

#if DEBUG
        public bool IsConsistent()
        {
            if (PrioritizableItems.Count == 0) return true;
            var lastIndex = PrioritizableItems.Count - 1;

            for (var parentIndex = 0; parentIndex < PrioritizableItems.Count; parentIndex++)
            {
                var leftChildIndex = 2 * parentIndex + 1;
                var rightChildIndex = 2 * parentIndex + 2;
                if ((leftChildIndex <= lastIndex &&
                     PrioritizableItems[parentIndex].CompareTo(PrioritizableItems[leftChildIndex]) > 0) ||
                    (rightChildIndex <= lastIndex &&
                     PrioritizableItems[parentIndex].CompareTo(PrioritizableItems[rightChildIndex]) > 0))
                    return false;
            }

            return true;
        }
#endif
    }
}
