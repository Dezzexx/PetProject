using System;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class CreateEffectEventSystem : IEcsRunSystem
    {
        readonly EcsWorldInject _world;

        readonly EcsSharedInject<GameState> _state;

        readonly EcsFilterInject<Inc<CreateEffectEvent>> _createEffectEventFilter = default;

        readonly EcsPoolInject<CreateEffectEvent> _createEffectEventPool = default;

        private EcsPoolInject<AudioSourceComponent> _audioPool = default;

        private int _eventEntity = GameState.NULL_ENTITY;

        public AudioSource AudioSource;

        public void Run (EcsSystems systems)
        {
            foreach (var eventEntity in _createEffectEventFilter.Value)
            {
                _eventEntity = eventEntity;

                ref var createEffectEventComponent = ref _createEffectEventPool.Value.Get(_eventEntity);

                switch (createEffectEventComponent.EffectType)
                {
                    case EffectType.UnitFightEffect:
                        InvokeUnitFightEffect(createEffectEventComponent);
                        break;
                    
                    case EffectType.DiamondPoofEffect:
                        InvokeDiamondPoofEffect(createEffectEventComponent);
                        break;

                    default:
                        Debug.LogWarning("Income unknown EffectType!");
                        break;
                }

                DeleteEvent();
            }
        }

        private void InvokeDiamondPoofEffect(CreateEffectEvent createEffectEventComponent)
        {
            var poofEffect = _state.Value.ActivePools.DiamondPoofEffectPool.GetFromPool();

            poofEffect.transform.position = createEffectEventComponent.SpawnPoint;

            if (poofEffect.TryGetComponent<ParticleSystem>(out var poofEffectParticle))
            {
                poofEffectParticle.Play();
            }
            else
            {
                Debug.LogWarning("PoofEffect object from DiamondPoofEffectPool haven't ParticleSystem!");
            }
            _state.Value.ActivePools.DiamondPoofEffectPool.ReturnToPool(poofEffect);
        }

        private void InvokeUnitFightEffect(CreateEffectEvent createEffectEventComponent)
        {
            var fightEffect = _state.Value.ActivePools.UnitFightEffectPool.GetFromPool();

            fightEffect.transform.position = createEffectEventComponent.SpawnPoint;

            if (fightEffect.TryGetComponent<ParticleSystem>(out var fightEffectParticle))
            {
                fightEffectParticle.Play();
            }
            else
            {
                Debug.LogWarning("FightEffect object from UnitFightEffectPool haven't ParticleSystem!");
            }
            _state.Value.ActivePools.UnitFightEffectPool.ReturnToPool(fightEffect);
        }

        private void DeleteEvent()
        {
            _createEffectEventPool.Value.Del(_eventEntity);

            _eventEntity = GameState.NULL_ENTITY;
        }
    }
}