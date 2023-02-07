using System.Collections.Generic;
using _GameFiles.Scripts.Controllers;
using _GameFiles.Scripts.EventArgs;
using _GameFiles.Scripts.ScriptableObjects;
using EventDrivenFramework.Core;
using UnityEngine;

namespace _GameFiles.Scripts.Managers
{
    public class ExplodeEffectManager : BaseManager
    {
        [SerializeField] private EffectData effectData;
        
        private readonly Queue<Controller> _explodeEffectsQueue = new Queue<Controller>();
        private readonly Queue<Controller> _cubeBreakEffectsEffectsQueue = new Queue<Controller>();


        public override void Receive(BaseEventArgs baseEventArgs)
        {
            switch (baseEventArgs)
            {
                case BallIsExplodedEventArgs ballIsExplodedEventArgs:
                    PlayEffect(ballIsExplodedEventArgs.Position, _explodeEffectsQueue);
                    break;
                case CubeIsExplodedEventArgs cubeIsExplodedEventArgs:
                    PlayEffect(cubeIsExplodedEventArgs.Position, _cubeBreakEffectsEffectsQueue);
                    break;
            }
        }

        //Creates effects by using Object Pooling.
        protected override void Awake()
        {
            base.Awake();
            Broadcast(new CreateObjectsEventArgs(
                effectData.MaxExplodeEffectNum,
                effectData.ExplodeEffectPrefab,
                transform,
                _explodeEffectsQueue
                ));
            Broadcast(new CreateObjectsEventArgs(
                effectData.MaxCubeBreakEffectNum,
                effectData.CubeBreakEffectPrefab,
                transform,
                _cubeBreakEffectsEffectsQueue
            ));
        }
        
        //Plays effects when ball or cube is exploded.
        private void PlayEffect(Vector3 pos, Queue<Controller> queue)
        {
            Controller effect = queue.Dequeue();
            effect.transform.position = new Vector3(pos.x, pos.y, 22);
            effect.gameObject.SetActive(true);
            
            effect.GetComponent<ParticleSystem>().Play();
            queue.Enqueue(effect);
        }
    }
}