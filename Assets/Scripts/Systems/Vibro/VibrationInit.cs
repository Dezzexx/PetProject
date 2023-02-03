using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Lofelt.NiceVibrations;

namespace Client
{
    sealed class VibrationInit : IEcsInitSystem
    {
        readonly EcsSharedInject<GameState> _state = default;
        readonly EcsPoolInject<VibrationComponent> _vibroPool = default;

        public void Init(EcsSystems systems)
        {
            var world = systems.GetWorld();
            var gameState = _state.Value;
            var entity = world.NewEntity();
            gameState.VibrationEntity = entity;
            ref var vibroComp = ref _vibroPool.Value.Add(entity);
        }
    }
}