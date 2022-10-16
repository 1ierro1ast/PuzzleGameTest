using System;
using System.Collections.Generic;
using Codebase.Infrastructure.GameFlow.States;
using Codebase.Infrastructure.Services;
using Codebase.Infrastructure.Services.Factories;
using Codebase.Infrastructure.StateMachine;

namespace Codebase.Infrastructure.GameFlow
{
    public class GameStateMachine : BaseStateMachine
    {
        public GameStateMachine(SceneLoader sceneLoader, AllServices services,
            ICoroutineRunner coroutineRunner)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, services, coroutineRunner),

                [typeof(LoadGameState)] = new LoadGameState(this, sceneLoader),

                [typeof(GameReadyState)] = new GameReadyState(this, services.Single<IUiFactory>(), coroutineRunner),

                [typeof(LoadLevelState)] = new LoadLevelState(this, services.Single<ILevelFactory>()),

                [typeof(GameplayState)] = new GameplayState(this, services.Single<IEventBus>(),
                    services.Single<IUiFactory>(), coroutineRunner),

                [typeof(WinState)] = new WinState(this, services.Single<IUiFactory>()),

                [typeof(LoseState)] = new LoseState(this, services.Single<IUiFactory>())
            };
        }
    }
}