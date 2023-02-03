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

#region PoolsValueCount
        
#endregion

        public void Init(EcsSystems systems)
        {
            _state.Value.ActivePools = new AllPools();

            var spawnPoint = new Vector3(0, 0, 1000f);
            // _state.Value.ActivePools.GridCellPool = new Pool(_state.Value.AllPools.GridCellPool.Prefab, spawnPoint, _gridCellPoolCount, parentName: "GridCellPool");
            
        }
    }
}
