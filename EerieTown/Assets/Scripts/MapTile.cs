

using JetBrains.Annotations;
using UnityEngine;

public class MapTile
{
    [CanBeNull] public Building Building { get; set; }
    public Vector2Int Position { get; set; }
}