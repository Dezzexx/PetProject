using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class DeformableObjectsInit : IEcsInitSystem {
        readonly EcsPoolInject<DeformableObject> _deformableObjectPool = default;
        readonly EcsPoolInject<View> _viewPool = default;

        readonly EcsSharedInject<GameState> _state = default;
        readonly EcsWorldInject _world = default;

        public void Init (EcsSystems systems) {
            var deformableObjectsMB = GameObject.FindObjectsOfType<DeformableObjectMB>();

            foreach (var deformableObjectMB in deformableObjectsMB) {
                var deformableObjectEntity = _world.Value.NewEntity();
                deformableObjectMB.Init(_world.Value, _state.Value, deformableObjectEntity);

                ref var deformableComp = ref _deformableObjectPool.Value.Add(deformableObjectEntity);
                deformableComp.MeshFilter = deformableObjectMB.GetMeshFilter;
                deformableComp.Mesh = deformableObjectMB.GetMesh;
                deformableComp.Vertices = deformableObjectMB.GetSetVertices;

                ref var view = ref _viewPool.Value.Add(deformableObjectEntity);
                view.Transform = deformableObjectMB.transform;
            }
        }
    }
}