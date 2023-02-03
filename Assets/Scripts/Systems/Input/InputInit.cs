using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using InputTouchPhase = UnityEngine.InputSystem.TouchPhase;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Client {
    sealed class InputInit : IEcsInitSystem {
        readonly EcsPoolInject<TouchComponent> _touchPool = default;
        readonly EcsPoolInject<InputComponent> _inputPool = default;
        readonly EcsSharedInject<GameState> _gameState = default;
        public void Init (EcsSystems systems)
        {
            var inputentity = _touchPool.Value.GetWorld().NewEntity();
            _inputPool.Value.Add(inputentity);
            //_disableinputpool.Value.Add(_inputentity);
            _gameState.Value.InputEntity = inputentity;

            Input.multiTouchEnabled = false;

            _gameState.Value.Raycaster = GameObject.FindObjectOfType<CanvasBehaviourMB>().GetComponent<GraphicRaycaster>();
            _gameState.Value.EventSystem = GameObject.FindObjectOfType<EventSystem>();

            EnhancedTouchSupport.Enable();
            TouchSimulation.Enable();

            //Debug.Log($"InputSystem: Init - EnhancedTouchSupport.enabled = {EnhancedTouchSupport.enabled}");
        }
    }
}