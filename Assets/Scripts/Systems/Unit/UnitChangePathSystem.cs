using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class UnitChangePathSystem : IEcsRunSystem {   
        readonly EcsFilterInject<Inc<UnitChangePathEvent, Unit>, Exc<Dead>> _unitFilter = default;
        readonly EcsPoolInject<Unit> _unitPool = default;
        readonly EcsPoolInject<UnitChangePathEvent> _changePathEvent = default;

        public void Run (EcsSystems systems) {
            foreach (var unitEntity in _unitFilter.Value) {
                ref var changePathEvent = ref _changePathEvent.Value.Get(unitEntity);
                ref var unitComp = ref _unitPool.Value.Get(unitEntity);

                unitComp.NavMeshAgent.SetDestination(changePathEvent.NewDestination);

                _changePathEvent.Value.Del(unitEntity);
            }
        }
    }
}