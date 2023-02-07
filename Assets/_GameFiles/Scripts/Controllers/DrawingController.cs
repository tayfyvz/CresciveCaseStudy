using System.Collections.Generic;
using _GameFiles.Scripts.Interfaces;
using UnityEngine;

namespace _GameFiles.Scripts.Controllers
{
    //Drawing object and its controller.
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

            // gameObject.tag = "Drawing";
            Rb2D.isKinematic = true;
        }

        //Adds new point to the renderer and sets collider on it.
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

        //Sets distance between points.
        public void SetPointsDistance(float distance)
        {
            PointsDistance = distance;
        }

        //Sets width of line.
        public void SetLineWidth(float width)
        {
            LineRenderer.startWidth = width;
            LineRenderer.endWidth = width;

            EdgeCollider.edgeRadius = width / 2;
        }

        //Sets color of line.
        public void SetLineColor(Gradient gradientColor)
        {
            LineRenderer.colorGradient = gradientColor;
        }

        //Resets line.
        public void ResetDrawing()
        {
            LineRenderer.positionCount = 0;
            PointsCount = 0;
            points.Clear();
            EdgeCollider.points = points.ToArray();
        }
    }
}