using _GameFiles.Scripts.Presenters;
using EventDrivenFramework.UI;
using UnityEngine;
using UnityEngine.UI;

namespace _GameFiles.Scripts.Views
{
    // Game view and button management.
    public class GameView : BaseView
    {
        [SerializeField] private Button nextLevelButton;
        protected override void Initialize()
        {
            nextLevelButton.onClick.AddListener((_presenter as GameViewPresenter).NextLevelButtonClicked);
        }

        public void ActivateLevelEnd()
        {
            nextLevelButton.gameObject.SetActive(true);
        }
        public void DeactivateLevelEnd()
        {
            nextLevelButton.gameObject.SetActive(false);
        }
    }
}