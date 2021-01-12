using System;

namespace AlgorithmLibrary.Extensions
{
    public static class StringExtensions
    {
        public static (string, string) SplitToKeyValuePair(this string text, string separator)
        {
            var texts = text?.Split(new[] {separator ?? string.Empty}, StringSplitOptions.None);
            
            return texts?.Length == 2 ? (texts[0], texts[1]) : throw new ArgumentException();
        }
    }
}
