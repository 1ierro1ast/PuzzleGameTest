using System.Collections;
using Codebase.Infrastructure.GameFlow;
using UnityEngine;

namespace Codebase.Infrastructure.Services.Game
{
    public class FinishHandler : IFinishHandler
    {
        private readonly IEventBus _eventBus;
        private readonly ICoroutineRunner _coroutineRunner;

        public FinishHandler(IEventBus eventBus, ICoroutineRunner coroutineRunner)
        {
            _eventBus = eventBus;
            _coroutineRunner = coroutineRunner;
            _eventBus.CellFilledEvent += EventBus_OnCellFilledEvent;
        }

        private void EventBus_OnCellFilledEvent(int cellId)
        {
            if (cellId == 4) _coroutineRunner.StartCoroutine(BroadcastWinCoroutine());
            else _eventBus.BroadcastPlayerLose();
        }

        private IEnumerator BroadcastWinCoroutine()
        {
            _eventBus.BroadcastLaunchWinFxs();
            yield return new WaitForSeconds(0.25f);
            _eventBus.BroadcastPlayerWin();
        }
    }
}