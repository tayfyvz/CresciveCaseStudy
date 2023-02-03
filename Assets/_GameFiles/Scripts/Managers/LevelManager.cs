using System.ComponentModel;
using _GameFiles.Scripts.EventArgs;
using _GameFiles.Scripts.Models;
using EventDrivenFramework;
using EventDrivenFramework.Core;

namespace _GameFiles.Scripts.Managers
{
    public class LevelManager : BaseManager
    {
        private GameModel _gameModel;
        public override void Receive(BaseEventArgs baseEventArgs)
        {
            switch (baseEventArgs)
            {
                
            }
        }

        protected override void Start()
        {
            Broadcast(new SceneStartedEventArgs(_gameModel.Level));
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