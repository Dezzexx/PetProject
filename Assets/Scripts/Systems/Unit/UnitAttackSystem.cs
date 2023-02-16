using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class UnitAttackSystem : IEcsRunSystem {  
        readonly EcsFilterInject<Inc<Unit, View, ReadyToAttack>, Exc<Dead>> _unitFilter = default;   
        readonly EcsPoolInject<Unit> _unitPool = default;
        readonly EcsPoolInject<View> _viewPool = default;
        readonly EcsPoolInject<UnitsHolder> _unitsHolderPool = default;   
        readonly EcsPoolInject<Dead> _deadPool = default;
        readonly EcsPoolInject<ReadyToAttack> _readyToAttackPool = default;
        readonly EcsPoolInject<UnitChangePathEvent> _changePathEvent = default;
        readonly EcsPoolInject<NavMeshSurfaceComp> _navMeshSurfacePool = default;
        readonly EcsPoolInject<AnimationSwitchEvent> _animationSwitchEvent = default;
        readonly EcsPoolInject<LoseCheck> _loseCheckPool = default;

        readonly EcsSharedInject<GameState> _state = default;
        readonly EcsWorldInject _world = default;

        private Vector3 _randomPosition;
        private int _unitEntity = GameState.NULL_ENTITY;

        public void Run (EcsSystems systems) {
            foreach (var unitEntity in _unitFilter.Value) {
                _unitEntity = unitEntity;

                ref var unitComp = ref _unitPool.Value.Get(unitEntity);
                ref var unitsHolderComp = ref _unitsHolderPool.Value.Get(_state.Value.UnitsHolderEntity);

                switch (unitComp.UnitType)
                {
                    case UnitTypes.FriendlyUnit:
                        RandomHostileUnitPos(unitsHolderComp.EnemyUnitsHolder, unitComp);
                        break;

                    case UnitTypes.EnemyUnit:
                        RandomHostileUnitPos(unitsHolderComp.FriendlyUnitsHolder, unitComp);
                        break;
                    
                    default:
                        break;
                }
            }
        }


        // сделать однокадровый поиск таргета
        private void RandomHostileUnitPos(List<UnitMB> units, Unit unitComp) {
            if (units.Count is not 0) {
                var randomEntity = Random.Range(units[0]._entity, units[units.Count - 1]._entity);
                if (!_deadPool.Value.Has(randomEntity) && _readyToAttackPool.Value.Has(randomEntity)) {
                    if (unitComp.UnitType is UnitTypes.EnemyUnit) {
                        _animationSwitchEvent.Value.Add(_unitEntity).AnimationSwitcher = AnimationSwitchEvent.AnimationType.Run;
                    }
                    _randomPosition = _viewPool.Value.Get(randomEntity).Transform.position;
                    unitComp.NavMeshAgent.SetDestination(_randomPosition);
                }
            }
            else if (unitComp.UnitType is UnitTypes.FriendlyUnit) {
                ref var navMeshSurfaceComp = ref _navMeshSurfacePool.Value.Get(_state.Value.NavMeshSurfaceEntity);
                if (!_changePathEvent.Value.Has(_unitEntity)) _changePathEvent.Value.Add(_unitEntity).NewDestination = 
                    navMeshSurfaceComp.DestinationPoints[(int)unitComp.UnitDestinationPoint + 1].transform.position;

                if (!navMeshSurfaceComp.DestinationPoints[(int)unitComp.UnitDestinationPoint + 1].GetComponent<UnitDestinationPointMB>().IsAttackPoint) {
                    _readyToAttackPool.Value.Del(_unitEntity);
                }
            }
            else {
                _loseCheckPool.Value.Add(_world.Value.NewEntity());
            }
        }
    }
}