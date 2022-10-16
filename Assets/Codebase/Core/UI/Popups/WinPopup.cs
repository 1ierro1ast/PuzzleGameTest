using System;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Core.UI.Popups
{
    public class WinPopup : Popup
    {
        [SerializeField] private Button _nextLevelButton;

        public event Action NextLevelEvent;

        protected override void OnInitialization()
        {
            base.OnInitialization();

            _nextLevelButton.onClick.AddListener(OnNextLevelButtonClick);
        }

        protected override void OnOpenPopup()
        {
            base.OnOpenPopup();
            _nextLevelButton.interactable = true;

        }

        private void OnNextLevelButtonClick()
        {
            _nextLevelButton.interactable = false;
            NextLevelEvent?.Invoke();
        }
    }
}