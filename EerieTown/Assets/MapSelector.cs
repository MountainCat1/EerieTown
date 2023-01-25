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
    [SerializeField] private LayerMask targetMask;

    #endregion

    #region Events

    public event Action<MapTile> TileSelectedEvent;
    public event Action<MapTile> TileDeselectedEvent;

    public event Action<MapTile> TileHovered;

    #endregion

    // Public properties
    public MapTile SelectedTile { get; set; }
    public MapTile HoveredTile { get; set; }


    private void Awake()
    {
        inputManager.MainClicked += InputManagerMainClicked;
    }

    private void Update()
    {
        CheckHover();
    }

    private void CheckHover()
    {
        var ray = camera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var hit))
            return;

        var point = hit.point;
        
        var mapTile = mapManager.GetClosestTile(point);
        
        if(mapTile == HoveredTile)
            return;

        HoveredTile = mapTile;
        TileHovered?.Invoke(mapTile);

        // if (!hit.transform.TryGetComponent<MapTile>(out var mapTile))
        // {
        //     TileHovered?.Invoke(null);
        // }
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