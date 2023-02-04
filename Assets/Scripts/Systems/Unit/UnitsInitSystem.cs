using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using UnityEngine.AI;

namespace Client {
    sealed class UnitsInitSystem : IEcsInitSystem {
        readonly EcsPoolInject<Unit> _unitPool = default;
        readonly EcsPoolInject<View> _viewPool = default;
        readonly EcsPoolInject<IsMoving> _isMovingPool = default;

        readonly EcsSharedInject<GameState> _state = default;
        readonly EcsWorldInject _world = default;

        public void Init (EcsSystems systems) {
            var destination = GameObject.FindObjectOfType<UnitDestinationMB>().transform.position;

            for (int i = 0; i <= _state.Value.GameConfig.UnitSpawnCount; i++) {
                var unitEntity = _world.Value.NewEntity();

                var unitMB = _state.Value.ActivePools.UnitPool.GetFromPool().GetComponent<UnitMB>();
                unitMB.Init(_world.Value, _state.Value, unitEntity);

                ref var unitComp = ref _unitPool.Value.Add(unitEntity);
                unitComp.NavMeshAgent = unitMB.GetComponent<NavMeshAgent>();
                unitComp.Destination = destination;

                ref var viewComp = ref _viewPool.Value.Add(unitEntity);
                viewComp.Transform = unitMB.transform;
                
                unitComp.NavMeshAgent.Warp(new Vector3(0, 0.12f, 0));
                unitComp.NavMeshAgent.SetDestination(new Vector3(0, 0.12f, 0.1f));

                _isMovingPool.Value.Add(unitEntity);               
            }
        }
    }
}