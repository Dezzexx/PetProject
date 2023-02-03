using UnityEngine;
using Leopotam.EcsLite;
using Client;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGamePanelBehaviourMB : MonoBehaviour
{
    #region ECS
    private EcsWorld _world;
    private GameState _state;
    private EcsPool<SpawnEvent> _spawnEventPool = default;
    
    public void Init(EcsWorld world, GameState state)
    {
        world = state.EcsWorld;
        _world = world;
        _state = state;
        _spawnEventPool = _world.GetPool<SpawnEvent>();

        UpdateMoneyValue(_state.PlayerResourceValue);
    }
    #endregion

    [SerializeField] private Text _resourceAmount;

    public void UpdateMoneyValue(int value) {
        _resourceAmount.text = value.ToString();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
