using EventDrivenFramework.Core;
using UnityEngine;

namespace _GameFiles.Scripts.EventArgs
{
    public class SceneStartedEventArgs : BaseEventArgs
    {
        public int LevelPref { get; set; }

        public SceneStartedEventArgs()
        {
            
        }

        public SceneStartedEventArgs(int levelPref)
        {
            LevelPref = levelPref;
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
}