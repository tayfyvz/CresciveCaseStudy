using System.Collections.Generic;
using _GameFiles.Scripts.Controllers;
using _GameFiles.Scripts.EventArgs;
using _GameFiles.Scripts.ScriptableObjects;
using _GameFiles.Scripts.Utilities;
using EventDrivenFramework.Core;
using UnityEngine;

namespace _GameFiles.Scripts.Managers
{
    public class ConstructionManager : BaseManager
    {
        [SerializeField] private ConstructionData constructionData;
        private Queue<CubeController> _cubesQueue = new Queue<CubeController>();
        [SerializeField] private List<CubeController> _activeCubes = new List<CubeController>();

        public override void Receive(BaseEventArgs baseEventArgs)
        {
            switch (baseEventArgs)
            {
                case SceneStartedEventArgs sceneStartedEventArgs:
                    SetConstruction(sceneStartedEventArgs.LevelPref);
                    break;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            int maxCube = constructionData.MaxCube;
            CubeController cube = constructionData.CubeController;
            
            for (int i = 0; i < maxCube; i++)
            {
                CubeController cc = Instantiate(cube, transform);
                cc.gameObject.SetActive(false);
                _cubesQueue.Enqueue(cc);
            }
        }

        private void SetConstruction(int levelPref)
        {
            int level = levelPref - 1 % constructionData.ConstructionTextures.Count;
            Texture2D texture = constructionData.ConstructionTextures[level];

            float x = -texture.width / 10;
            for (int i = 0; i < texture.width; i++)
            {
                float y = -5f;
                for (int j = 0; j < texture.height; j++)
                {
                    Color cubeColor = texture.GetPixel(i, j);
                    
                    if (cubeColor.a != 0f)
                    {
                        Vector3 pos = new Vector3(x, y, 25);
                        SetCube(pos, cubeColor);
                    }

                    y += 0.5f;
                }

                x += 0.5f;
            }
            
            BroadcastUpward(new ConstructionCreatedEventArgs());
        }
        
        private void SetCube(Vector3 pos, Color color)
        {
            CubeController cube = _cubesQueue.Dequeue();
            cube.gameObject.SetActive(true);
            _activeCubes.Add(cube);
            
            cube.transform.position = pos;
            MaterialPropertyBlockUtility.ColorSetter(cube.GetComponent<Renderer>(), color);
            cube.GetComponent<Renderer>().material.color = color;
        }
    }
}