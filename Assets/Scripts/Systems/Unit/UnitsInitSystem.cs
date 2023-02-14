using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;

namespace Client {
    sealed class UnitsInitSystem : IEcsInitSystem {
        readonly EcsPoolInject<Unit> _unitPool = default;
        readonly EcsPoolInject<View> _viewPool = default;
        readonly EcsPoolInject<IsMoving> _isMovingPool = default;
        readonly EcsPoolInject<NavMeshSurfaceComp> _navMeshSurfaceComp = default;

        readonly EcsSharedInject<GameState> _state = default;
        readonly EcsWorldInject _world = default;

        private float _unitParameters = 1;

        public void Init (EcsSystems systems) {
            var unitSpawnPointTransform = GameObject.FindObjectOfType<UnitSpawnPointMB>().transform;
            var targetGroup = GameObject.FindObjectOfType<CinemachineTargetGroup>();
            
            ref var navMeshSurfaceComp = ref _navMeshSurfaceComp.Value.Get(_state.Value.NavMeshSurfaceEntity);
            navMeshSurfaceComp.AllFriendlyUnits = new List<UnitMB>();

            for (int i = 0; i <= _state.Value.GameConfig.UnitSpawnCount; i++) {
                var unitEntity = _world.Value.NewEntity();

                var unitMB = _state.Value.ActivePools.UnitPool.GetFromPool().GetComponent<UnitMB>();
                unitMB.Init(_world.Value, _state.Value, unitEntity);

                ref var unitComp = ref _unitPool.Value.Add(unitEntity);
                unitComp.NavMeshAgent = unitMB.GetComponent<NavMeshAgent>();
                
                ref var viewComp = ref _viewPool.Value.Add(unitEntity);
                viewComp.Transform = unitMB.transform;
                viewComp.Transform.rotation = unitSpawnPointTransform.rotation;
                
                unitComp.NavMeshAgent.Warp(unitSpawnPointTransform.position);
                unitComp.NavMeshAgent.SetDestination(navMeshSurfaceComp.DestinationPoints[0].transform.position);
                unitComp.UnitDestinationPoint = Points.First;

                navMeshSurfaceComp.AllFriendlyUnits.Add(unitMB);
                targetGroup.AddMember(viewComp.Transform, _unitParameters, _unitParameters);

                _isMovingPool.Value.Add(unitEntity);               
            }
        }
    }
}