using System.Collections;
using Codebase.Core.UI.Popups;
using Codebase.Infrastructure.Services.Factories;
using Codebase.Infrastructure.StateMachine;
using UnityEngine;

namespace Codebase.Infrastructure.GameFlow.States
{
    public class GameplayState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IEventBus _eventBus;
        private readonly IUiFactory _uiFactory;
        private readonly ICoroutineRunner _coroutineRunner;

        private OverlayPopup _overlayPopup;


        public GameplayState(GameStateMachine gameStateMachine, IEventBus eventBus,
            IUiFactory uiFactory, ICoroutineRunner coroutineRunner)
        {
            _gameStateMachine = gameStateMachine;
            _eventBus = eventBus;
            _uiFactory = uiFactory;
            _coroutineRunner = coroutineRunner;
        }

        public void Exit()
        {
            _eventBus.PlayerWinEvent -= EventBus_OnPlayerWinEvent;
            _eventBus.PlayerLoseEvent -= EventBus_OnPlayerLoseEvent;
        }

        public void Enter()
        {
            _overlayPopup = _uiFactory.GetOverlayPopup();
            _overlayPopup.OpenPopup();

            _eventBus.BroadcastGamePlayStart();

            _eventBus.PlayerWinEvent += EventBus_OnPlayerWinEvent;
            _eventBus.PlayerLoseEvent += EventBus_OnPlayerLoseEvent;
        }

        private void EventBus_OnPlayerLoseEvent()
        {
            _eventBus.BroadcastLevelFinished();
            _coroutineRunner.StartCoroutine(LoseCoroutine());
        }

        private void EventBus_OnPlayerWinEvent()
        {
            _eventBus.BroadcastLevelFinished();
            _coroutineRunner.StartCoroutine(WinCoroutine());
        }

        private void ToWinState()
        {
            _gameStateMachine.Enter<WinState>();
        }

        private void ToLoseState()
        {
            _gameStateMachine.Enter<LoseState>();
        }

        private IEnumerator WinCoroutine()
        {
            yield return new WaitForSeconds(0.1f);
            ToWinState();
        }

        private IEnumerator LoseCoroutine()
        {
            yield return new WaitForSeconds(0.1f);
            ToLoseState();
        }
    }
}