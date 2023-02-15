using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using UnityEngine.AI;

namespace Client {
    sealed class UnitsInitSystem : IEcsInitSystem {
        readonly EcsPoolInject<Unit> _unitPool = default;
        readonly EcsPoolInject<View> _viewPool = default;
        readonly EcsPoolInject<IsMoving> _isMovingPool = default;
        readonly EcsPoolInject<Health> _healthPool = default;
        readonly EcsPoolInject<NavMeshSurfaceComp> _navMeshSurfaceComp = default;
        readonly EcsPoolInject<UnitsHolder> _unitsHolderPool = default;

        readonly EcsSharedInject<GameState> _state = default;
        readonly EcsWorldInject _world = default;

        public void Init (EcsSystems systems) {
            var unitSpawnPointTransform = GameObject.FindObjectOfType<FriendlyUnitSpawnPointMB>().transform;
            var enemyUnitsHolderMB = GameObject.FindObjectOfType<EnemyUnitsMB>().EnemyUnitsHolderMB;

            var unitsHolderEntity = _world.Value.NewEntity();
            _state.Value.UnitsHolderEntity = unitsHolderEntity;
            ref var unitsHolderComp = ref _unitsHolderPool.Value.Add(unitsHolderEntity);
            unitsHolderComp.FriendlyUnitsHolder = new List<UnitMB>();
            unitsHolderComp.EnemyUnitsHolder = new List<UnitMB>();
            
            ref var navMeshSurfaceComp = ref _navMeshSurfaceComp.Value.Get(_state.Value.NavMeshSurfaceEntity);

            for (int i = 0; i <= _state.Value.GameConfig.UnitSpawnCount; i++) {
                var unitEntity = _world.Value.NewEntity();

                var unitMB = _state.Value.ActivePools.FriendlyUnitPool.GetFromPool().GetComponent<UnitMB>();
                unitMB.Init(_world.Value, _state.Value, unitEntity);

                ref var unitComp = ref _unitPool.Value.Add(unitEntity);
                unitComp.NavMeshAgent = unitMB.GetComponent<NavMeshAgent>();
                unitComp.UnitType = unitMB.UnitType;
                unitComp.UnitMB = unitMB;
                
                var randomSpawnSpread = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.3f, 0f), 0f);

                ref var viewComp = ref _viewPool.Value.Add(unitEntity);
                viewComp.Transform = unitMB.transform;
                viewComp.Transform.rotation = unitSpawnPointTransform.rotation;
                
                unitComp.NavMeshAgent.Warp(unitSpawnPointTransform.position + randomSpawnSpread);
                unitComp.NavMeshAgent.SetDestination(navMeshSurfaceComp.DestinationPoints[0].transform.position);
                unitComp.UnitDestinationPoint = Points.First;

                ref var healthComp = ref _healthPool.Value.Add(unitEntity);
                healthComp.MaxAmount = unitMB.UnitParameterConfig.Health;
                healthComp.CurrentAmount = healthComp.MaxAmount;

                unitsHolderComp.FriendlyUnitsHolder.Add(unitMB);

                _isMovingPool.Value.Add(unitEntity);               
            }

            for (int i = 0; i < enemyUnitsHolderMB.Length; i++) {
                var unitEntity = _world.Value.NewEntity();

                enemyUnitsHolderMB[i].Init(_world.Value, _state.Value, unitEntity);

                ref var unitComp = ref _unitPool.Value.Add(unitEntity);
                unitComp.UnitType = enemyUnitsHolderMB[i].UnitType;
                unitComp.NavMeshAgent = enemyUnitsHolderMB[i].GetComponent<NavMeshAgent>();
                unitComp.UnitMB = enemyUnitsHolderMB[i];

                ref var viewComp = ref _viewPool.Value.Add(unitEntity);
                viewComp.Transform = enemyUnitsHolderMB[i].transform;
                viewComp.Transform.rotation = enemyUnitsHolderMB[i].transform.rotation;

                ref var healthComp = ref _healthPool.Value.Add(unitEntity);
                healthComp.MaxAmount = enemyUnitsHolderMB[i].UnitParameterConfig.Health;
                healthComp.CurrentAmount = healthComp.MaxAmount;

                unitsHolderComp.EnemyUnitsHolder.Add(enemyUnitsHolderMB[i]);
            }
        }
    }
}