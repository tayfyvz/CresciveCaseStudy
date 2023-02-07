using _GameFiles.Scripts.EventArgs;
using EventDrivenFramework.Core;
using UnityEngine;

namespace _GameFiles.Scripts.Managers
{
    public class InputManager : BaseManager
    {
        public override void Receive(BaseEventArgs baseEventArgs)
        {
            
        }

        //Checks there is an input or not and sends the packages.
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Broadcast(new StartDrawEventArgs());
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Broadcast(new EndDrawEventArgs());
            }
        }
    }
}