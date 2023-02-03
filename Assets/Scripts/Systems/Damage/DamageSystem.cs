using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using System.Collections.Generic;

namespace Client {
    sealed class DamageSystem : IEcsRunSystem {   
        readonly EcsFilterInject<Inc<DamageEvent>> _filterDamage = default;
        readonly EcsPoolInject<DamageEvent> _damageEventPool = default;


        #region Universal
        readonly EcsPoolInject<InterfaceComponent> _interfacePool = default;
        readonly EcsPoolInject<Health> _healthPool = default;
        readonly EcsPoolInject<View> _viewPool = default;
        readonly EcsPoolInject<Dead> _deadPool = default;
        readonly EcsPoolInject<CreateEffectEvent> _createEffectEventPool = default;

        readonly EcsSharedInject<GameState> _state = default;
        readonly EcsWorldInject _world = default;
#endregion
        
        private int _entityEvent = GameState.NULL_ENTITY;
        private int _entityTarget = GameState.NULL_ENTITY;  

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

                DeleteEvent();
            }
        }


        private void DamageToUnit(float damageAmount) {
            if (!_healthPool.Value.Has(_entityTarget)) return;
            ref var healthComp = ref _healthPool.Value.Get(_entityTarget);
            healthComp.CurrentAmount -= damageAmount;
            ref var viewComp = ref _viewPool.Value.Get(_entityTarget);

        }


        private void DeleteEvent() {
            _entityTarget = GameState.NULL_ENTITY;

            _damageEventPool.Value.Del(_entityEvent);
        }
    }
}