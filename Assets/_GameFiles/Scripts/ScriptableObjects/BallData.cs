using _GameFiles.Scripts.Controllers;
using UnityEngine;

namespace _GameFiles.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ballData", menuName = "Ball Data", order = 54)]

    public class BallData : ScriptableObject
    {
        [SerializeField] private BallController ballControllerPrefabPrefab;
        [SerializeField] private int maxBall;
        [SerializeField] private int explodeAmount;
        [SerializeField] private float checkRadius;
        [SerializeField] private LayerMask cubeLayer;
        [SerializeField] private LayerMask drawingLayer;

        public BallController BallControllerPrefab => ballControllerPrefabPrefab;
        public int MaxBall => maxBall;
        public int ExplodeAmount => explodeAmount;
        public float CheckRadius => checkRadius;
        public LayerMask CubeLayer => cubeLayer;
        public LayerMask DrawingLayer => drawingLayer;

    }
}