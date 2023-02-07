using _GameFiles.Scripts.Controllers;
using UnityEngine;

namespace _GameFiles.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "coinData", menuName = "Coin Data", order = 56)]

    public class CoinData : ScriptableObject
    {
        [SerializeField] private CoinController coinPrefab;
        [SerializeField] private int maxCoinNum;

        
        public CoinController CoinPrefab => coinPrefab;
        public int MaxCoinNum => maxCoinNum;
    }
}