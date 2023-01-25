using System;
using Unity.VisualScripting;
using UnityEngine;

public class MapSelector : MonoBehaviour
{
    #region Dependencies

    [SerializeField] private InputManager inputManager;
    [SerializeField] private MapManager mapManager;
    [SerializeField] private new Camera camera;

    #endregion

    #region Configuration

    [SerializeField] private string targetTags;

    #endregion

    #region Events

    public event Action<MapTile> TileSelectedEvent;
    public event Action<MapTile> TileDeselectedEvent;

    #endregion

    // Public properties
    public MapTile SelectedTile { get; set; }


    private void Awake()
    {
        inputManager.MainClicked += InputManagerMainClicked;
    }

    private void InputManagerMainClicked()
    {
        var ray = camera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var hit))
            return;

        var point = hit.point;

        var hitTile = mapManager.GetClosestTile(point);

        SelectTile(hitTile);
    }

    private void SelectTile(MapTile mapTile)
    {
        if (SelectedTile is not null)
            TileDeselectedEvent?.Invoke(SelectedTile);

        SelectedTile = mapTile;
        
        TileSelectedEvent?.Invoke(mapTile);
    }
}