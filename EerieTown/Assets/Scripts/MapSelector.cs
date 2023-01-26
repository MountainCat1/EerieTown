using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapSelector : MonoBehaviour
{
    #region Dependencies

    [SerializeField] private InputManager _inputManager;
    [SerializeField] private MapManager _mapManager;
    [SerializeField] private Camera _camera;

    #endregion

    #region Configuration

    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private GameObject _selectionMarkerPrefab;
    [SerializeField] private float _selectionMarkersYPosition = 0.125f;

    #endregion

    private readonly List<GameObject> _instantiatedSelectionMarkers = new();

    #region Events

    public event Action<MapTile> TileSelectedEvent;
    public event Action<MapTile> TileDeselectedEvent;
    public event Action<MapTile> TileHoveredEvent;
    public event Action<List<MapTile>, List<MapTile>> SelectionChanged;

    #endregion

    // Public properties
    public MapTile SelectedTile { get; private set; }
    public MapTile HoveredTile { get; private set; }
    public List<MapTile> Selection { get; private set; } = new();

    [field: SerializeField] public int SelectionSize { get; set; } = 1;


    private void Awake()
    {
        _inputManager.MainClickedEvent += InputManagerMainClickedEvent;

        SelectionChanged += (_, newSelection) => UpdateSelectionMarkers(newSelection);
    }

    private void Update()
    {
        CheckHover();

        UpdateSelection();
    }

    private void UpdateSelection()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var hit,  float.PositiveInfinity, _targetMask))
            return;

        var point = hit.point;

        var newSelection = SelectRectangle(new Vector2(point.x, point.z), SelectionSize);
        
        // Check if old selection is the same as an old one
        bool same = newSelection.Count == Selection.Count && !Selection.Except(newSelection).Any();

        if (!same)
        {
            SelectionChanged?.Invoke(Selection, newSelection);
            Selection = newSelection;
        }
    }

    private void UpdateSelectionMarkers(List<MapTile> selection)
    {
        foreach (var instantiatedSelectionMarker in _instantiatedSelectionMarkers)
        {
            Destroy(instantiatedSelectionMarker);
        }
        
        _instantiatedSelectionMarkers.Clear();
        
        foreach (var tile in selection)
        {
            var tilePosition = tile.Position;
            var go = Instantiate(_selectionMarkerPrefab,
                new Vector3(tilePosition.x, _selectionMarkersYPosition, tilePosition.y),
                Quaternion.identity);
            
            _instantiatedSelectionMarkers.Add(go);
        }
    }

    
    
    private void CheckHover()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var hit))
            return;

        var point = hit.point;

        var mapTile = _mapManager.GetClosestTile(point);

        if (mapTile == HoveredTile)
            return;

        HoveredTile = mapTile;
        TileHoveredEvent?.Invoke(mapTile);
    }

    private void InputManagerMainClickedEvent()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var hit, float.PositiveInfinity, _targetMask))
            return;

        var point = hit.point;

        var hitTile = _mapManager.GetClosestTile(point);

        SelectTile(hitTile);
    }

    private void SelectTile(MapTile mapTile)
    {
        if (SelectedTile is not null)
            TileDeselectedEvent?.Invoke(SelectedTile);

        SelectedTile = mapTile;

        TileSelectedEvent?.Invoke(mapTile);
    }

    List<MapTile> SelectRectangle(Vector2 pos, int selectionSize)
    {
        var selection = new List<MapTile>();
        
        if (selectionSize % 2 == 1)
        {
            var posInt = Vector2Int.RoundToInt(pos);

            for (int x = posInt.x - selectionSize / 2; x <= posInt.x + selectionSize / 2; x++)
            {
                for (int y = posInt.y - selectionSize / 2; y <= posInt.y + selectionSize / 2; y++)
                {
                    var position = new Vector2Int(x, y);
                    selection.Add(_mapManager.GetRequiredMapTile(position));
                }
            }
        }
        else
        {
            var posInt = Vector2Int.RoundToInt(pos + new Vector2(0.5f, 0.5f));

            for (int x = posInt.x - selectionSize / 2; x <= posInt.x + selectionSize / 2 - 1; x++)
            {
                for (int y = posInt.y - selectionSize / 2; y <= posInt.y + selectionSize / 2 - 1; y++)
                {
                    var position = new Vector2Int(x, y);
                    selection.Add(_mapManager.GetRequiredMapTile(position));
                }
            }
        }

        return selection;
    }
}