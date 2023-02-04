using System.Collections.Generic;
using UnityEngine;

namespace _GameFiles.Scripts.Interfaces
{
    public interface IDrawing 
    {
        public LineRenderer LineRenderer { get; set; }
        public Rigidbody2D Rb2D { get; set; }
        public EdgeCollider2D EdgeCollider { get; set; }

        public int PointsCount { get; set; }
        public float PointsDistance { get; set; }

        public void AddPoint(Vector3 point);
        public Vector3 GetLastPoint();
        public void SetPointsDistance(float distance);
        public void SetLineWidth(float width);
        public void SetLineColor(Gradient gradientColor);
    }
}
