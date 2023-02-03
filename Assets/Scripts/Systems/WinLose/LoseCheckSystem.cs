using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class LoseCheckSystem : IEcsRunSystem {  
        readonly EcsFilterInject<Inc<LoseCheck>> _loseFilter = default;

        readonly EcsPoolInject<LoseEvent> _losePool = default;
        readonly EcsPoolInject<LoseCheck> _loseCheck = default;

        readonly EcsSharedInject<GameState> _state = default;
        readonly EcsWorldInject _world = default;

        // private float _waitToStart = 2f;

        public void Run (EcsSystems systems) {
            foreach (var loseCheckEntity in _loseFilter.Value) {  
                // if (_waitToStart > 0)
                //     _waitToStart -= Time.deltaTime;
                // else {
                //     _waitToStart = 0;
                //     _state.Value.GameMode = GameMode.lose;
                //     _losePool.Value.Add(_world.Value.NewEntity());

                //     _loseFilter.Pools.Inc1.Del(entity);
                // }
                
                _state.Value.GameMode = GameMode.lose;
                _losePool.Value.Add(_world.Value.NewEntity());
                _loseCheck.Value.Del(loseCheckEntity);
            }
        }
    }
}