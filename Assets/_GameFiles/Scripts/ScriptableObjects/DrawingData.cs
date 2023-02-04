using _GameFiles.Scripts.Controllers;
using UnityEngine;

namespace _GameFiles.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "drawingData", menuName = "Drawing Data", order = 53)]

    public class DrawingData : ScriptableObject
    {
        [SerializeField] private DrawingController drawingPrefab;

        [SerializeField] private Gradient gradientColor;
        [SerializeField] private float pointsDistance;
        [SerializeField] private float lineWidth;
        
        public DrawingController DrawingPrefab => drawingPrefab;
        public Gradient GradientColor => gradientColor;
        public float PointsDistance => pointsDistance;
        public float LineWidth => lineWidth;
    }
}