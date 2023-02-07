using System.Collections.Generic;
using _GameFiles.Scripts.Controllers;
using _GameFiles.Scripts.EventArgs;
using _GameFiles.Scripts.ScriptableObjects;
using EventDrivenFramework.Core;
using UnityEngine;

namespace _GameFiles.Scripts.Managers
{
    public class CollectorManager : BaseManager
    {
        [SerializeField] private CoinData coinData;
        [SerializeField] private CollectorController collectorController;
        [SerializeField] private MoneyMakerController moneyMakerController;
        [SerializeField] private int cubeCounter;
        
        private readonly Queue<Controller> _coinsQueue = new Queue<Controller>();
        public override void Receive(BaseEventArgs baseEventArgs)
        {
        }
        
        //Creates coins by using Object Pooling.
        protected override void Awake()
        {
            base.Awake();
            
            Broadcast(new CreateObjectsEventArgs(
                coinData.MaxCoinNum,
                coinData.CoinPrefab,
                transform,
                _coinsQueue
            ));
            
            collectorController.OnCubeCollectedEvent += OnCubeCollectedEventHandler;
        }

        private void OnCubeCollectedEventHandler()
        {
            cubeCounter++;
            if (cubeCounter % 10 == 0)
            {
                Controller coin = _coinsQueue.Dequeue();
                moneyMakerController.ThrowMoney(coin);
            }
        }
    }
}