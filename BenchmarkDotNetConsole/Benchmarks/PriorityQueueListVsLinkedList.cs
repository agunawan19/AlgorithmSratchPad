using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using AlgorithmScratchPad;
using AlgorithmLibrary.Utility;

namespace BenchmarkDotNetConsole.Benchmarks
{
    public class PriorityQueueListVsLinkedList : BenchmarkBase
    {
        private List<Employee> _employees;
        private IPriorityQueue<Employee> _priorityQueueList;
        private IPriorityQueue<Employee> _priorityQueueLinkedList;
        private const int NumberOfDequeues = 4; 
        
        [GlobalSetup]
        public void GlobalSetup()
        {
            _employees = new List<Employee>
            {
                new Employee("Aiden", 1.0),
                new Employee("Baker", 2.0),
                new Employee("Chung", 3.0),
                new Employee("Dunne", 4.0),
                new Employee("Eason", 5.0),
                new Employee("Flynn", 6.0),
                new Employee("Aiden 2", 1.0)
            };

            _priorityQueueList = new PriorityQueue<Employee>();
            _priorityQueueList.EnqueueRange(_employees);            
            
            _priorityQueueLinkedList = new PriorityQueue2<Employee>();
            _priorityQueueLinkedList.EnqueueRange(_employees);
        }
        
        [Benchmark]
        public void PriorityQueueListEnqueueOnly() => new PriorityQueue<Employee>().EnqueueRange(_employees);
        
        [Benchmark]
        public void PriorityQueueLinkedListEnqueueOnly() => new PriorityQueue2<Employee>().EnqueueRange(_employees);

        [Benchmark]
        public void PriorityQueueListDequeueOnly()
        {
            for (var i = 0; i < NumberOfDequeues; i++) _priorityQueueList.Dequeue();
        }
        
        [Benchmark]
        public void PriorityQueueLinkedListDequeueOnly()
        {
            for (var i = 0; i < NumberOfDequeues; i++) _priorityQueueLinkedList.Dequeue();
        }
        
        [Benchmark]
        public void PriorityQueueList()
        {
            var priorityQueue = new PriorityQueue<Employee>();
            priorityQueue.EnqueueRange(_employees);
            priorityQueue.Dequeue();
            priorityQueue.Dequeue();
            priorityQueue.Dequeue();
            priorityQueue.Dequeue();
        }
        
        [Benchmark]
        public void PriorityQueueLinkedList()
        {
            var priorityQueue = new PriorityQueue2<Employee>();
            priorityQueue.EnqueueRange(_employees);
            priorityQueue.Dequeue();
            priorityQueue.Dequeue();
            priorityQueue.Dequeue();
            priorityQueue.Dequeue();
        }
    }
}
