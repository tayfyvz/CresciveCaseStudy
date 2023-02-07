using _GameFiles.Scripts.Controllers;
using UnityEngine;

namespace _GameFiles.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ballData", menuName = "Ball Data", order = 54)]

    public class BallData : ScriptableObject
    {
        [SerializeField] private BallController ballControllerPrefabPrefab;
        [SerializeField] private int maxBallNum;
        [SerializeField] private int explodeAmount;
        [SerializeField] private float checkRadius;
        [SerializeField] private float releaseHeight;
        [SerializeField] private LayerMask cubeLayer;
        [SerializeField] private LayerMask drawingLayer;

        public BallController BallControllerPrefab => ballControllerPrefabPrefab;
        public int MaxBallNum => maxBallNum;
        public int ExplodeAmount => explodeAmount;
        public float CheckRadius => checkRadius;
        public float ReleaseHeight => releaseHeight;

        public LayerMask CubeLayer => cubeLayer;
        public LayerMask DrawingLayer => drawingLayer;

    }
}