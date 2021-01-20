using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmLibrary.Extensions
{
    public static class StringExtensions
    {
        public static (string, string) SplitToKeyValuePair(this string text, string separator)
        {
            var texts = text?.Split(new[] {separator}, StringSplitOptions.None);
            
            return texts?.Length == 2 ? (texts[0], texts[1]) : throw new ArgumentException();
        }

        public static Dictionary<string, string> ToDictionary(this string text, string keyValueSeparator = ":", string itemSeparator = ",")
        {
            var keyValuePairs = (text ?? string.Empty).Split(new[] { itemSeparator }, StringSplitOptions.RemoveEmptyEntries);
            var itemMap = new Dictionary<string, string>();

            if (!keyValuePairs.Any()) return itemMap;

            foreach (var keyValuePair in keyValuePairs)
            {
                var tokens = keyValuePair.Split(new[] { keyValueSeparator }, 2, StringSplitOptions.None);
                if (tokens.Length != 2 || string.IsNullOrWhiteSpace(tokens[0])) continue;

                itemMap[tokens[0]] = tokens[1];
            }

            return itemMap;
        }

    }
}
