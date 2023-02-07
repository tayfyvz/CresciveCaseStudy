using _GameFiles.Scripts.Controllers;
using UnityEngine;

namespace _GameFiles.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "effectData", menuName = "Effect Data", order = 55)]

    public class EffectData : ScriptableObject
    {
        [SerializeField] private ParticleEffectController explodeEffectPrefab;
        [SerializeField] private ParticleEffectController cubeBreakEffectPrefab;

        [SerializeField] private int maxExplodeEffectNum;
        [SerializeField] private int maxCubeBreakEffectNum;

        
        public ParticleEffectController ExplodeEffectPrefab => explodeEffectPrefab;
        public ParticleEffectController CubeBreakEffectPrefab => cubeBreakEffectPrefab;

        public int MaxExplodeEffectNum => maxExplodeEffectNum;
        public int MaxCubeBreakEffectNum => maxCubeBreakEffectNum;

    }
}