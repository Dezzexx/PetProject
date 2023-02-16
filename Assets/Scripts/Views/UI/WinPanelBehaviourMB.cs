using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.EcsLite;
using Client;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class WinPanelBehaviourMB : MonoBehaviour
{
    #region ECS
    private EcsWorld _world;
    private GameState _state;
    private EcsPool<ClickEvent> _clickPool;

    [SerializeField] private Transform _target;
    [SerializeField] private Transform _holderWin;
    [SerializeField] private Text _rewardText;
    [SerializeField] private GameObject _leftConfetti;
    [SerializeField] private GameObject _rigthConfetti;
    

    public void Init(EcsWorld world, GameState state)
    {
        _world = world;
        _state = state;
        _clickPool = _world.GetPool<ClickEvent>();
    }
    #endregion

    public void NextLevel()
    {
        _clickPool.Add(_world.NewEntity());
        SceneManager.LoadScene(_state.SceneNumber);
    }

    public void StartWinEvent()
    {
        StartCoroutine(StartWin(1));
    }
    
    private IEnumerator StartWin(int value)
    {
        yield return new WaitForSeconds(value);
        _holderWin.gameObject.SetActive(true);
        _holderWin.transform.DOMove(_target.position, 1, false);
    }

    public Transform GetHolderWin() {
        return _holderWin;
    }

    public Text GetRewardText() {
        return _rewardText;
    }
}
