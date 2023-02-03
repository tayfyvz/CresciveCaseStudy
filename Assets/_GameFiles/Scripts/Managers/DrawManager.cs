using System.Collections;
using _GameFiles.Scripts.EventArgs;
using _GameFiles.Scripts.Utilities;
using EventDrivenFramework.Core;
using UnityEngine;

namespace _GameFiles.Scripts.Managers
{
    public class DrawManager : BaseManager
    {
        
        public override void Receive(BaseEventArgs baseEventArgs)
        {
            switch (baseEventArgs)
            {
                case StartDrawEventArgs startDrawEventArgs:
                    DrawEnabled();
                    break;
                case EndDrawEventArgs endDrawEventArgs:
                    DrawDisabled();
                    break;
            }
        }

        private void DrawEnabled()
        {
            
        }

        private void DrawDisabled()
        {

        }
    }
}