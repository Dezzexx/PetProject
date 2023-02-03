using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client {
    sealed class LoseSystem : IEcsRunSystem 
    {
        readonly EcsWorldInject _world = default;
        readonly EcsSharedInject<GameState> _state = default;
        readonly EcsFilterInject<Inc<LoseEvent>> _loseFilter = default;
        readonly EcsPoolInject<InterfaceComponent> _interfacePool = default;

        int entityEvent = GameState.NULL_ENTITY;
        public void Run (EcsSystems systems) 
        {
            foreach (var evnt in _loseFilter.Value)
            {
                entityEvent = evnt;
                ref var interfaceComponent = ref _interfacePool.Value.Get(_state.Value.InterfaceEntity);
                interfaceComponent.CanvasBehaviour.OpenLosePanels();
                
                DeleteEvent();
            }
        }
        void DeleteEvent()
        {
            _loseFilter.Pools.Inc1.Del(entityEvent);
        }
    }
}