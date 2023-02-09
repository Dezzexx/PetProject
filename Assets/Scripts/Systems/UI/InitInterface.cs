using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Client
{
    sealed class InitInterface : IEcsInitSystem
    {
        readonly EcsSharedInject<GameState> _state = default;
        readonly EcsWorldInject _world = default;
        readonly EcsPoolInject<InterfaceComponent> _interfacePool = default;

        public void Init(EcsSystems systems)
        {
            var entity = _world.Value.NewEntity();
            _state.Value.InterfaceEntity = entity;

            ref var interfaceComp = ref _interfacePool.Value.Add(entity);
            var canvasGO = GameObject.FindGameObjectWithTag("Canvas");

            interfaceComp.CanvasBehaviour = canvasGO.GetComponent<CanvasBehaviourMB>();
            interfaceComp.CanvasBehaviour.Init(_world.Value, _state.Value);

            var inGamePanelBehaviourMB = interfaceComp.CanvasBehaviour.GetComponentInChildren<InGamePanelBehaviourMB>();
            inGamePanelBehaviourMB.Init(_world.Value, _state.Value);
            interfaceComp.InGamePanelBehaviour = inGamePanelBehaviourMB;

            var settingsPanelBehaviourMB = interfaceComp.CanvasBehaviour.GetComponentInChildren<SettingsPanelBehaviourMB>();
            settingsPanelBehaviourMB.Init(_world.Value, _state.Value);
            interfaceComp.SettingsPanelBehaviour = settingsPanelBehaviourMB;

            var winPanelBehaviourMB = interfaceComp.CanvasBehaviour.GetComponentInChildren<WinPanelBehaviourMB>();
            winPanelBehaviourMB.Init(_world.Value, _state.Value);
            interfaceComp.WinPanelBehaviour = winPanelBehaviourMB;

            var losePanelBehaviourMB = interfaceComp.CanvasBehaviour.GetComponentInChildren<LosePanelBehaviourMB>();
            losePanelBehaviourMB.Init(_world.Value, _state.Value);
            interfaceComp.LosePanelBehaviour = losePanelBehaviourMB;

            var tutorialPanelBehaviourMB = interfaceComp.CanvasBehaviour.GetComponentInChildren<TutorialPanelBehaviourMB>();
            tutorialPanelBehaviourMB.Init(_world.Value, _state.Value);
            interfaceComp.TutorialPanelBehaviour = tutorialPanelBehaviourMB;

            var generalPanelBehaviourMB = interfaceComp.CanvasBehaviour.GetComponentInChildren<GeneralPanelBehaviourMB>();
            generalPanelBehaviourMB.Init(_world.Value, _state.Value);
            interfaceComp.GenaralPanelBehaviourMB = generalPanelBehaviourMB;

            // interfaceComp.HealthbarBehaviour = GameObject.FindObjectOfType<HealthbarMB>();
            // interfaceComp.HealthbarBehaviour.Init(_world.Value, _state.Value);
            // interfaceComp.HealthbarBehaviour.GetHolderHealhbar().gameObject.SetActive(!interfaceComp.HealthbarBehaviour.GetHolderHealhbar().gameObject.activeSelf);
            //методы для старта
            interfaceComp.CanvasBehaviour.OpenBeforePlayPanels();
        }
    }
}