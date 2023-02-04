using System;
using System.Collections.Generic;
using _GameFiles.Scripts.Interfaces;
using UnityEngine;

namespace _GameFiles.Scripts.Controllers
{
    public class DrawingController : MonoBehaviour, IDrawing
    {
        public LineRenderer LineRenderer { get; set; }
        
        public Rigidbody2D Rb2D { get; set; }
        
        public EdgeCollider2D EdgeCollider { get; set; }

        public List<Vector2> points = new List<Vector2>();

        public int PointsCount { get; set; }
        
        public float PointsDistance { get; set; }

        private void Awake()
        {
            LineRenderer = GetComponent<LineRenderer>();
            Rb2D = GetComponent<Rigidbody2D>();
            EdgeCollider = GetComponent<EdgeCollider2D>();
            
            Rb2D.isKinematic = true;
        }

        public void AddPoint(Vector3 point)
        {
            if ((PointsCount >= 1 && Vector2.Distance(point, GetLastPoint()) < PointsDistance)
                || PointsCount > 30)
            {
                return;
            }

            points.Add(point);
            PointsCount++;
            
            LineRenderer.positionCount = PointsCount;
            LineRenderer.SetPosition(PointsCount - 1, point);

            if (PointsCount > 1)
            {
                EdgeCollider.points = points.ToArray();
            }
        }

        public Vector3 GetLastPoint()
        {
            return LineRenderer.GetPosition(PointsCount - 1);
        }
        public Vector3 GetMinPoint()
        {
            return LineRenderer.bounds.min;
        }

        public void SetPointsDistance(float distance)
        {
            PointsDistance = distance;
        }

        public void SetLineWidth(float width)
        {
            LineRenderer.startWidth = width;
            LineRenderer.endWidth = width;

            EdgeCollider.edgeRadius = width / 2;
        }

        public void SetLineColor(Gradient gradientColor)
        {
            LineRenderer.colorGradient = gradientColor;
        }

        public void ResetDrawing()
        {
            LineRenderer.positionCount = 0;
            PointsCount = 0;
            points.Clear();
            EdgeCollider.points = points.ToArray();
        }
    }
}