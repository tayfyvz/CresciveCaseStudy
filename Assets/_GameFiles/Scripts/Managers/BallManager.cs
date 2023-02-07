using System.Collections.Generic;
using _GameFiles.Scripts.Controllers;
using _GameFiles.Scripts.EventArgs;
using _GameFiles.Scripts.ScriptableObjects;
using EventDrivenFramework.Core;
using UnityEngine;

namespace _GameFiles.Scripts.Managers
{
    //Responsible for balls.
    public class BallManager : BaseManager
    {
        [SerializeField] private BallData ballData;
        private readonly Queue<Controller> _ballsQueue = new Queue<Controller>();
        private BallController _activeBall;
        public override void Receive(BaseEventArgs baseEventArgs)
        {
            switch (baseEventArgs)
            {
                case DrawingIsCreatedEventArgs drawIsCreatedEventArgs:
                    ReleaseBall(drawIsCreatedEventArgs.StartPos, drawIsCreatedEventArgs.Angle);
                    break;
            }
        }
        
        //Creates balls by using Object Pooling.
        protected override void Awake()
        {
            base.Awake();
            Broadcast(new CreateObjectsEventArgs(
                ballData.MaxBallNum,
                ballData.BallControllerPrefab,
                transform,
                _ballsQueue
            ));
        }

        //Activates ball and starts listen its event.
        private void ReleaseBall(Vector3 firstPointPos, float angle)
        {
            if (_activeBall != null)
            {
                OnBallExplodeEventHandler(_activeBall);
            }
            BallController ball = _ballsQueue.Dequeue().GetComponent<BallController>();
            _activeBall = ball;
            ball.gameObject.SetActive(true);
            ball.OnBallExplodeEvent += OnBallExplodeEventHandler;
            Vector3 pos = new Vector3(firstPointPos.x + 1 , ballData.ReleaseHeight, 24.4f);
            ball.SetBall(ballData.CheckRadius,angle, ballData.ExplodeAmount, pos, ballData.CubeLayer, ballData.DrawingLayer);
        }

        //When the ball is explode, stops listen its event and send package to the explode effect manager.
        private void OnBallExplodeEventHandler(BallController ball)
        {
            ball.gameObject.SetActive(false);
            _ballsQueue.Enqueue(ball);
            ball.OnBallExplodeEvent -= OnBallExplodeEventHandler;
            _activeBall = null;
            Broadcast(new BallIsExplodedEventArgs(ball.transform.position));
        }
    }
}