using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class UpdateNavMeshSurfaceSystem : IEcsRunSystem {   
        readonly EcsFilterInject<Inc<UpdatingNavMeshSurface, NavMeshSurfaceComp>> _updatingSurfaceFilter = default;   
        readonly EcsPoolInject<NavMeshSurfaceComp> _navMeshSurfacePool = default;

        public void Run (EcsSystems systems) {
            foreach (var updatingSurfaceEntity in _updatingSurfaceFilter.Value) {
                ref var navMeshSurfaceComp = ref _navMeshSurfacePool.Value.Get(updatingSurfaceEntity);
                navMeshSurfaceComp.Surface.BuildNavMesh();
            }
        }
    }
}