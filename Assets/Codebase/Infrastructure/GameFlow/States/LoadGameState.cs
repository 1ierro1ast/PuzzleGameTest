using Codebase.Infrastructure.StateMachine;

namespace Codebase.Infrastructure.GameFlow.States
{
    public class LoadGameState : IPayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadGameState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string sceneName)
        {
            _sceneLoader.LoadScene(sceneName, false, OnLoaded);
        }

        public void Exit()
        {
        }

        private void OnLoaded()
        {
            _gameStateMachine.Enter<GameReadyState>();
        }
    }
}