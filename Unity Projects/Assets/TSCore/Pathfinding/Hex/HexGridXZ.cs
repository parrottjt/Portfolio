using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace TSCore.Pathfinding.Hex
{
    public class HexGridXZ : MonoBehaviour
    {
        [SerializeField] int _width = 6;
        [SerializeField] int _height = 6;
        [SerializeField] Color _cellColor = Color.white;
        [SerializeField] Color _touchedColor = Color.magenta;
        
        [FormerlySerializedAs("cellXZPrefab")] [SerializeField] HexCellXZ _cellXZPrefab;
        [SerializeField] Text _cellLabelPrefab;
        
        HexCellXZ[] _cells;
        Canvas _gridCanvas;
        HexMesh _hexMesh;
        
        void Awake()
        {
            _gridCanvas = GetComponentInChildren<Canvas>();
            _hexMesh = GetComponentInChildren<HexMesh>();
            
            _cells = new HexCellXZ[_height * _width];

            for (int z = 0, i = 0; z < _height; z++)
            {
                for (int x = 0; x < _width; x++)
                {
                    CreateCell(x, z, i++);
                }
            }
        }

        void Start()
        {
            _hexMesh.Triangulate(_cells);
        }

        void Update()
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                var inputRay = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                if (Physics.Raycast(inputRay, out var hit))
                {
                    TouchCell(hit.point);
                }
            }
        }

        void CreateCell(int x, int z, int i)
        {
            Vector3 position = HexMath.CellPosition(x, z);

            var cellObject = ObjectPooling.GetAvailable(_cellXZPrefab.gameObject, true);
            var cellTransform = cellObject.transform;
            cellTransform.SetParent(transform, false);
            cellTransform.localPosition = position;
            
            var cell = CellSetup(x, z, i, cellObject);

            var uiRectObject = ObjectPooling.GetAvailable(_cellLabelPrefab.gameObject, true);
            cell.uiRect = uiRectObject.GetComponent<RectTransform>();
            cell.uiRect.SetParent(_gridCanvas.transform, false);
            cell.uiRect.anchoredPosition = new Vector2(position.x, position.z);
            
            cell.DisableHighlight();
            
            ToggleLabel(cell, false);
        }

        HexCellXZ CellSetup(int x, int z, int i, GameObject cellObject)
        {
            var cell = _cells[i] = cellObject.GetComponent<HexCellXZ>();
            cell.coordinates = HexCoordinatesXZ.FromOffsetCoordinates(x, z);
            cell.color = _cellColor;
            SetNeighbors(x, z, i, _cells);

            return cell;
        }

        void ToggleLabel(HexCellXZ cell, bool value)
        {
            var label = cell.uiRect.GetChild(1).GetComponent<Text>();
            label.text = value ? cell.coordinates.ToStringOnSeparateLines() : "";
        }
        
        void SetNeighbors(int x, int z, int i, HexCellXZ[] cells)
        {
            var cell = cells[i];
            if (x > 0)
            {
                cell.SetNeighbor(HexDirection.W, _cells[i - 1]);
            }

            if (z > 0)
            {
                if ((z & 1) == 0)
                {
                    cell.SetNeighbor(HexDirection.SE, _cells[i - _width]);
                    if (x > 0)
                    {
                        cell.SetNeighbor(HexDirection.SW, _cells[i - _width - 1]);
                    }
                }
                else
                {
                    cell.SetNeighbor(HexDirection.SW, _cells[i - _width]);
                    if (x < _width - 1)
                    {
                        cell.SetNeighbor(HexDirection.SE, _cells[i - _width + 1]);
                    }
                }
            }
        }

        public void TouchCell(Vector3 position)
        {
            position = transform.InverseTransformPoint(position);
            HexCoordinatesXZ coordinates = HexCoordinatesXZ.FromPosition(position);
            int index = coordinates.X + coordinates.Z * _width + coordinates.Z / 2;
            HexCellXZ cell = _cells[index];
            cell.ToggleHighlight(_touchedColor);
        }
    }
}
