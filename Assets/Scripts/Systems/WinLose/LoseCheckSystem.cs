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

        public void Run (EcsSystems systems) {
            foreach (var loseCheckEntity in _loseFilter.Value) {  
                _state.Value.GameMode = GameMode.lose;
                _losePool.Value.Add(_world.Value.NewEntity());
                _loseCheck.Value.Del(loseCheckEntity);
            }
        }
    }
}