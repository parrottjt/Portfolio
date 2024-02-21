using System;
using TSCore.Utils;
using UnityEngine;

namespace TSCore.Pathfinding
{
    public class GridXZ<TGridObject>
    {
        public event EventHandler<OnGridValueChangedEventArgs> OnGridObjectChanged;

        public class OnGridValueChangedEventArgs : EventArgs
        {
            public int x, z;
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

        public GridXZ(int width, int height, float cellSize, Vector3 originPosition,
            Func<GridXZ<TGridObject>, int, int, TGridObject> createGridObject, bool showDebug = true)
        {
            _width = width;
            _height = height;
            _cellSize = cellSize;
            _originPosition = originPosition;

            _gridArray = new TGridObject[width, height];

            for (int x = 0; x < _gridArray.GetLength(0); x++)
            {
                for (int z = 0; z < _gridArray.GetLength(1); z++)
                {
                    _gridArray[x, z] = createGridObject(this, x, z);
                }
            }

            if (showDebug) _debugTextArray = new TextMesh[width, height];

            ShowDebug(showDebug);
        }

        public void ShowDebug(bool showDebug)
        {
            if (showDebug)
            {
                if (_debugTextArray.GetLength(0) == 0)
                {
                    _debugTextArray = new TextMesh[_width, _height];
                }

                for (int x = 0; x < _gridArray.GetLength(0); x++)
                {
                    for (int z = 0; z < _gridArray.GetLength(1); z++)
                    {
                        _debugTextArray[x, z] = GameObjectUtils.CreateWorldText(null, _gridArray[x, z]?.ToString(),
                            GetWorldPosition(x, z) + new Vector3(_cellSize, 1, _cellSize) * .5f, 20, Color.white,
                            TextAnchor.MiddleCenter, TextAlignment.Center, new Vector3(90, 0, 0));
                        Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z + 1), Color.white, 100f);
                        Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x + 1, z), Color.white, 100f);
                    }

                    Debug.DrawLine(GetWorldPosition(0, _height), GetWorldPosition(_width, _height), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(_width, 0), GetWorldPosition(_width, _height), Color.white, 100f);
                }

                OnGridObjectChanged += (sender, eventArgs) =>
                {
                    _debugTextArray[eventArgs.x, eventArgs.z].text =
                        _gridArray[eventArgs.x, eventArgs.z]?.ToString();
                };
            }
        }

        public Vector3 GetWorldPosition(int x, int z)
        {
            return new Vector3(x, 1, z) * _cellSize + _originPosition;
        }

        public void GetXZ(Vector3 worldPosition, out int x, out int z)
        {
            x = Mathf.FloorToInt((worldPosition - _originPosition).x / _cellSize);
            z = Mathf.FloorToInt((worldPosition - _originPosition).z / _cellSize);
        }

        public void SetValue(int x, int z, TGridObject value)
        {
            if (x >= 0 && z >= 0 && x < _width && z < _height)
            {
                _gridArray[x, z] = value;
                _debugTextArray[x, z].text = _gridArray[x, z]?.ToString();
            }

            OnGridObjectChanged?.Invoke(this, new OnGridValueChangedEventArgs { x = x, z = z });
        }

        public void SetValue(Vector3 worldPosition, TGridObject value)
        {
            GetXZ(worldPosition, out var x, out var z);
            SetValue(x, z, value);
        }

        public void TriggerGridObjectChanged(int x, int z)
        {
            OnGridObjectChanged?.Invoke(this, new OnGridValueChangedEventArgs { x = x, z = z });
        }


        public TGridObject GetGridObject(int x, int z)
        {
            if (x >= 0 && z >= 0 && x < _width && z < _height)
            {
                return _gridArray[x, z];
            }

            return default;
        }

        public TGridObject GetGridObject(Vector3 worldPosition)
        {
            GetXZ(worldPosition, out var x, out var z);
            return GetGridObject(x, z);
        }
    }
}
