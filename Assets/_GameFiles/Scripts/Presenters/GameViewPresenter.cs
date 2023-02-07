using _GameFiles.Scripts.EventArgs;
using _GameFiles.Scripts.Views;
using EventDrivenFramework.Core;
using EventDrivenFramework.UI;

namespace _GameFiles.Scripts.Presenters
{
    //Manages the game view.
    public class GameViewPresenter : BasePresenter
    {
        public override void Receive(BaseEventArgs baseEventArgs)
        {
            switch (baseEventArgs)
            {
                case LevelFinishedEventArgs levelFinishedEventArgs:
                    (view as GameView).ActivateLevelEnd();
                    break;
            }
        }

        public void NextLevelButtonClicked()
        {
            BroadcastUpward(new NextLevelButtonClickedEventArgs());
            (view as GameView).DeactivateLevelEnd();
        }
    }
}