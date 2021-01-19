using System;
using AlgorithmLibrary.Utility;

namespace AlgorithmScratchPad
{
    public class Employee : IPrioritizable<Employee>
    {
        public string LastName { get; set; }
        public double Priority { get; set; }

        public Employee(string lastName, double priority)
        {
            LastName = lastName;
            Priority = priority;
        }

        public override string ToString() => $"({LastName}, {Priority:f1})";

        public int CompareTo(Employee other) =>
            other is null ? throw new ArgumentNullException($"{nameof(other)} cannot be null") :
            Priority < other.Priority ? -1 :
            Priority > other.Priority ? 1 : 0;
    }
}
