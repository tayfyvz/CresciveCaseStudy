using System.ComponentModel;
using _GameFiles.Scripts.EventArgs;
using _GameFiles.Scripts.Models;
using EventDrivenFramework.Core;
using EventDrivenFramework.Game;
using UnityEngine;

namespace _GameFiles.Scripts.Managers._HighLevelManagers
{
    //Builds the event driven infrastructure by setting up the communication between
    // level manager, input manager, construction manager, drawing manager, ball manager, explode effect manager and collector manager.
    public class GameManager : BaseGameManager
    {
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private InputManager inputManager;
        [SerializeField] private ConstructionManager constructionManager;
        [SerializeField] private DrawingManager drawingManager;
        [SerializeField] private BallManager ballManager;
        [SerializeField] private ExplodeEffectManager explodeEffectManager;
        [SerializeField] private CollectorManager collectorManager;
        private GameModel _gameModel;
        public override void Receive(BaseEventArgs baseEventArgs)
        {
            switch (baseEventArgs)
            {
                case ConstructionCreatedEventArgs _:
                    inputManager.gameObject.SetActive(true);
                    break;
                case LevelFinishedEventArgs levelFinishedEventArgs:
                    Broadcast(levelFinishedEventArgs);
                    break;
                case NextLevelButtonClickedEventArgs nextLevelButtonClickedEventSArgs:
                    BroadcastDownward(nextLevelButtonClickedEventSArgs);
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
            
            drawingManager.InjectMediator(mediator);
            drawingManager.InjectManager(this);
            
            ballManager.InjectMediator(mediator);
            ballManager.InjectManager(this);
            
            explodeEffectManager.InjectMediator(mediator);
            explodeEffectManager.InjectManager(this);
            
            collectorManager.InjectMediator(mediator);
            collectorManager.InjectManager(this);
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