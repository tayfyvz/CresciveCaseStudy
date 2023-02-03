using EventDrivenFramework.Core;

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
}