using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class UnitMoveSystem : IEcsRunSystem {  
        readonly EcsFilterInject<Inc<Unit, IsMoving, View>, Exc<Dead>> _unitMoveFilter = default;
        readonly EcsPoolInject<Unit> _unitPool = default;

        public void Run (EcsSystems systems) {
            foreach (var unitMoveEntity in _unitMoveFilter.Value) {
                ref var unitComp = ref _unitPool.Value.Get(unitMoveEntity);
                
                unitComp.NavMeshAgent.SetDestination(unitComp.Destination);
            }
        }
    }
}