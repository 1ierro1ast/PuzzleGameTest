using System.Collections;
using Codebase.Core.UI.Popups;
using Codebase.Infrastructure.Services.Factories;
using Codebase.Infrastructure.StateMachine;
using UnityEngine;

namespace Codebase.Infrastructure.GameFlow.States
{
    public class GameReadyState : IState
    {
        private readonly IUiFactory _uiFactory;
        private readonly GameStateMachine _gameStateMachine;
        private readonly ICoroutineRunner _coroutineRunner;
        private StartPopup _startPopup;

        public GameReadyState(GameStateMachine gameStateMachine, IUiFactory uiFactory,
            ICoroutineRunner coroutineRunner)
        {
            _uiFactory = uiFactory;
            _gameStateMachine = gameStateMachine;
            _coroutineRunner = coroutineRunner;
        }

        public void Exit()
        {
            _startPopup.StartButtonClickEvent -= StartPopup_OnStartButtonClickEvent;
        }

        public void Enter()
        {
            _startPopup = _uiFactory.GetStartPopup();
            _startPopup.OpenPopup();
            _startPopup.StartButtonClickEvent += StartPopup_OnStartButtonClickEvent;

            _coroutineRunner.StartCoroutine(CloseCurtainCoroutine());
        }

        private IEnumerator CloseCurtainCoroutine()
        {
            yield return new WaitForSeconds(0.5f);
        }

        private void StartPopup_OnStartButtonClickEvent()
        {
            _coroutineRunner.StartCoroutine(GoToGameCoroutine());
        }

        private IEnumerator GoToGameCoroutine()
        {
            _startPopup.ClosePopup();
            yield return new WaitForSeconds(1f);
            _gameStateMachine.Enter<LoadLevelState>();
        }
    }
}