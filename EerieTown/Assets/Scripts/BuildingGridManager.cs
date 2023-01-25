// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using UnityEngine;
// using UnityEngine.EventSystems;
// using UnityEngine.Serialization;
//
// public enum SelectionType
// {
//     Rectangle,
//     Building
// }
//
// public class BuildingGridManager : MonoBehaviour
// {
//     public static BuildingGridManager Instance;
//     public static List<MapTile> Selection = new List<MapTile>();
//     public static MapTile SelectedSlot;
//     public static Building HardSelectedBuilding;
//     public static Building SelectedBuilding;
//
//     public GameObject _prefab;
//     public Transform _gridParent;
//
//     public int _gridSize = 20;
//     public int _selectionSize = 1;
//
//     public bool SelectionFreeToBuild { get; set; }
//     
//     public SelectionType _selectionType = SelectionType.Rectangle;
//
//     private Dictionary<Vector2Int, MapTile> _map = new Dictionary<Vector2Int, MapTile>();
//
//     /// Events
//     /// 
//     public delegate void SelectedSlotChanged(IEnumerable<MapTile> slots);
//
//     public event SelectedSlotChanged SelectedSlotChangedEvent;
//
//     public delegate void SelectedBuildingChangeed(Building building);
//
//     public event SelectedBuildingChangeed SelectedBuildingChangedEvent;
//     //
//
//     private void Awake()
//     {
//         Instance = this;
//     }
//
//     private void Start()
//     {
//         // float heightOffset = 0.1f;
//         // float heightDiffrence = 0; // 0.02f;
//         //
//         // for (int x = -_gridSize / 2; x < _gridSize / 2; x++)
//         // {
//         //     for (int y = -_gridSize / 2; y < _gridSize / 2; y++)
//         //     {
//         //         _map.Add(
//         //             new Vector2Int(x, y),
//         //             Instantiate(
//         //                 _prefab,
//         //                 new Vector3(
//         //                     x,
//         //                     heightOffset + heightDiffrence * (x % 2) - (y % 2 - 1) * heightDiffrence,
//         //                     y),
//         //                 Quaternion.identity,
//         //                 _gridParent).GetComponent<MapTile>()
//         //         );
//         //     }
//         // }
//     }
//
//     private void Update()
//     {
//         if (EventSystem.current.IsPointerOverGameObject())
//             return;
//
//         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//         if (Physics.Raycast(ray, out RaycastHit hit))
//         {
//             Vector2 hitPos = new Vector2(hit.point.x, hit.point.z);
//             if (_selectionType == SelectionType.Rectangle)
//             {
//                 SelectRectangle(hitPos);
//
//                 if (Input.GetMouseButtonUp(0) && Selection.Count > 0)
//                 {
//                     if (CanBuildOnSelection && Builder.instance.SelectedBuilding != null)
//                     {
//                         Builder.instance.Build(Selection.ToArray(), Builder.instance.SelectedBuilding);
//                     }
//                 }
//                 else if (Input.GetMouseButtonUp(1) && SelectedSlot != null)
//                 {
//                     if (SelectedSlot.presentBuilding != null)
//                     {
//                         Builder.instance.DestroyBuilding(SelectedSlot.presentBuilding);
//                     }
//                 }
//             }
//             else
//             {
//                 Building building = SelectBuilding(hitPos);
//                 SelectedBuilding = building;
//
//                 if (Input.GetMouseButtonDown(0))
//                 {
//                     HardSelectedBuilding = building;
//                     SelectedBuildingChangedEvent(building);
//                 }
//             }
//         }
//     }
//
//     public void AddBuildingSlotOutsideOfMainGrid(BuildingSlot slot)
//     {
//         _map.Add(Vector2Int.RoundToInt(
//                 new Vector2(slot.transform.position.x, slot.transform.position.z)),
//             slot);
//     }
//
//     public void CheckIfSelectionIsFree()
//     {
//         bool allFree = true;
//
//         if (Selection.Count != _selectionSize * _selectionSize)
//         {
//             // If there less slots in selection than there should be
//             // the selection is out of grid
//             allFree = false;
//         }
//         else
//         {
//             foreach (var slot in Selection)
//             {
//                 if (slot.presentBuilding != null)
//                 {
//                     allFree = false;
//                     break;
//                 }
//             }
//         }
//
//         SelectionFreeToBuild = allFree;
//     }
//
//     Building SelectBuilding(Vector2 pos)
//     {
//         Vector2Int posInt = Vector2Int.RoundToInt(pos);
//
//         List<BuildingSlot> tempSelection;
//         Building building = null;
//
//         if (!_map.TryGetValue(posInt, out BuildingSlot slot))
//         {
//             tempSelection = new List<BuildingSlot>();
//         }
//         else
//         {
//             building = slot.presentBuilding;
//
//             if (building)
//             {
//                 tempSelection = building.slots.Select(item => item).ToList();
//             }
//             else
//             {
//                 tempSelection = new List<BuildingSlot>();
//             }
//         }
//
//         /// ====================
//         Deselect();
//         Selection = tempSelection;
//         ApplySelection(Color.green);
//         /// =======
//         /// 
//         return building;
//     }
//
//     void SelectRectangle(Vector2 pos)
//     {
//         List<BuildingSlot> tempSelection = new List<BuildingSlot>();
//
//         /// Select tiles
//         /// 
//
//         if (_map.ContainsKey(Vector2Int.RoundToInt(pos)))
//         {
//             SelectedSlot = _map[Vector2Int.RoundToInt(pos)];
//         }
//         else
//         {
//             SelectedSlot = null;
//         }
//
//
//         if (_selectionSize % 2 == 1)
//         {
//             Vector2Int posInt = Vector2Int.RoundToInt(pos);
//
//             for (int x = posInt.x - _selectionSize / 2; x <= posInt.x + _selectionSize / 2; x++)
//             {
//                 for (int y = posInt.y - _selectionSize / 2; y <= posInt.y + _selectionSize / 2; y++)
//                 {
//                     Vector2Int v = new Vector2Int(x, y);
//                     TryAddToSelection(v, tempSelection);
//                 }
//             }
//         }
//         else
//         {
//             Vector2Int posInt = Vector2Int.RoundToInt(pos + new Vector2(0.5f, 0.5f));
//
//             for (int x = posInt.x - _selectionSize / 2; x <= posInt.x + _selectionSize / 2 - 1; x++)
//             {
//                 for (int y = posInt.y - _selectionSize / 2; y <= posInt.y + _selectionSize / 2 - 1; y++)
//                 {
//                     Vector2Int v = new Vector2Int(x, y);
//                     TryAddToSelection(v, tempSelection);
//                 }
//             }
//         }
//
//         /// Check if tempSelection is the same as selection
//         /// 
//
//         bool same = tempSelection.Count == Selection.Count && !Selection.Except(tempSelection).Any();
//
//         if (same)
//             return;
//
//         /// ====================
//         Deselect();
//         Selection = tempSelection;
//         CheckIfSelectionIsFree();
//         ApplySelection(CanBuildOnSelection ? Color.green : Color.red);
//     }
//
//     /// <summary>
//     /// Colors tiles with suitable colors and triggers SelectedSlotChangedEvent
//     /// </summary>
//     void ApplySelection(Color color)
//     {
//         SelectedSlotChangedEvent(Selection.AsEnumerable());
//         ColorSelected(color);
//     }
//
//     void TryAddToSelection(Vector2Int pos, List<BuildingSlot> selection)
//     {
//         if (_map.ContainsKey(pos))
//         {
//             selection.Add(_map[pos]);
//         }
//     }
//
//     void ColorSelected(Color color)
//     {
//         foreach (var go in Selection)
//         {
//             go.GetComponent<Renderer>().material.color = color;
//         }
//     }
//
//     void Deselect()
//     {
//         foreach (var go in Selection)
//         {
//             go.GetComponent<Renderer>().material.color = Color.white;
//         }
//
//         Selection.Clear();
//     }
// }