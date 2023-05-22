using System.Collections.Generic;
using UnityEngine;

namespace DLMG
{
    public interface IDrawnLineModelGenerator
    {
        int PointCount { get; }
        Mesh Mesh { get; }
        void AppendPoint(Vector3 point);
        List<Vector2> GetPolygonColliderPath();
    }
}
