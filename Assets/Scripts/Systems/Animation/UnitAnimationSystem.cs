using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client {
    sealed class UnitAnimationSystem : IEcsRunSystem {   
        readonly EcsFilterInject<Inc<AnimationSwitchEvent, UnitAnimator>, Exc<Dead>> _animationSwitchEventFilter = default;
        readonly EcsPoolInject<UnitAnimator> _animatorPool = default;   
        readonly EcsPoolInject<AnimationSwitchEvent> _animationSwitchEventPool = default;

        public void Run (EcsSystems systems) {
            foreach (var animationSwitchEventEntity in _animationSwitchEventFilter.Value) {
                ref var animatorComp = ref _animatorPool.Value.Get(animationSwitchEventEntity);
                ref var animationSwitchEventComp = ref _animationSwitchEventPool.Value.Get(animationSwitchEventEntity);
                switch (animationSwitchEventComp.AnimationSwitcher)
                {
                    case AnimationSwitchEvent.AnimationType.Run:
                        animatorComp.UnityAnimator.SetBool("Run", true);
                        break;
                    
                    default:
                        break;
                }
                _animationSwitchEventPool.Value.Del(animationSwitchEventEntity);
            }
        }
    }
}