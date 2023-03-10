using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
namespace Client {
    sealed class WinCheckSystem : IEcsRunSystem 
    {
        readonly EcsSharedInject<GameState> _state = default;
        readonly EcsWorldInject _world = default;
        readonly EcsFilterInject<Inc<WinCheck>> _winFilter = default;
        readonly EcsPoolInject<View> _viewPool = default;
        readonly EcsPoolInject<AnimationSwitchEvent> _animationSwitchEvent = default;
        readonly EcsPoolInject<WinEvent> _winPool = default;
        private float _waitToStart = 4f;

        public void Run (EcsSystems systems) 
        {
            foreach (var winCheckEntity in _winFilter.Value)
            {
                if (_waitToStart > 0)
                    _waitToStart -= Time.deltaTime;
                else
                {
                    _waitToStart = 0;
                    _state.Value.GameMode = GameMode.win;                    
                    _winPool.Value.Add(_world.Value.NewEntity());

                    _winFilter.Pools.Inc1.Del(winCheckEntity);
                }
            }
        }
    }
}