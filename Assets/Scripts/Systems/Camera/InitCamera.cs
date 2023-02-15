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
        readonly EcsPoolInject<UnitsHolder> _unitsHolderPool = default;

        private float _unitParameters = 1;

        public void Init (EcsSystems systems) {
            Camera camera = Camera.main;
            var targetGroup = GameObject.FindObjectOfType<CinemachineTargetGroup>();

            int entity = _world.Value.NewEntity();
            ref var CameraComponent = ref _cameraComponentPool.Value.Add(entity);
            CameraComponent.Camera = camera;
            CameraComponent.TargetGroup = targetGroup;

            ref var unitsHolderComp = ref _unitsHolderPool.Value.Get(_state.Value.UnitsHolderEntity);

            foreach (var friendlyUnitMB in unitsHolderComp.FriendlyUnitsHolder) {
                targetGroup.AddMember(friendlyUnitMB.transform, _unitParameters, _unitParameters);
            }

            _state.Value.CameraEntity = entity;
        }
    }
}