using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class ChestInitSystem : IEcsInitSystem {
        readonly EcsPoolInject<Chest> _chestPool = default;

        readonly EcsSharedInject<GameState> _state = default;
        readonly EcsWorldInject _world = default;

        public void Init (EcsSystems systems) {
            var chestEntity = _world.Value.NewEntity();

            var chestMB = GameObject.FindObjectOfType<ChestMB>();
            chestMB.Init(_world.Value, _state.Value, chestEntity);

            ref var chestComp = ref _chestPool.Value.Add(chestEntity);
            chestComp.Animator = chestMB.GetAnimator();
            chestComp.ChestMB = chestMB;
            chestComp.ParticlePlayPosition = chestMB.GetParticlePlayPosition();
        }
    }
}