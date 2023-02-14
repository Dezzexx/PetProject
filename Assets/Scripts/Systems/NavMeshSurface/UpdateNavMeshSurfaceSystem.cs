using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class UpdateNavMeshSurfaceSystem : IEcsRunSystem {   
        readonly EcsFilterInject<Inc<UpdateNavMeshSurfaceEvent, NavMeshSurfaceComp>> _updateSurfaceFilter = default;   
        readonly EcsPoolInject<NavMeshSurfaceComp> _navMeshSurfacePool = default;
        readonly EcsPoolInject<UpdateNavMeshSurfaceEvent> _updateNavMeshSurfaceEvent = default;
        readonly EcsPoolInject<FindFirstUnitEvent> _findFirstUnitEvent = default;

        readonly EcsWorldInject _world = default;

        public void Run (EcsSystems systems) {
            foreach (var updatingSurfaceEntity in _updateSurfaceFilter.Value) {
                ref var navMeshSurfaceComp = ref _navMeshSurfacePool.Value.Get(updatingSurfaceEntity);
                navMeshSurfaceComp.Surface.BuildNavMesh();
                
                // _findFirstUnitEvent.Value.Add(_world.Value.NewEntity());

                _updateNavMeshSurfaceEvent.Value.Del(updatingSurfaceEntity);
            }
        }
    }
}