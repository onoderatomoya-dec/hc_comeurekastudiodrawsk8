using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DLMG
{
    public class DrawnLineModelGenerateDomain : IDrawnLineModelGenerator
    {
        private readonly List<Vector3> _points = new List<Vector3>();
        private readonly List<Vector3> _vertices = new List<Vector3>();
        private readonly List<int> _triangles = new List<int>();
        private readonly float _halfHeight;
        private readonly float _halfDepth;

        public int PointCount => _points.Count;
        public Mesh Mesh { get; }

        public DrawnLineModelGenerateDomain(float height, float depth)
        {
            Mesh = new Mesh();
            _halfHeight = height / 2f;
            _halfDepth = depth / 2f;
        }

        public void AppendPoint(Vector3 point)
        {
            if (!_points.Any())
            {
                _points.Add(point);
                return;
            }

            if (_points.Count == 1)
            {
                InitializeMesh(_points[0], point);
            }
            else
            {
                UpdateMesh(point);
            }

            _points.Add(point);
        }

        public List<Vector2> GetPolygonColliderPath()
        {
            var vertices = _vertices.ToArray();
            var points = new List<Vector2>();
            for (var i = 0; i < vertices.Length; i += 4)
            {
                points.Add(vertices[i]);
            }

            for (var i = vertices.Length - 1; i >= 0; i -= 4)
            {
                points.Add(vertices[i]);
            }

            return points;
        }

        void UpdateMesh(Vector3 point)
        {
            var last = _points.Last();
            var direction = point - last;
            var cross = Vector3.Cross(direction, Vector3.back).normalized;
            _vertices.AddRange(new[]
            {
                point + cross * _halfHeight + Vector3.back * _halfDepth,
                point - cross * _halfHeight + Vector3.back * _halfDepth,
                point + cross * _halfHeight + Vector3.forward * _halfDepth,
                point - cross * _halfHeight + Vector3.forward * _halfDepth
            });

            var baseIndex = (_points.Count - 1) * 4;
            _triangles.AddRange(new []
            {
                // top
                baseIndex, baseIndex + 2, baseIndex + 4,
                baseIndex + 2, baseIndex + 6, baseIndex + 4,

                // right
                baseIndex, baseIndex + 4, baseIndex + 1,
                baseIndex + 1, baseIndex + 4, baseIndex + 5,

                // bottom
                baseIndex + 3, baseIndex + 1, baseIndex + 7,
                baseIndex + 1, baseIndex + 5, baseIndex + 7,

                // left 
                baseIndex + 2, baseIndex + 3, baseIndex + 6,
                baseIndex + 3, baseIndex + 7, baseIndex + 6,

                // far
                baseIndex + 5, baseIndex + 4, baseIndex + 7,
                baseIndex + 7, baseIndex + 4, baseIndex + 6
            });
            
            ApplyMeshData();
        }

        void InitializeMesh(Vector3 from, Vector3 to)
        {
            var direction = to - from;
            var cross = Vector3.Cross(direction, Vector3.back).normalized;
            _vertices.AddRange(new[]
            {
                from + cross * _halfHeight + Vector3.back * _halfDepth,
                from - cross * _halfHeight + Vector3.back * _halfDepth,
                from + cross * _halfHeight + Vector3.forward * _halfDepth,
                from - cross * _halfHeight + Vector3.forward * _halfDepth,
                to + cross * _halfHeight + Vector3.back * _halfDepth,
                to - cross * _halfHeight + Vector3.back * _halfDepth,
                to + cross * _halfHeight + Vector3.forward * _halfDepth,
                to - cross * _halfHeight + Vector3.forward * _halfDepth
            });

            _triangles.AddRange(new[]
            {
                // near
                0, 1, 3,
                0, 3, 2,
                // top
                0, 2, 4,
                2, 6, 4,
                // right
                0, 4, 1,
                1, 4, 5,
                // bottom
                3, 1, 7,
                1, 5, 7,
                // left
                2, 3, 6,
                3, 7, 6,
                // far
                5, 4, 7,
                7, 4, 6
            });

            ApplyMeshData();
        }

        void ApplyMeshData()
        {
            Mesh.SetVertices(_vertices);
            Mesh.SetTriangles(_triangles, 0);
            Mesh.RecalculateNormals();
        }
    }
}
