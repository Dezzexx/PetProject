using System.Xml.Schema;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Client {
    sealed class WinSystem : IEcsRunSystem 
    {
        readonly EcsWorldInject _world = default;
        readonly EcsSharedInject<GameState> _state = default;
        readonly EcsFilterInject<Inc<WinEvent>> _winFilter = default;
        readonly EcsPoolInject<InterfaceComponent> _interfacePool = default;

        // Text CountSoldier;
        // Text CountTanks;
        // Text CountHelicopter; 
        // Text CountTruck;
        // Text CountGigant;

        int entityEvent = GameState.NULL_ENTITY;
        public void Run (EcsSystems systems) 
        {
            foreach (var evnt in _winFilter.Value)
            {
                entityEvent = evnt;
                ref var interfaceComponent = ref _interfacePool.Value.Get(_state.Value.InterfaceEntity);
                int index = 0;
                if (SceneManager.GetActiveScene().buildIndex + 1 > SceneManager.sceneCountInBuildSettings - 1)
                    index = 0;
                else
                    index = SceneManager.GetActiveScene().buildIndex + 1;


                _state.Value.SceneNumber = index;
                // _state.Value.Level++;

                interfaceComponent.WinPanelBehaviour.StartWinEvent();
                _state.Value.PlayerResourceValue += 200;


                _state.Value.Save();
                DeleteEvent();
            }
        }
        private void DeleteEvent()
        {
            _winFilter.Pools.Inc1.Del(entityEvent);
        }
    }
}