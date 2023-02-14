using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Cinemachine;

namespace Client {
    sealed class InitCamera : IEcsInitSystem
    {
        readonly EcsSharedInject<GameState> _state = default;
        readonly EcsWorldInject _world = default;
        readonly EcsPoolInject<CameraComponent> _cameraComponentPool = default;
        public void Init (EcsSystems systems) {
            Camera camera = Camera.main;

            int entity = _world.Value.NewEntity();
            ref var CameraComponent = ref _cameraComponentPool.Value.Add(entity);
            CameraComponent.Camera = camera;
            CameraComponent.VirtualCamera = GameObject.FindObjectOfType<CinemachineVirtualCameraPlug>().GetComponent<CinemachineVirtualCamera>();

            _state.Value.CameraEntity = entity;
        }
    }
}