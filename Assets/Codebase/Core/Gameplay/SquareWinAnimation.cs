using Codebase.Infrastructure.GameFlow;
using Codebase.Infrastructure.Services;
using UnityEngine;

namespace Codebase.Core.Gameplay
{
    [RequireComponent(typeof(Animator))]
    public class SquareWinAnimation : MonoBehaviour
    {
        private IEventBus _eventBus;
        private Animator _animator;
        
        private static readonly int Win = Animator.StringToHash("Win");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _eventBus = AllServices.Container.Single<IEventBus>();
            _eventBus.LaunchWinFxsEvent += EventBus_OnLaunchWinFxsEvent;
        }

        private void OnDestroy()
        {
            _eventBus.LaunchWinFxsEvent -= EventBus_OnLaunchWinFxsEvent;

        }

        private void EventBus_OnLaunchWinFxsEvent()
        {
            _animator.SetTrigger(Win);
        }
    }
}