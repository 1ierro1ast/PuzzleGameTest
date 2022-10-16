using Codebase.Infrastructure.GameFlow;
using Codebase.Infrastructure.GameFlow.States;
using Codebase.Infrastructure.Services;
using UnityEngine;

namespace Codebase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private GameStateMachine _stateMachine;

        private void Awake()
        {
            _stateMachine = new GameStateMachine(new SceneLoader(this), AllServices.Container, this);
            
            _stateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}