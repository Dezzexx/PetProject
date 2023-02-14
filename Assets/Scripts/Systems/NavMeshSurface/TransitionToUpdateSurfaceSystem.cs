using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class TransitionToUpdateSurfaceSystem : IEcsRunSystem {   
        readonly EcsFilterInject<Inc<TouchComponent>> _touchFilter = default;
        readonly EcsPoolInject<TouchComponent> _touchPool = default;
        readonly EcsPoolInject<UpdateNavMeshSurfaceEvent> _updateNavMeshSurfaceEvent = default;
        readonly EcsPoolInject<RuntimeClipper> _clipperPool = default;

        readonly EcsSharedInject<GameState> _state = default;
        readonly EcsWorldInject _world = default;

        public void Run (EcsSystems systems) {
            foreach (var touchEntity in _touchFilter.Value) {
                ref var touchComp = ref _touchPool.Value.Get(touchEntity);
                ref var clipperComp = ref _clipperPool.Value.Get(_state.Value.ClipperEntity);

                clipperComp.Clipper.UpdateTouch(touchComp.Position, touchComp.Phase);
            }
        }
    }
}