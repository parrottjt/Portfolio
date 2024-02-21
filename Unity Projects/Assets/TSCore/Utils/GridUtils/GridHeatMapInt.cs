using UnityEngine;
using FrikinCore.Utils.Utils;
using TSCore.Pathfinding;
using TSCore.Utils;

public class GridHeatMapInt : MonoBehaviour
{
    public const int HEAT_MAP_MAX_VALUE = 100;
    public const int HEAT_MAP_MIN_VALUE = 0;

    GridXY<int> _grid;
    Mesh _mesh;
    bool _updateMesh;

    void Awake()
    {
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;
    }

    void LateUpdate()
    {
        if (_updateMesh)
        {
            _updateMesh = false;
            UpdateGeatMapVisual();
        }
    }

    public void SetGrid(GridXY<int> grid)
    {
        _grid = grid;
        UpdateGeatMapVisual();
    }

    void OnGridValueChanged(object sender, GridXY<int>.OnGridValueChangedEventArgs e)
    {
        _updateMesh = true;
    }
    void UpdateGeatMapVisual()
    {
        MeshUtils.CreateEmptyMeshArrays(_grid.Width * _grid.Height, out Vector3[] vertices, out Vector2[] uv,
            out int[] triangles);

        for (int x = 0; x < _grid.Width; x++)
        {
            for (int y = 0; y < _grid.Height; y++)
            {
                int index = x * _grid.Height + y;
                Vector3 quadSize = new Vector3(1, 1) * _grid.CellSize;

                int gridCellValue = _grid.GetGridObject(x, y);
                float gridCellValueNormalized = (float) gridCellValue / HEAT_MAP_MAX_VALUE;
                Vector2 gridCellValueUV = new Vector2(gridCellValueNormalized, 0f);
                MeshUtils.AddToMeshArrays(vertices, uv, triangles, index, _grid.GetWorldPosition(x,y)+quadSize *.5f, 0f, quadSize, gridCellValueUV, gridCellValueUV);
            }
        }
    }
}
