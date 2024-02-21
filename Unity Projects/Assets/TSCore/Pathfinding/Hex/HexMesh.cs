using System.Collections.Generic;
using TSCore.Utils;
using UnityEngine;

namespace TSCore.Pathfinding.Hex
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class HexMesh : MonoBehaviour
    {
        Mesh hexMesh;
        List<Vector3> _vertices;
        List<int> _triangles;
        List<Color> _colors;
        
        MeshCollider _collidier;
        
        void Awake()
        {
            GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
            _collidier = gameObject.AddComponent<MeshCollider>();
            
            hexMesh.name = "Hex Mesh";
            _vertices = new List<Vector3>();
            _triangles = new List<int>();
            _colors = new List<Color>();
        }

        public void Triangulate(HexCellXZ[] hexCells)
        {
            hexMesh.Clear();
            _vertices.Clear();
            _triangles.Clear();
            _colors.Clear();

            for (int i = 0; i < hexCells.Length; i++)
            {
                Triangulate(hexCells[i]);
            }
            hexMesh.vertices = _vertices.ToArray();
            hexMesh.colors = _colors.ToArray();
            hexMesh.triangles = _triangles.ToArray();
            hexMesh.RecalculateNormals();

            _collidier.sharedMesh = hexMesh;
        }

        void Triangulate(HexCellXZ cellXZ)
        {
            Vector3 center = cellXZ.transform.localPosition;
            for (int i = 0; i < 6; i++)
            {
                AddTriangle(
                    center,
                    center + HexMetrics.Corners[i],
                    center + HexMetrics.Corners[i + 1]
                );
                AddTriangleColor(cellXZ.color);
            }
        }

        void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            int vertexIndex = _vertices.Count;
            _vertices.Add(v1);
            _vertices.Add(v2);
            _vertices.Add(v3);
            _triangles.Add(vertexIndex);
            _triangles.Add(vertexIndex + 1);
            _triangles.Add(vertexIndex + 2);
        }

        void AddTriangleColor(Color color)
        {
            _colors.Add(color);
            _colors.Add(color);
            _colors.Add(color);
        }
    }
}
