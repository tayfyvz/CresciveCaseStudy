using System.Collections.Generic;
using _GameFiles.Scripts.Controllers;
using EventDrivenFramework.Core;
using UnityEngine;

//Contains event classes.
namespace _GameFiles.Scripts.EventArgs
{
    public class SceneStartedEventArgs : BaseEventArgs
    {
        public int LevelPref { get; set; }
        public SceneStartedEventArgs(int levelPref)
        {
            LevelPref = levelPref;
        }
    }

    public class CreateObjectsEventArgs : BaseEventArgs 
    {
        public int Max { get; set; }
        public Controller Prefab { get; set; }
        public Transform Transform { get; set; }
        public Queue<Controller> Queue { get; set; }

        public CreateObjectsEventArgs(int max, Controller prefab, Transform transform, Queue<Controller> queue)
        {
            Max = max;
            Prefab = prefab;
            Transform = transform;
            Queue = queue;
        }
    }

    public class ConstructionCreatedEventArgs : BaseEventArgs
    {
        
    }

    public class StartDrawEventArgs : BaseEventArgs
    {
        
    }
    public class EndDrawEventArgs : BaseEventArgs
    {
        
    }

    public class DrawingIsCreatedEventArgs : BaseEventArgs
    {
        public Vector3 StartPos { get; set; }
        public float Angle { get; set; }

        public DrawingIsCreatedEventArgs(Vector3 startPos, float angle)
        {
            StartPos = startPos;
            Angle = angle;
        }
    }

    public class BallIsExplodedEventArgs : BaseEventArgs
    {
        public Vector3 Position { get; set; }

        public BallIsExplodedEventArgs(Vector3 position)
        {
            Position = position;
        }
    }

    public class CubeIsExplodedEventArgs : BaseEventArgs
    {
        public Vector3 Position { get; set; }

        public CubeIsExplodedEventArgs(Vector3 position)
        {
            Position = position;
        }
    }

    public class LevelFinishedEventArgs : BaseEventArgs
    {
        
    }

    public class NextLevelButtonClickedEventArgs : BaseEventArgs
    {
        
    }
}