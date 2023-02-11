using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using System.Collections.Generic;

namespace Client {
    sealed class NavMeshSurfaceInit : IEcsInitSystem {
        readonly EcsPoolInject<NavMeshSurfaceComp> _navMeshSurfacePool = default;

        readonly EcsSharedInject<GameState> _state = default;
        readonly EcsWorldInject _world = default;

        public void Init (EcsSystems systems) {
            var surface = GameObject.FindObjectOfType<NavMeshSurfaceMB>();
            var destinations = GameObject.FindObjectOfType<UnitDestinationMB>().UnitDestinations;

            var surfaceEntity = _world.Value.NewEntity();
            _state.Value.NavMeshSurfaceEntity = surfaceEntity;
            surface.Init(_world.Value, _state.Value, surfaceEntity);

            ref var surfaceComp = ref _navMeshSurfacePool.Value.Add(surfaceEntity);
            surfaceComp.Surface = surface.GetSetSurface;
            surfaceComp.DestinationPoints = new List<Transform>();

            foreach (var destination in destinations) {
                surfaceComp.DestinationPoints.Add(destination);
            }

            surfaceComp.Surface.BuildNavMesh();
        }
    }
}