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

        private int _unitMoveEntity = GameState.NULL_ENTITY;

        public void Run (EcsSystems systems) {
            foreach (var unitMoveEntity in _unitMoveFilter.Value) {
                _unitMoveEntity = unitMoveEntity;
                ref var unitComp = ref _unitPool.Value.Get(unitMoveEntity);
                ref var viewComp = ref _viewPool.Value.Get(unitMoveEntity);

                ChangePath(viewComp.Transform.position.y, (int)unitComp.UnitDestinationPoint);
            }
        }

        private void ChangePath(float unitCurrentPos, int pointNumber) {
            ref var navMeshSurfaceComp = ref _navMeshSurfaceComp.Value.Get(_state.Value.NavMeshSurfaceEntity);

            if (pointNumber < navMeshSurfaceComp.DestinationPoints.Count - 1) {
                ref var unitComp = ref _unitPool.Value.Get(_unitMoveEntity);
                var distance = navMeshSurfaceComp.DestinationPoints[pointNumber].transform.position.y - unitCurrentPos;

                if (distance < 0.1f) {
                    ref var changePathEvent = ref _changePathEvent.Value.Add(_unitMoveEntity);
                    changePathEvent.NewDestination = navMeshSurfaceComp.DestinationPoints[pointNumber + 1].transform.position;
                    if (navMeshSurfaceComp.DestinationPoints[pointNumber + 1].GetComponent<UnitDestinationPointMB>().IsAttackPoint) {
                        changePathEvent.IsDirectionToAttack = true;
                    }
                    else {
                        changePathEvent.IsDirectionToAttack = false;
                    }
                    unitComp.UnitDestinationPoint++;
                }
            }
        }
    }
}