using UnityEngine;
using Client;
using Leopotam.EcsLite;

public class UnitMB : MonoBehaviour
{
#region ECS
    private EcsWorld _world;
    private GameState _state;
    private EcsPool<View> _viewPool = default;
    private EcsPool<Unit> _unitPool = default;
    private EcsPool<DamageEvent> _damageEvent = default;

    public int _entity;

    public void Init(EcsWorld world, GameState state, int entity)
    {
        _world = world;
        _state = state;
        _entity = entity;
        _viewPool = _world.GetPool<View>();
        _unitPool = _world.GetPool<Unit>();
        _damageEvent = _world.GetPool<DamageEvent>();
    }
#endregion
}
