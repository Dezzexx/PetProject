using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class DeformMeshSystem : IEcsRunSystem {   
        readonly EcsFilterInject<Inc<TouchComponent>> _touchFilter = default;
        readonly EcsPoolInject<TouchComponent> _touchPool = default;
        readonly EcsPoolInject<CameraComponent> _cameraPool = default;

        readonly EcsFilterInject<Inc<DeformableObject>> _deformableObjectFilter = default;
        readonly EcsPoolInject<DeformableObject> _deformableObjectPool = default;
        readonly EcsPoolInject<View> _viewPool = default;
        readonly EcsPoolInject<Donut> _donutPool = default;

        readonly EcsSharedInject<GameState> _state = default;
        readonly EcsWorldInject _world = default;

        private Ray _ray;
        private RaycastHit _hit;

        public void Run (EcsSystems systems) {
            foreach (var touchEntity in _touchFilter.Value) {
                ref var cameraComp = ref _cameraPool.Value.Get(_state.Value.CameraEntity);
                ref var touchComp = ref _touchPool.Value.Get(touchEntity);
                

                switch (touchComp.Phase)
                {
                    case TouchPhase.Began:
                        DeformMesh(cameraComp.Camera, touchComp.Position, _state.Value.DeformableMask);
                        break;

                    case TouchPhase.Moved:
                        DeformMesh(cameraComp.Camera, touchComp.Position, _state.Value.DeformableMask);
                        break;

                    // case TouchPhase.Stationary:
                    //     break;

                    // case TouchPhase.Ended:
                    //     break;

                    // case TouchPhase.Canceled:
                    //     break;
                    
                    default:
                        break;
                }
            }
        }

        private void DeformMesh(Camera camera, Vector3 inputPosition, LayerMask deformableMask) {
            _ray = camera.ScreenPointToRay(inputPosition);

            if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, deformableMask)) {
                DeformObject(_hit.point);
                var donut = _state.Value.ActivePools.DonutPool.GetFromPool();
                var donutMB = donut.GetComponent<DonutMB>();
                var donutEntity = _world.Value.NewEntity();
                donutMB.Init(_world.Value, _state.Value, donutEntity);

                ref var donutView = ref _viewPool.Value.Add(donutEntity);
                donutView.Transform = donut.transform;
                donutView.Transform.position = new Vector3(_hit.point.x, 0.417f, _hit.point.z);

                ref var donutComp = ref _donutPool.Value.Add(donutEntity);
            }
        }

        private void DeformObject (Vector3 positionToDeform) {
            foreach (var deformableObjectEntity in _deformableObjectFilter.Value) {
                ref var deformableObjectComp = ref _deformableObjectPool.Value.Get(deformableObjectEntity);
                ref var viewComp = ref _viewPool.Value.Get(deformableObjectEntity);

                positionToDeform = viewComp.Transform.InverseTransformPoint(positionToDeform);

                for (int i = 0; i < deformableObjectComp.Vertices.Length; i++) {
                    var distance = (deformableObjectComp.Vertices[i] - positionToDeform).sqrMagnitude;

                    if (distance < _state.Value.DeformableObjectsConfig.DeformableRadius) {
                        deformableObjectComp.Vertices[i] -= Vector3.up * _state.Value.DeformableObjectsConfig.DefomablePower;
                    }
                }

                deformableObjectComp.Mesh.vertices = deformableObjectComp.Vertices; 
            }
        }
    }
}