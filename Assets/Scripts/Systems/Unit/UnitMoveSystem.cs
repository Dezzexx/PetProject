using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class UnitMoveSystem : IEcsRunSystem {  
        readonly EcsFilterInject<Inc<Unit, IsMoving, View>, Exc<Dead>> _unitMoveFilter = default;
        readonly EcsPoolInject<UnitChangePathEvent> _changePathEvent = default;
        readonly EcsPoolInject<Unit> _unitPool = default;
        readonly EcsPoolInject<View> _viewPool = default;
        readonly EcsPoolInject<NavMeshSurfaceComp> _navMeshSurfaceComp = default;

        readonly EcsSharedInject<GameState> _state = default;

        public void Run (EcsSystems systems) {
            foreach (var unitMoveEntity in _unitMoveFilter.Value) {
                ref var unitComp = ref _unitPool.Value.Get(unitMoveEntity);
                ref var viewComp = ref _viewPool.Value.Get(unitMoveEntity);
                ref var navMeshSurfaceComp = ref _navMeshSurfaceComp.Value.Get(_state.Value.NavMeshSurfaceEntity);
                
                var distance = navMeshSurfaceComp.DestinationPoints[0].transform.position.y - viewComp.Transform.position.y ;

                if (distance < 0.1f && unitComp.UnitDestinationPoint is Points.First) {
                    _changePathEvent.Value.Add(unitMoveEntity).NewDestination = navMeshSurfaceComp.DestinationPoints[1].transform.position;
                    unitComp.UnitDestinationPoint = Points.Two;
                }
            }
        }
    }
}