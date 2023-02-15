using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class InitPools : IEcsInitSystem
    {
        readonly EcsSharedInject<GameState> _state;

        private int _friendlyUnitInitCount = 50;
        private int _donutCounter = 150;

#region PoolsValueCount
        
#endregion

        public void Init(EcsSystems systems)
        {
            _state.Value.ActivePools = new AllPools();

            var spawnPoint = new Vector3(0, 0, 500f);

            _state.Value.ActivePools.FriendlyUnitPool = new Pool(_state.Value.AllPools.FriendlyUnitPool.Prefab, spawnPoint, _friendlyUnitInitCount, parentName: "FriendlyUnitPool");      
            _state.Value.ActivePools.DonutPool = new Pool(_state.Value.AllPools.DonutPool.Prefab, spawnPoint, _donutCounter, parentName: "DonutPool");      
        }
    }
}
