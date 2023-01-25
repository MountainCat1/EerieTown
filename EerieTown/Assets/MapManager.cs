using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    

    private Dictionary<Vector2Int, MapTile> Tiles { get; } = new();

    public bool PlaceBuilding(Building building)
    {
        var takenPositions = new List<Vector2Int>();
        for (int x = building.Position.x; x < building.Position.x + building.Size.x; x++)
        {
            for (int y = building.Position.y; y < building.Position.y + building.Size.y; y++)
            {
                var position = new Vector2Int(x, y);
                takenPositions.Add(position);

                if (SetBuilding(position, building)) continue;
                
                // One of tiles is already taken, roll back changes
                foreach (var takenPosition in takenPositions)
                {
                    SetBuilding(takenPosition, null);
                }

                return false;
            }
        }

        return true;
    }

    private bool SetBuilding(Vector2Int position, Building building)
    {
        var mapTile = GetRequiredMapTile(position);

        if (mapTile.Building == null)
        {
            mapTile.Building = building;
            return true;
        }

        return false;
        //
        // if (Tiles.TryGetValue(position, out var foundTile))
        // {
        //     if (foundTile.Building != null)
        //     {
        //         // Tile is already taken!
        //         return false;
        //     }
        //
        //     // Tile is empty, so make a new one and set building
        //     foundTile.Building = building;
        //
        //     return true;
        // }
        //
        // // Tile was not yet initialized, so its free to initialize and take
        // Tiles.Add(position, new MapTile()
        // {
        //     Building = building
        // });
        //
        // return true;
    }

    public MapTile GetClosestTile(Vector3 point)
    {
        var roundedPosition = Vector2Int.RoundToInt(new Vector2(point.x, point.z));

        var mapTile = GetRequiredMapTile(roundedPosition);

        return mapTile;
    }

    private MapTile GetRequiredMapTile(Vector2Int position)
    {
        if (Tiles.TryGetValue(position, out var foundTile))
            return foundTile;

        
        var newTile = new MapTile();

        Tiles.Add(position, newTile);
        
        return newTile;
    }
}