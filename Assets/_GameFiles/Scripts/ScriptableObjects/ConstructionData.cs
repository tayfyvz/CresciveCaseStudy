using System.Collections.Generic;
using _GameFiles.Scripts.Controllers;
using UnityEngine;

namespace _GameFiles.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "constructionData", menuName = "Construction Data", order = 52)]
    public class ConstructionData : ScriptableObject
    {
        [SerializeField] private List<Texture2D> constructionTextures;
        [SerializeField] private int maxCube;
        [SerializeField] private CubeController cubeController;

        public List<Texture2D> ConstructionTextures => constructionTextures;
        public int MaxCube => maxCube;
        public CubeController CubeController => cubeController;
    }
}
