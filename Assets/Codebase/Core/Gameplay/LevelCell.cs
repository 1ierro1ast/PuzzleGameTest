using Codebase.Infrastructure.GameFlow;
using Codebase.Infrastructure.Services;

namespace Codebase.Core.Gameplay
{
    public class LevelCell : BaseCell
    {
        private int _cellId;
        private IEventBus _eventBus;

        private void Awake()
        {
            _eventBus = AllServices.Container.Single<IEventBus>();
        }

        protected override void OnCellFilled()
        {
            _eventBus.BroadcastCellFilled(_cellId);
        }

        public void SetId(int id) => _cellId = id;

    }
}