using Codebase.Infrastructure.GameFlow;
using Codebase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Core.UI.Popups
{
    public class OverlayPopup : Popup
    {
        [SerializeField] private Button _audioButton;
        private IEventBus _eventBus;

        private AudioPopup _audioPopup;

        public void Construct(AudioPopup audioPopup)
        {
            _audioPopup = audioPopup;
            _audioButton.onClick.AddListener(audioPopup.OpenPopup);
        }

        protected override void OnInitialization()
        {
            base.OnInitialization();

            _eventBus = AllServices.Container.Single<IEventBus>();
            _eventBus.LevelFinishedEvent += EventBusOnLevelFinishedEvent;
        }

        private void EventBusOnLevelFinishedEvent()
        {
            _eventBus.LevelFinishedEvent -= EventBusOnLevelFinishedEvent;
            ClosePopup();
        }

        private void OnDestroy()
        {
            _eventBus.LevelFinishedEvent -= EventBusOnLevelFinishedEvent;
            _audioButton.onClick.RemoveListener(_audioPopup.OpenPopup);
        }
    }
}
