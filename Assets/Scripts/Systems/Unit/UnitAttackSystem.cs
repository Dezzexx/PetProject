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

        readonly EcsSharedInject<GameState> _state = default;

        private Vector3 _randomPosition;

        public void Run (EcsSystems systems) {
            foreach (var unitEntity in _unitFilter.Value) {
                ref var unitComp = ref _unitPool.Value.Get(unitEntity);
                ref var unitsHolderComp = ref _unitsHolderPool.Value.Get(_state.Value.UnitsHolderEntity);

                switch (unitComp.UnitType)
                {
                    case UnitTypes.FriendlyUnit:
                        RandomHostileUnitPos(unitsHolderComp.EnemyUnitsHolder);
                        break;

                    case UnitTypes.EnemyUnit:
                        RandomHostileUnitPos(unitsHolderComp.FriendlyUnitsHolder);
                        break;
                    
                    default:
                        break;
                }

                unitComp.NavMeshAgent.SetDestination(_randomPosition);
            }
        }


        // сделать однокадровый поиск таргета
        private void RandomHostileUnitPos(List<UnitMB> units) {
            if (units.Count is not 0) {
                var randomEntity = Random.Range(units[0]._entity, units[units.Count - 1]._entity);
                if (!_deadPool.Value.Has(randomEntity)) _randomPosition = _viewPool.Value.Get(randomEntity).Transform.position;
            }
        }
    }
}