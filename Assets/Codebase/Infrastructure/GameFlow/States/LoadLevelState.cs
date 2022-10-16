using Codebase.Infrastructure.Services.Factories;
using Codebase.Infrastructure.StateMachine;

namespace Codebase.Infrastructure.GameFlow.States
{
    public class LoadLevelState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ILevelFactory _levelFactory;

        public LoadLevelState(GameStateMachine gameStateMachine, ILevelFactory levelFactory)
        {
            _gameStateMachine = gameStateMachine;
            _levelFactory = levelFactory;
        }

        public void Exit()
        {
            
        }

        public void Enter()
        {
            _levelFactory.ClearLevel();
            _levelFactory.CreateLevel(1);
            _gameStateMachine.Enter<GameplayState>();
        }
    }
}