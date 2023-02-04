using _GameFiles.Scripts.Controllers;
using UnityEngine;

namespace _GameFiles.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ballData", menuName = "Ball Data", order = 54)]

    public class BallData : ScriptableObject
    {
        [SerializeField] private int maxBall;
        [SerializeField] private BallController ballControllerPrefabPrefab;

        public int MaxBall => maxBall;
        public BallController BallControllerPrefab => ballControllerPrefabPrefab;
    }
}