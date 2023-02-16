using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class DamageSystem : IEcsRunSystem {   
        readonly EcsFilterInject<Inc<DamageEvent>> _filterDamage = default;
        readonly EcsPoolInject<DamageEvent> _damageEventPool = default;


#region General
        readonly EcsPoolInject<InterfaceComponent> _interfacePool = default;
        readonly EcsPoolInject<UnitsHolder> _unitsHolderPool = default;
        readonly EcsPoolInject<Unit> _unitPool = default;
        readonly EcsPoolInject<Health> _healthPool = default;
        readonly EcsPoolInject<View> _viewPool = default;
        readonly EcsPoolInject<Dead> _deadPool = default;
        readonly EcsPoolInject<CameraComponent> _cameraPool = default;
        readonly EcsPoolInject<CreateEffectEvent> _createEffectEventPool = default;

        readonly EcsSharedInject<GameState> _state = default;
        readonly EcsWorldInject _world = default;
#endregion
        
        private int _entityEvent = GameState.NULL_ENTITY;
        private int _entityTarget = GameState.NULL_ENTITY;  

        private RaycastHit _hit;

        public void Run (EcsSystems systems) {
            foreach (var entity in _filterDamage.Value)
            {
                _entityEvent = entity;

                ref var damageEventComp = ref _damageEventPool.Value.Get(entity);
                _entityTarget = damageEventComp.EntityTarget;

                if (_deadPool.Value.Has(_entityTarget)) {
                    DeleteEvent();
                    continue;
                }

                if (_unitPool.Value.Has(_entityTarget)) {
                    DamageToUnit(damageEventComp.Damage);
                }

                DeleteEvent();
            }
        }


        private void DamageToUnit(float damageAmount) {
            if (!_healthPool.Value.Has(_entityTarget)) return;
            ref var healthComp = ref _healthPool.Value.Get(_entityTarget);
            healthComp.CurrentAmount -= damageAmount;

            if (healthComp.CurrentAmount <= 0) {
                DestroyUnit();
            }
            
        }

        private void DestroyUnit() {
            ref var unitComp = ref _unitPool.Value.Get(_entityTarget);
            ref var unitsHolderComp = ref _unitsHolderPool.Value.Get(_state.Value.UnitsHolderEntity);
            ref var viewComp = ref _viewPool.Value.Get(_entityTarget);

            var unitEntity = GameState.NULL_ENTITY;
            UnitMB unitMB = null;
            Transform spot = null;

            switch (unitComp.UnitType)
            {
                case UnitTypes.FriendlyUnit:
                    unitEntity = unitComp.UnitMB._entity;
                    unitMB = unitsHolderComp.FriendlyUnitsHolder.Find(x => x._entity == unitEntity);
                    unitsHolderComp.FriendlyUnitsHolder.Remove(unitMB);
                    _state.Value.ActivePools.FriendlyUnitPool.ReturnToPool(viewComp.Transform.gameObject);
                    _cameraPool.Value.Get(_state.Value.CameraEntity).TargetGroup.RemoveMember(viewComp.Transform);
                    spot = _state.Value.ActivePools.BlueSpotPool.GetFromPool().transform;
                    viewComp.Transform.gameObject.SetActive(false);
                    break;
                
                case UnitTypes.EnemyUnit:
                    unitEntity = unitComp.UnitMB._entity;
                    unitMB = unitsHolderComp.EnemyUnitsHolder.Find(x => x._entity == unitEntity);
                    unitsHolderComp.EnemyUnitsHolder.Remove(unitMB);
                    spot = _state.Value.ActivePools.RedSpotPool.GetFromPool().transform;
                    GameObject.Destroy(viewComp.Transform.gameObject);
                    break;

                default:
                    break;
            }
            if (Physics.Raycast(viewComp.Transform.position, Vector3.forward, out _hit, 2f, _state.Value.SurfaceMask)) {
                var randomDivider = Random.Range(3.5f, 5f);
                var randomOffset = new Vector3(0, 0, -Random.Range(0.01f, 0.05f));

                spot.transform.position = _hit.point + randomOffset;
                spot.transform.rotation = Quaternion.Euler(270, 0, 0);
                spot.transform.localScale = spot.transform.localScale / randomDivider;
            }

            _deadPool.Value.Add(_entityTarget);
            _viewPool.Value.Del(_entityTarget);
        }

        private void DeleteEvent() {
            _entityTarget = GameState.NULL_ENTITY;

            _damageEventPool.Value.Del(_entityEvent);
        }
    }
}