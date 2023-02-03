using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.EcsLite;
using Client;

public class GeneralPanelBehaviourMB : MonoBehaviour
{
    #region ECS
    private EcsWorld _world;
    private GameState _state;
    private EcsPool<ClickEvent> _clickPool;
    private EcsPool<FailClickEvent> _failClickPool;
    private EcsPool<BuyEvent> _buyPool;

    public void Init(EcsWorld world, GameState state)
    {
        world = state.EcsWorld;
        _world = world;
        _state = state;
        _clickPool = _world.GetPool<ClickEvent>();
        _failClickPool = _world.GetPool<FailClickEvent>();
        _buyPool = _world.GetPool<BuyEvent>();
    }
    #endregion

    
}
