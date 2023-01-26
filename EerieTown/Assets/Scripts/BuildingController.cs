using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities;

public class BuildingController : MonoBehaviour
{
    #region Dependencies

    [SerializeField] private InputManager _inputManager;
    [SerializeField] private MapSelector _mapSelector;
    [SerializeField] private MapManager _mapManager;

    #endregion

    
    [SerializeField] private Building _buildingPrefab;

    private Dictionary<Vector2Int, Building> Buildings { get; } = new();

    private void Awake()
    {
        _inputManager.MainClickedEvent += InputManagerOnMainClickedEvent;

        _mapSelector.TileSelectedEvent += MapSelectorOnTileSelectedEvent;
    }

    public Building GetRandomBuildingInRange(Vector2Int center, int range)
        => GetRandomBuildingInRange(center, range, Array.Empty<Building>());
    
    public Building GetRandomBuildingInRange(Vector2Int center, int range, IEnumerable<Building> except)
    {
        var position = VectorInt2RangeEnumerator
            .GetRange(center, range)
            .Where(position => Buildings.ContainsKey(position))
            .GetRandomValue();
        return Buildings[position];
    }
    
    private bool PlaceBuilding(Building buildingPrefab, Vector2Int position)
    {
        var building = Instantiate(buildingPrefab, new Vector3(position.x, 0, position.y), Quaternion.identity);
        building.Position = position;

        if (_mapManager.PlaceBuilding(buildingPrefab))
        {
            Buildings.Add(position, building);
            return true;
        }

        Destroy(building);
        return false;
    }
    
    #region EventHandlers

    private void MapSelectorOnTileSelectedEvent(MapTile tile)
    {
        PlaceBuilding(_buildingPrefab, tile.Position);
    }

    private void InputManagerOnMainClickedEvent()
    {
    }

    #endregion
}