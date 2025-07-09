using System;
using TSCore.Utils;
using UnityEngine;

namespace TSCore.Pathfinding
{
    public class GridXY<TGridObject>
    {
        public event EventHandler<OnGridValueChangedEventArgs> OnGridObjectChanged;

        public class OnGridValueChangedEventArgs : EventArgs
        {
            public int x, y;
        }

        int _width;
        int _height;
        TGridObject[,] _gridArray;
        float _cellSize;

        TextMesh[,] _debugTextArray;
        Vector3 _originPosition;

        public int Width => _width;
        public int Height => _height;
        public float CellSize => _cellSize;
        (int, int) _centerCell;
        public (int X, int Y) CenterCell => _centerCell;
        
        public GridXY(int width, int height, float cellSize, Transform originPosition,
            Func<GridXY<TGridObject>, int, int, TGridObject> createGridObject, bool showDebug = true)
        {
            _width = width;
            _height = height;
            _cellSize = cellSize;
            _originPosition = originPosition.position;

            var centerX = width / 2;
            var centerY = height / 2;
            _centerCell = (centerX, centerY);
            
            _gridArray = new TGridObject[width, height];

            for (int x = 0; x < _gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < _gridArray.GetLength(1); y++)
                {
                    _gridArray[x, y] = createGridObject(this, x, y);
                }
            }

            if (showDebug) _debugTextArray = new TextMesh[width, height];

            ShowDebug(showDebug, originPosition);
        }

        public void ShowDebug(bool showDebug, Transform parent = null)
        {
            if (showDebug)
            {
                if (_debugTextArray.GetLength(0) == 0)
                {
                    _debugTextArray = new TextMesh[_width, _height];
                }

                for (int x = 0; x < _gridArray.GetLength(0); x++)
                {
                    for (int y = 0; y < _gridArray.GetLength(1); y++)
                    {
                        _debugTextArray[x, y] = GameObjectUtils.CreateWorldText(parent, _gridArray[x, y]?.ToString(),
                            GetWorldPosition(x, y) + new Vector3(_cellSize, _cellSize) * .5f, 20, Color.white,
                            TextAnchor.MiddleCenter, TextAlignment.Center);
                        Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                        Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                    }

                    Debug.DrawLine(GetWorldPosition(0, _height), GetWorldPosition(_width, _height), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(_width, 0), GetWorldPosition(_width, _height), Color.white, 100f);
                }

                OnGridObjectChanged += (sender, eventArgs) =>
                {
                    _debugTextArray[eventArgs.x, eventArgs.y].text =
                        _gridArray[eventArgs.x, eventArgs.y]?.ToString();
                };
            }
        }

        public Vector3 GetWorldPosition(int x, int y)
        {
            return new Vector3(x, y) * _cellSize + _originPosition;
        }

        public void GetXY(Vector3 worldPosition, out int x, out int y)
        {
            x = Mathf.FloorToInt((worldPosition - _originPosition).x / _cellSize);
            y = Mathf.FloorToInt((worldPosition - _originPosition).y / _cellSize);
        }
        public Vector3 GetCenterPositionOfCell((int x, int y) cell) => 
            GetWorldPosition(cell.x, cell.y) - _originPosition + new Vector3(_cellSize, _cellSize) * .5f;

        public bool CellValid((int x, int y) cell)
        {
            return CellInGrid(cell) && GetGridObject(cell) == null;
        }

        public bool CellInGrid((int x, int y) cell)
        {
            return Conditions.ValueWithinRange(cell.x, (0, _gridArray.GetLength(0) - 1)) &&
                   Conditions.ValueWithinRange(cell.y, (0, _gridArray.GetLength(1) - 1));
        }
        public void SetValue((int x, int y) cell, TGridObject value) => SetValue(cell.x, cell.y, value);
        public void SetValue(int x, int y, TGridObject value)
        {
            if (x >= 0 && y >= 0 && x < _width && y < _height)
            {
                _gridArray[x, y] = value;
            }

            OnGridObjectChanged?.Invoke(this, new OnGridValueChangedEventArgs { x = x, y = y });
        }

        public void SetValue(Vector3 worldPosition, TGridObject value)
        {
            GetXY(worldPosition, out var x, out var y);
            SetValue(x, y, value);
        }

        public void TriggerGridObjectChanged(int x, int y)
        {
            OnGridObjectChanged?.Invoke(this, new OnGridValueChangedEventArgs { x = x, y = y });
        }

        public TGridObject GetGridObject((int x, int y) cell) => GetGridObject(cell.x, cell.y);
        public TGridObject GetGridObject(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < _width && y < _height)
            {
                return _gridArray[x, y];
            }

            return default;
        }

        public TGridObject GetGridObject(Vector3 worldPosition)
        {
            int x, y;
            GetXY(worldPosition, out x, out y);
            return GetGridObject(x, y);
        }
    }
}