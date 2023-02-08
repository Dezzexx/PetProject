using UnityEngine;
using Leopotam.EcsLite;
using Client;

public class DonutMB : MonoBehaviour
{
    #region ECS
    private EcsWorld _world;
    private GameState _state;
    private EcsPool<View> _viewPool = default;
    private EcsPool<Donut> _donutPool = default;

    public int _entity;

    public void Init(EcsWorld world, GameState state, int entity)
    {
        _world = world;
        _state = state;
        _entity = entity;
        _viewPool = _world.GetPool<View>();
        _donutPool = _world.GetPool<Donut>();
    }
#endregion

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent<DonutPartMB>(out var donutPartMB)) {
            other.gameObject.SetActive(false);
        }
    }
}
