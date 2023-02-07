using System.Collections.Generic;
using _GameFiles.Scripts.Controllers;
using _GameFiles.Scripts.EventArgs;
using _GameFiles.Scripts.ScriptableObjects;
using EventDrivenFramework.Core;
using UnityEngine;

namespace _GameFiles.Scripts.Managers
{
    public class ConstructionManager : BaseManager
    {
        [SerializeField] private ConstructionData constructionData;
        
        private readonly Queue<Controller> _cubesQueue = new Queue<Controller>();

        private int _explodedCubeCounter;
        private int _startCubeCounter;

        public override void Receive(BaseEventArgs baseEventArgs)
        {
            switch (baseEventArgs)
            {
                case SceneStartedEventArgs sceneStartedEventArgs:
                    SetConstruction(sceneStartedEventArgs.LevelPref);
                    break;
            }
        }

        //Creates cubes by using Object Pooling.
        protected override void Awake()
        {
            base.Awake();
            Broadcast(new CreateObjectsEventArgs(
                constructionData.MaxCube,
                constructionData.CubeController,
                transform,
                _cubesQueue
                ));
        }

        //Sets building by getting textures from Construction Data.
        private void SetConstruction(int levelPref)
        {
            int level = (levelPref - 1) % (constructionData.ConstructionTextures.Count);
            Texture2D texture = constructionData.ConstructionTextures[level];

            float x = -texture.width / 10f;
            
            for (int i = 0; i < texture.width; i++)
            {
                float y = -2f;
                for (int j = 0; j < texture.height; j++)
                {
                    Color cubeColor = texture.GetPixel(i, j);
                    
                    if (cubeColor.a != 0f)
                    {
                        _startCubeCounter++;
                        Vector3 pos = new Vector3(x, y, 25);
                        ActivateCube(pos, cubeColor);
                    }

                    y += 0.5f;
                }

                x += 0.5f;
            }

            _explodedCubeCounter = 0;
            BroadcastUpward(new ConstructionCreatedEventArgs());
        }
        
        //Activates cube and send its position and color.
        private void ActivateCube(Vector3 pos, Color color)
        {
            Controller cube = _cubesQueue.Dequeue();
            cube.gameObject.SetActive(true);
            CubeController cc = cube.GetComponent<CubeController>();
            cc.OnCubeIsExplodeEvent += OnCubeIsExplodeEventHandler;
            cc.SetCube(pos, color);
        }

        //When the cube is explode, stops listening its event and checks level is end or not.
        private void OnCubeIsExplodeEventHandler(CubeController cube)
        {
            _cubesQueue.Enqueue(cube);
            cube.OnCubeIsExplodeEvent -= OnCubeIsExplodeEventHandler;
            _explodedCubeCounter++;
            
            Broadcast(new CubeIsExplodedEventArgs(cube.transform.position));
            
            if (_explodedCubeCounter  >= (_startCubeCounter* 90/100))
            {
                BroadcastUpward(new LevelFinishedEventArgs());
            }
        }
    }
}