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

        public override string ToString() => $"({LastName}, {Priority:F1})";

        public int CompareTo(Employee other) => Priority.CompareTo(other.Priority);
    }
}
