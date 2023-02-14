using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class RunTimeClipperInitSystem : IEcsInitSystem {
        readonly EcsPoolInject<RuntimeClipper> _runtimeClipperPool = default;

        readonly EcsSharedInject<GameState> _state = default;
        readonly EcsWorldInject _world = default;

        public void Init (EcsSystems systems) {
            var clipper = GameObject.FindObjectOfType<RuntimeCircleClipperMB>();

            var clipperEntity = _world.Value.NewEntity();
            _state.Value.ClipperEntity = clipperEntity;

            clipper.Init(_world.Value, _state.Value, clipperEntity);

            ref var clipperComp = ref _runtimeClipperPool.Value.Add(clipperEntity);
            clipperComp.Clipper = clipper;
        }
    }
}