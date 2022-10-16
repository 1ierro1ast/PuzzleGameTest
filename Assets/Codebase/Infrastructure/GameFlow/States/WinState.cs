using Codebase.Core.UI.Popups;
using Codebase.Infrastructure.Services.Factories;
using Codebase.Infrastructure.StateMachine;

namespace Codebase.Infrastructure.GameFlow.States
{
    public class WinState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IUiFactory _uiFactory;
        private WinPopup _popup;

        public WinState(GameStateMachine gameStateMachine, IUiFactory uiFactory)
        {
            _gameStateMachine = gameStateMachine;
            _uiFactory = uiFactory;
        }

        public void Exit()
        {
            _popup.NextLevelEvent -= PopupOnNextLevelEvent;
            _popup.ClosePopup();
        }

        public void Enter()
        {
            _popup = _uiFactory.GetWinPopup();
            _popup.OpenPopup();
            _popup.NextLevelEvent += PopupOnNextLevelEvent;
        }
        
        private void PopupOnNextLevelEvent()
        {
            _gameStateMachine.Enter<LoadGameState, string>("MainScene");
        }
    }
}