using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using System.Collections.Generic;
using System;

namespace Client
{
    sealed class InitPools : IEcsInitSystem
    {
        readonly EcsSharedInject<GameState> _state;

        private int _unitInitCount = 60;

#region PoolsValueCount
        
#endregion

        public void Init(EcsSystems systems)
        {
            _state.Value.ActivePools = new AllPools();

            var spawnPoint = new Vector3(0, 0, 500f);

            _state.Value.ActivePools.UnitPool = new Pool(_state.Value.AllPools.UnitPool.Prefab, spawnPoint, _unitInitCount, parentName: "UnitPool");            
        }
    }
}
