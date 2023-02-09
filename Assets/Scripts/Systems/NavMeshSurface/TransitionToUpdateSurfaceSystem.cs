using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class TransitionToUpdateSurfaceSystem : IEcsRunSystem {   
        readonly EcsFilterInject<Inc<TouchComponent>> _touchFilter = default;
        readonly EcsPoolInject<TouchComponent> _touchPool = default;
        readonly EcsPoolInject<UpdatingNavMeshSurface> _updateNavMeshSurfaceEvent = default;

        readonly EcsSharedInject<GameState> _state = default;
        readonly EcsWorldInject _world = default;

        public void Run (EcsSystems systems) {
            foreach (var touchEntity in _touchFilter.Value) {
                ref var touchComp = ref _touchPool.Value.Get(touchEntity);
                
                switch (touchComp.Phase)
                {
                    case TouchPhase.Began:
                        ApplyUpdateComp();
                        break;

                    case TouchPhase.Ended:
                        DeleteUpdateComp();
                        break;

                    case TouchPhase.Canceled:
                        DeleteUpdateComp();
                        break;
                    
                    default:
                        break;
                }
            }
        }

        private void ApplyUpdateComp() {
            _updateNavMeshSurfaceEvent.Value.Add(_state.Value.NavMeshSurfaceEntity);
        }

        private void DeleteUpdateComp() {
            _updateNavMeshSurfaceEvent.Value.Del(_state.Value.NavMeshSurfaceEntity);
        }
    }
}