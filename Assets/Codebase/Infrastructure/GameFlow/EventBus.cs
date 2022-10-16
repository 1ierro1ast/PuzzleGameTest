using System;

namespace Codebase.Infrastructure.GameFlow
{
    public class EventBus : IEventBus
    {
        public event Action GamePlayStartEvent;
        public event Action LevelFinishedEvent;
        
        public event Action PlayerWinEvent;
        public event Action PlayerLoseEvent;
        public event Action<int> CellFilledEvent;
        public event Action LaunchWinFxsEvent;

        public void BroadcastGamePlayStart()
        {
            GamePlayStartEvent?.Invoke();
        }

        public void BroadcastLevelFinished()
        {
            LevelFinishedEvent?.Invoke();
        }

        public void BroadcastPlayerWin()
        {
            PlayerWinEvent?.Invoke();
        }

        public void BroadcastPlayerLose()
        {
           PlayerLoseEvent?.Invoke();
        }

        public void BroadcastCellFilled(int cellId)
        {
            CellFilledEvent?.Invoke(cellId);
        }

        public void BroadcastLaunchWinFxs()
        {
            LaunchWinFxsEvent?.Invoke();
        }
    }
}