using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using _GameFiles.Scripts.Controllers;
using _GameFiles.Scripts.EventArgs;
using _GameFiles.Scripts.Models;
using EventDrivenFramework.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _GameFiles.Scripts.Managers
{
    public class LevelManager : BaseManager
    {
        private GameModel _gameModel;
        public override void Receive(BaseEventArgs baseEventArgs)
        {
            switch (baseEventArgs)
            {
                case CreateObjectsEventArgs createObjectsEventArgs:
                    CreateObjects(
                        createObjectsEventArgs.Max,
                        createObjectsEventArgs.Prefab,
                        createObjectsEventArgs.Transform,
                        createObjectsEventArgs.Queue
                    );
                    break;
                case NextLevelButtonClickedEventArgs  _:
                    ResetLevel();
                    break;
            }
        }

        //Increases level and resets scene.
        private void ResetLevel()
        {
            _gameModel.Level++;
            SceneManager.LoadSceneAsync(0);
        }

        //Awakes the other managers.
        protected override void Start()
        {
            Broadcast(new SceneStartedEventArgs(_gameModel.Level));
        }
        
        //Main object pooling creator.
        private void CreateObjects(int max, Controller prefab, Transform parentTransform, Queue<Controller> queue)
        {
            for (int i = 0; i < max; i++)
            {
                Controller item = Instantiate(prefab, parentTransform);
                item.gameObject.SetActive(false);
                queue.Enqueue(item);
            }
        }
        public void InjectModel(GameModel gameModel)
        {
            _gameModel = gameModel;
            _gameModel.PropertyChanged += GameMOdelProperetyChangedHandler;
        }
        private void GameMOdelProperetyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_gameModel.Level))
            {
                
            }
        }
    }
}