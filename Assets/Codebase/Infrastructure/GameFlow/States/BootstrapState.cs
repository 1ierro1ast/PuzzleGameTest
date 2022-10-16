using Codebase.Infrastructure.Services;
using Codebase.Infrastructure.Services.AssetManagement;
using Codebase.Infrastructure.Services.Audio;
using Codebase.Infrastructure.Services.Factories;
using Codebase.Infrastructure.Services.Game;
using Codebase.Infrastructure.Services.SaveLoad;
using Codebase.Infrastructure.StateMachine;

namespace Codebase.Infrastructure.GameFlow.States
{
    public class BootstrapState : IState
    {
        private const string NextSceneName = "MainScene";

        private readonly GameStateMachine _stateMachine;
        private readonly AllServices _services;
        private readonly ICoroutineRunner _coroutineRunner;

        public BootstrapState(GameStateMachine stateMachine, AllServices allServices, ICoroutineRunner coroutineRunner)
        {
            _stateMachine = stateMachine;
            _services = allServices;
            _coroutineRunner = coroutineRunner;

            RegisterServices();
        }

        public void Enter()
        {
            EnterLoadLevel();
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadGameState, string>(NextSceneName);
        }

        private void RegisterServices()
        {
            RegisterAssetProvider();
            RegisterSaveLoadService();

            RegisterEventBus();
            RegisterUiFactory();
            RegisterAudioService();
            RegisterLevelFactory();
            RegisterFinishHandler();
        }

        private void RegisterFinishHandler()
        {
            _services.RegisterSingle<IFinishHandler>(new FinishHandler(_services.Single<IEventBus>(),
                _coroutineRunner));
        }

        private void RegisterAudioService()
        {
            _services.RegisterSingle<IAudioService>(new AudioService(_services.Single<IAssetProvider>(),
                _services.Single<ISaveLoadService>(), _services.Single<IUiFactory>()));
        }

        private void RegisterAssetProvider()
        {
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
        }

        private void RegisterSaveLoadService()
        {
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService());
        }

        private void RegisterLevelFactory()
        {
            _services.RegisterSingle<ILevelFactory>(
                new LevelFactory(_services.Single<IAssetProvider>()));
        }

        private void RegisterEventBus()
        {
            _services.RegisterSingle<IEventBus>(
                new EventBus());
        }

        private void RegisterUiFactory()
        {
            _services.RegisterSingle<IUiFactory>(
                new UiFactory(_services.Single<IAssetProvider>()));
        }
    }
}