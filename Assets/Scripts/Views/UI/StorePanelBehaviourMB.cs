using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.EcsLite;
using Client;
using UnityEngine.UI;

public class StorePanelBehaviourMB : MonoBehaviour
{
    #region ECS
    private EcsWorld _world;
    private GameState _state;
    private EcsPool<ClickEvent> _clickPool;
    private EcsPool<FailClickEvent> _failClickPool;
    private EcsPool<BuyEvent> _buyPool;

    [SerializeField] private Text _moneyAmount;
    [SerializeField] private Transform _holderStore;

    public void Init(EcsWorld world, GameState state)
    {
        world = state.EcsWorld;
        _world = world;
        _state = state;
        _clickPool = _world.GetPool<ClickEvent>();
        _failClickPool = _world.GetPool<FailClickEvent>();
        _buyPool = _world.GetPool<BuyEvent>();

        // SetupMoneyValue(_state.PlayerResourceValue);
    }
    #endregion

    // public void SetupMoneyValue(int value) {
    //     _moneyAmount.text = value.ToString();
    // }

    public Transform GetHolderStore()
    {
        return _holderStore;
    }   
}
