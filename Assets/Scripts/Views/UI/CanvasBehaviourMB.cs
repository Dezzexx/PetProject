using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.EcsLite;
using Client;
using UnityEngine.SceneManagement;
using Cinemachine;
using System;

public class CanvasBehaviourMB : MonoBehaviour
{
    #region ECS
    private EcsWorld _world;
    private GameState _state;
    private EcsPool<ClickEvent> _clickPool;
    private EcsPool<InterfaceComponent> _interfacePool = default;

    public void Init(EcsWorld world, GameState state)
    {
        world = state.EcsWorld;
        _world = world;
        _state = state;
        _clickPool = _world.GetPool<ClickEvent>();
        _interfacePool = _world.GetPool<InterfaceComponent>();
    }
    #endregion

    public GameObject InGamePanel;
    public GameObject WinPanel;
    public GameObject LosePanel;
    public GameObject StorePanel;
    public GameObject SettingsPanel;
    public GameObject TutorialPanel;
    public GameObject GeneralPanel;

    private int _cinemachinePOVCameraSpeedOff = 0;
    private float _cinemachinePOVCameraSpeedOn = 0.1f;

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartButton()
    {
        ref var interfaceComponent = ref _interfacePool.Get(_state.InterfaceEntity);

        // interfaceComponent.HealthbarBehaviour.GetHolderHealhbar().gameObject.SetActive(!interfaceComponent.HealthbarBehaviour.GetHolderHealhbar().gameObject.activeSelf);
        _state.GameMode = GameMode.play;
        OpenPlayPanels();
    }

    public void OpenBeforePlayPanels()
    {
        DeactivateAllPanels();
        GeneralPanel.SetActive(true);
        InGamePanel.SetActive(true);
    }

    public void OpenPlayPanels()
    {
        DeactivateAllPanels();
        InGamePanel.SetActive(true);
    }

    public void OpenLosePanels()
    {
        ref var interfaceComponent = ref _interfacePool.Get(_state.InterfaceEntity);
        DeactivateAllPanels();
        interfaceComponent.LosePanelBehaviour.GetHolderLose().gameObject.SetActive(true);
    }

    public void OpenCloseStorePanel()
    {
        ref var interfaceComponent = ref _interfacePool.Get(_state.InterfaceEntity);
        interfaceComponent.StorePanelBehaviour.GetHolderStore().gameObject.SetActive(!interfaceComponent.StorePanelBehaviour.GetHolderStore().gameObject.activeSelf);
        GeneralPanel.SetActive(!GeneralPanel.activeSelf);
        InGamePanel.SetActive(!InGamePanel.activeSelf);
    }

    public void DeactivateAllPanels()
    {
        ref var interfaceComponent = ref _interfacePool.Get(_state.InterfaceEntity);
        InGamePanel.SetActive(false);
        interfaceComponent.WinPanelBehaviour.GetHolderWin().gameObject.SetActive(false);
        interfaceComponent.LosePanelBehaviour.GetHolderLose().gameObject.SetActive(false);
        interfaceComponent.StorePanelBehaviour.GetHolderStore().gameObject.SetActive(false);

        TutorialPanel.SetActive(false);
        GeneralPanel.SetActive(false);
    }

    public void PlayClickSound()
    {
        _clickPool.Add(_world.NewEntity());
    }
}
