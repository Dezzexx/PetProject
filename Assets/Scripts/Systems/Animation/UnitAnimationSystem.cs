using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class UnitAnimationSystem : IEcsRunSystem {   
        readonly EcsFilterInject<Inc<AnimationSwitchEvent, UnitAnimator>, Exc<Dead>> _animationSwitchEventFilter = default;
        readonly EcsPoolInject<UnitAnimator> _animatorPool = default;   
        readonly EcsPoolInject<AnimationSwitchEvent> _animationSwitchEventPool = default;

        public void Run (EcsSystems systems) {
            foreach (var animationSwitchEventEntity in _animationSwitchEventFilter.Value) {
                ref var animatorComp = ref _animatorPool.Value.Get(animationSwitchEventEntity);
                ref var animationSwitchEventComp = ref _animationSwitchEventPool.Value.Get(animationSwitchEventEntity);
                // Debug.Log("зашел в анимации");
                switch (animationSwitchEventComp.AnimationSwitcher)
                {
                    case AnimationSwitchEvent.AnimationType.Idle:
                        animatorComp.UnityAnimator.SetBool("Win", false);
                        animatorComp.UnityAnimator.SetBool("Shoot", false);
                        animatorComp.UnityAnimator.SetBool("Idle", true);
                        break;

                    case AnimationSwitchEvent.AnimationType.StayShoot:
                        animatorComp.UnityAnimator.SetBool("Win", false);
                        animatorComp.UnityAnimator.SetBool("Shoot", true);
                        animatorComp.UnityAnimator.SetBool("Idle", false);
                        break;

                    case AnimationSwitchEvent.AnimationType.Win:
                        animatorComp.UnityAnimator.SetBool("Win", true);
                        animatorComp.UnityAnimator.SetBool("Shoot", false);
                        animatorComp.UnityAnimator.SetBool("Idle", false);
                        break;

                    case AnimationSwitchEvent.AnimationType.DefaultRun:
                        animatorComp.UnityAnimator.SetBool("Win", false);
                        animatorComp.UnityAnimator.SetBool("Shoot", false);
                        animatorComp.UnityAnimator.SetBool("Idle", false);
                        break;
                    
                    default:
                        break;
                }
                _animationSwitchEventPool.Value.Del(animationSwitchEventEntity);
            }
        }
    }
}