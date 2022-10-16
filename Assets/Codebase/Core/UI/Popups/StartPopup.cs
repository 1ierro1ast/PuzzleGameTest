using System;
using Codebase.Infrastructure.GameFlow;
using Codebase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Core.UI.Popups
{
    public class StartPopup : Popup
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _audioButton;
        
        private IEventBus _eventBus;
        private AudioPopup _audioPopup;

        public event Action StartButtonClickEvent;

        public void Construct(AudioPopup audioPopup)
        {
            _audioPopup = audioPopup;
            _audioButton.onClick.AddListener(OnAudioButtonClick);
        }

        protected override void OnInitialization()
        {
            base.OnInitialization();
            OpenPopup();
            
            _startButton.onClick.AddListener(OnStartButtonClick);
            
            _eventBus = AllServices.Container.Single<IEventBus>();
            _eventBus.GamePlayStartEvent += EventBus_OnGamePlayStart;
        }

        protected override void OnOpenPopup()
        {
            base.OnOpenPopup();
            _startButton.interactable = true;
            _audioButton.interactable = true;
        }

        private void OnStartButtonClick()
        {
            _startButton.interactable = false;
            _audioButton.interactable = false;
            StartButtonClickEvent?.Invoke();
        }

        private void OnAudioButtonClick()
        {
            _audioPopup.OpenPopup();
        }

        private void EventBus_OnGamePlayStart()
        {
            ClosePopup();
        }

        private void OnDestroy()
        {
            _eventBus.GamePlayStartEvent -= EventBus_OnGamePlayStart;
            _audioButton.onClick.RemoveListener(OnAudioButtonClick);
        }
    }
}
