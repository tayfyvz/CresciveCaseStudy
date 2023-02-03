using System.ComponentModel;
using _GameFiles.Scripts.EventArgs;
using _GameFiles.Scripts.Models;
using EventDrivenFramework;
using EventDrivenFramework.Core;
using EventDrivenFramework.Game;
using UnityEngine;

namespace _GameFiles.Scripts.Managers._HighLevelManagers
{
    public class GameManager : BaseGameManager
    {
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private InputManager inputManager;
        [SerializeField] private ConstructionManager constructionManager;
        [SerializeField] private DrawManager drawManager;
        private GameModel _gameModel;
        public override void Receive(BaseEventArgs baseEventArgs)
        {
            switch (baseEventArgs)
            {
                case ConstructionCreatedEventArgs _:
                    inputManager.gameObject.SetActive(true);
                    break;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            IMediator mediator = new BaseMediator();
            levelManager.InjectMediator(mediator);
            levelManager.InjectManager(this);
            levelManager.InjectModel(_gameModel);
            
            inputManager.InjectMediator(mediator);
            inputManager.InjectManager(this);
            inputManager.gameObject.SetActive(false);
            
            constructionManager.InjectMediator(mediator);
            constructionManager.InjectManager(this);
            
            drawManager.InjectMediator(mediator);
            drawManager.InjectManager(this);
        }

        public void InjectModel(GameModel gameModel)
        {
            this._gameModel = gameModel;
            this._gameModel.PropertyChanged += GameMOdelProperetyChangedHandler;
        }

        private void GameMOdelProperetyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_gameModel.InstantScore))
            {
                
            }
        }
    }
}