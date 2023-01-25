using UnityEngine;

public class BuilderController : MonoBehaviour
{
    #region Dependencies

    [SerializeField] private InputManager _inputManager;
    [SerializeField] private MapSelector _mapSelector;
    [SerializeField] private MapManager _mapManager;

    #endregion

    [SerializeField] private Building _buildingPrefab;

    private void Awake()
    {
        _inputManager.MainClickedEvent += InputManagerOnMainClickedEvent;

        _mapSelector.TileSelectedEvent += MapSelectorOnTileSelectedEvent;
    }

    private bool PlaceBuilding(Building buildingPrefab, Vector2Int position)
    {
        var building = Instantiate(buildingPrefab, new Vector3(position.x, 0, position.y), Quaternion.identity);
        building.Position = position;

        return _mapManager.PlaceBuilding(buildingPrefab);
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