using System.ComponentModel;
using _GameFiles.Scripts.Models;
using _GameFiles.Scripts.Presenters;
using EventDrivenFramework;
using EventDrivenFramework.Core;
using EventDrivenFramework.UI;
using UnityEngine;

namespace _GameFiles.Scripts.Managers._HighLevelManagers
{
    public class UIManager : BaseUIManager
    {
        [SerializeField] private GameViewPresenter gameViewPresenter;
    
        private GameModel gameModel;
        protected override void Awake()
        {
            base.Awake();
            gameViewPresenter.InjectManager(this);
        }

        protected override void Start()
        {
            base.Start();
        
        }

        public override void Receive(BaseEventArgs baseEventArgs)
        {
            switch (baseEventArgs)
            {
            
            }
        }
        public void InjectModel(GameModel gameModel)
        {
            this.gameModel = gameModel;
            this.gameModel.PropertyChanged += GameMOdelProperetyChangedHandler;
        }

        private void GameMOdelProperetyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(gameModel.InstantScore))
            {
            
            }
        }
    }
}