using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Linq;
using UnityEngine;

namespace Client {
    sealed class FindFirstUnitSystem : IEcsRunSystem {       
        readonly EcsFilterInject<Inc<FindFirstUnitEvent>> _findFirstUnitEventFilter = default; 
        readonly EcsPoolInject<FindFirstUnitEvent> _findFirstUnitEvent = default; 
        readonly EcsPoolInject<NavMeshSurfaceComp> _navMeshSurfacePool = default;
        readonly EcsPoolInject<CameraComponent> _cameraPool = default;
        readonly EcsPoolInject<View> _viewPool = default;

        readonly EcsSharedInject<GameState> _state = default;

        public void Run (EcsSystems systems) {
            foreach (var eventEntity in _findFirstUnitEventFilter.Value) {
                ref var navMeshSurfaceComp = ref _navMeshSurfacePool.Value.Get(_state.Value.NavMeshSurfaceEntity);
                ref var cameraComp = ref _cameraPool.Value.Get(_state.Value.CameraEntity);

                Transform firstUnitTransform = null;
                List<float> unitsYPositionArray = new List<float>();
                var allUnits = navMeshSurfaceComp.AllFriendlyUnits;

                for (int i = 0; i < navMeshSurfaceComp.AllFriendlyUnits.Count; i++) {
                    ref var unitViewComp = ref _viewPool.Value.Get(allUnits[i]._entity);

                    if (unitsYPositionArray.Count is 0 || unitViewComp.Transform.position.z >= unitsYPositionArray.Max()) {
                        firstUnitTransform = allUnits[i].transform;
                    }
                    unitsYPositionArray.Add(unitViewComp.Transform.position.z);
                }
                cameraComp.VirtualCamera.Follow = firstUnitTransform;
                _findFirstUnitEvent.Value.Del(eventEntity);
            }
        }
    }
}