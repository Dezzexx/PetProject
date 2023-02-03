using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.EcsLite;
using Client;

public class LosePanelBehaviourMB : MonoBehaviour
{
    #region ECS
    private EcsWorld _world;
    private GameState _state;
    private EcsPool<ClickEvent> _clickPool;

    [SerializeField] private Transform _holderLose;
    

    public void Init(EcsWorld world, GameState state)
    {
        world = state.EcsWorld;
        _world = world;
        _state = state;
        _clickPool = _world.GetPool<ClickEvent>();
    }
    #endregion

    public Transform GetHolderLose()
    {
        return _holderLose;
    }
    
}
