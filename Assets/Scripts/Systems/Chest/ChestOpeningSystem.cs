using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class ChestOpeningSystem : IEcsRunSystem {    
        readonly EcsFilterInject<Inc<Chest, Opening>> _chestFilter = default;
        readonly EcsPoolInject<Chest> _chestPool = default;
        readonly EcsPoolInject<CreateEffectEvent> _createEffectEvent = default;

        readonly EcsWorldInject _world = default;

        // private string _animationHash = new string("Opening");
        // private float _waitToStart = 0.9f;

        public void Run (EcsSystems systems) {
            foreach (var chestEntity in _chestFilter.Value) {
                ref var chestComp = ref _chestPool.Value.Get(chestEntity);
                chestComp.ChestMB.GetAnimator().SetBool("Opening", true);

                // var animationState = chestComp.Animator.GetCurrentAnimatorStateInfo(0); 
                // if (_waitToStart > 0)
                //     _waitToStart -= Time.deltaTime;
                // else {
                //     CreateParticleEffect(EffectType.DiamondPoofEffect, chestComp.ParticlePlayPosition);
                //     _waitToStart = 0;
                // }
                // if (animationState.IsTag(_animationHash)) {
                    
                // }
            }
        }

        private void CreateParticleEffect(EffectType effectType, Vector3 effectSpawnPoint) {
            _createEffectEvent.Value.Add(_world.Value.NewEntity()).Invoke(effectType, effectSpawnPoint, Vector3.zero);
        }
    }
}