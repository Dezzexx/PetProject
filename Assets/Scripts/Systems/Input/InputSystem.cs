using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using InputTouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace Client
{
    sealed class InputSystem : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<InputComponent>, Exc<DisableInputComponent>> filter = default;
        readonly EcsPoolInject<TouchComponent> touchPool = default;
        readonly EcsSharedInject<GameState> gameState = default;
        public void Destroy(EcsSystems systems)
        {
            EnhancedTouchSupport.Disable();
            TouchSimulation.Disable();
        }

        public void Run(EcsSystems systems)
        {
            foreach (int entity in filter.Value)
            {
                if (Touch.activeTouches.Count == 0) continue;

                var activeTouch = Touch.activeTouches[0];
                var phase = activeTouch.phase;

                if (phase == InputTouchPhase.Began)
                {
                    if (!touchPool.Value.Has(entity))
                    {
                        touchPool.Value.Add(entity);
                    }
                    ref var touch = ref touchPool.Value.Get(entity);
                    touch.Phase = TouchPhase.Began;
                    touch.Direction = Vector3.zero;
                    touch.Position = activeTouch.screenPosition;
                    touch.InitialPosition = touch.Position;
                    
                    continue;
                }

                if (phase == InputTouchPhase.Ended || phase == InputTouchPhase.Canceled)
                {
                    if (!touchPool.Value.Has(entity))
                    {
                        touchPool.Value.Add(entity);
                    }
                    ref var touch = ref touchPool.Value.Get(entity);
                    touch.Phase = TouchPhase.Ended;
                    touch.Direction = Vector3.zero;
                    touch.Position = activeTouch.screenPosition;
                    continue;
                }
                if (phase == InputTouchPhase.Moved)
                {

                    if (!touchPool.Value.Has(entity)) touchPool.Value.Add(entity);
                    ref var touch = ref touchPool.Value.Get(entity);

                    touch.Phase = TouchPhase.Moved;
                    touch.Direction = activeTouch.screenPosition - touch.InitialPosition;
                    touch.Position = activeTouch.screenPosition;

                    
                }
                else if (phase == InputTouchPhase.Stationary)
                {
                    if (!touchPool.Value.Has(entity)) touchPool.Value.Add(entity);
                    ref var touch = ref touchPool.Value.Get(entity);
                    touch.Phase = TouchPhase.Stationary;
                    touch.Direction = Vector3.zero;
                    touch.Position = activeTouch.screenPosition;
                }
            }
        }
    }
}
