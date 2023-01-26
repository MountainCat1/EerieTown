using System.Collections.Generic;
using System.Linq;

namespace Utilities
{
    public static class EnumerableExtensions {
        public static T GetRandomValue<T>(this IEnumerable<T> enumerable) {
            var list = new List<T>(enumerable);
            var index = UnityEngine.Random.Range(0, enumerable.Count());
            return list[index];
        }
    }
}