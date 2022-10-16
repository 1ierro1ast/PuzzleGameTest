using Codebase.Core.UI.Popups;
using Codebase.Infrastructure.Services.Factories;
using Codebase.Infrastructure.StateMachine;

namespace Codebase.Infrastructure.GameFlow.States
{
    public class LoseState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IUiFactory _uiFactory;
        private LosePopup _popup;

        public LoseState(GameStateMachine gameStateMachine, IUiFactory uiFactory)
        {
            _gameStateMachine = gameStateMachine;
            _uiFactory = uiFactory;
        }

        public void Exit()
        {
            _popup.RetryLevelEvent -= PopupOnRetryLevelEvent;
            _popup.ClosePopup();
        }

        public void Enter()
        {
            _popup = _uiFactory.GetLosePopup();
            _popup.RetryLevelEvent += PopupOnRetryLevelEvent;
            _popup.OpenPopup();
        }

        private void PopupOnRetryLevelEvent()
        {
            _gameStateMachine.Enter<LoadLevelState>();
        }
    }
}