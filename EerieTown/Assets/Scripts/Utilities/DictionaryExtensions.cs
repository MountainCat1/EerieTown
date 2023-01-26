using System.Collections;
using System.Collections.Generic;

namespace Utilities
{
    public static class DictionaryExtensions
    {
        public static void Extend<TKey, TValue>(this IDictionary<TKey, List<TValue>> dictionary, TKey key, TValue value)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, new List<TValue>()
                {
                    value
                });
                return;
            }
            
            dictionary[key].Add(value);
        }
    }
}