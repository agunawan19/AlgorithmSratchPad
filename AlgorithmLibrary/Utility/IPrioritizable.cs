using System;

namespace AlgorithmLibrary.Utility
{
    public interface IPrioritizable<in T> : IComparable<T>
    {
        /// <summary>
        /// Priority of the item.
        /// </summary>
        public double Priority { get; set; }
    }
}
