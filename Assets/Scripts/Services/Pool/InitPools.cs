using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class InitPools : IEcsInitSystem
    {
        readonly EcsSharedInject<GameState> _state;

        private int _friendlyUnitInitCount = 50;
        // private int _donutCounter = 150;
        private int _spotCounter = 50;
        private int _unitFightEffectCounter = 50;
        private int _diamondPoofEffectCounter = 2;

#region PoolsValueCount
        
#endregion

        public void Init(EcsSystems systems)
        {
            _state.Value.ActivePools = new AllPools();

            var spawnPoint = new Vector3(0, 0, 500f);

            _state.Value.ActivePools.FriendlyUnitPool = new Pool(_state.Value.AllPools.FriendlyUnitPool.Prefab, spawnPoint, _friendlyUnitInitCount, parentName: "FriendlyUnitPool");      
            // _state.Value.ActivePools.DonutPool = new Pool(_state.Value.AllPools.DonutPool.Prefab, spawnPoint, _donutCounter, parentName: "DonutPool");      
            _state.Value.ActivePools.BlueSpotPool = new Pool(_state.Value.AllPools.BlueSpotPool.Prefab, spawnPoint, _spotCounter, parentName: "BlueSpot");
            _state.Value.ActivePools.RedSpotPool = new Pool(_state.Value.AllPools.RedSpotPool.Prefab, spawnPoint, _spotCounter, parentName: "RedSpot");
            _state.Value.ActivePools.UnitFightEffectPool = new Pool(_state.Value.AllPools.UnitFightEffectPool.Prefab, spawnPoint, _unitFightEffectCounter, parentName: "UnitFightEffect");
            _state.Value.ActivePools.DiamondPoofEffectPool = new Pool(_state.Value.AllPools.DiamondPoofEffectPool.Prefab, spawnPoint, _diamondPoofEffectCounter, parentName: "DiamondPoofEffect");
        }
    }
}
