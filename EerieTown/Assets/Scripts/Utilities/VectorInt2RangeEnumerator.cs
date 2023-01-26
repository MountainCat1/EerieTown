using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class VectorInt2RangeEnumerator
    {
        public static IEnumerable<Vector2Int> GetRange(Vector2Int center, int range) {
            for (int x = center.x - range; x <= center.x + range; x++) {
                for (int y = center.y - range; y <= center.y + range; y++) {
                    var current = new Vector2Int(x, y);
                    if (Vector2Int.Distance(current, center) <= range) {
                        yield return current;
                    }
                }
            }
        }
    }
}
