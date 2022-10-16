using System;
using Codebase.Infrastructure.Services;

namespace Codebase.Infrastructure.GameFlow
{
    public interface IEventBus : IService
    {
        event Action GamePlayStartEvent;
        event Action LevelFinishedEvent;
        event Action PlayerWinEvent;
        event Action PlayerLoseEvent;
        event Action<int> CellFilledEvent;
        event Action LaunchWinFxsEvent;

        void BroadcastGamePlayStart();
        void BroadcastLevelFinished();
        void BroadcastPlayerWin();
        void BroadcastPlayerLose();
        void BroadcastCellFilled(int cellId);
        void BroadcastLaunchWinFxs();
    }
}