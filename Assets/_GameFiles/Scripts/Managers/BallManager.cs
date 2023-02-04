using System.Collections.Generic;
using _GameFiles.Scripts.Controllers;
using _GameFiles.Scripts.EventArgs;
using _GameFiles.Scripts.ScriptableObjects;
using EventDrivenFramework.Core;
using UnityEngine;

namespace _GameFiles.Scripts.Managers
{
    public class BallManager : BaseManager
    {
        [SerializeField] private BallData ballData;
        private Queue<BallController> _ballsQueue = new Queue<BallController>();
        [SerializeField] private float height;

        public override void Receive(BaseEventArgs baseEventArgs)
        {
            switch (baseEventArgs)
            {
                case DrawingIsCreatedEventArgs drawIsCreatedEventArgs:
                    ReleaseBall(drawIsCreatedEventArgs.StartPos);
                    break;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            int maxBall = ballData.MaxBall;
            BallController ball = ballData.BallControllerPrefab;
            
            for (int i = 0; i < maxBall; i++)
            {
                BallController bc = Instantiate(ball, transform);
                bc.gameObject.SetActive(false);
                _ballsQueue.Enqueue(bc);
            }
        }

        private void ReleaseBall(Vector3 firstPointPos)
        {
            BallController ball = _ballsQueue.Dequeue();
            ball.gameObject.SetActive(true);
            ball.transform.position = new Vector3(firstPointPos.x + 1 , height, 25);
        }
    }
}