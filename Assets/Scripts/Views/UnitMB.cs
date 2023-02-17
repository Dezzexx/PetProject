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
    private EcsPool<Health> _healthPool = default;
    private EcsPool<WinCheck> _winCheckPool = default;
    private EcsPool<Opening> _openingPool = default;

    public int _entity;

    public void Init(EcsWorld world, GameState state, int entity)
    {
        _world = world;
        _state = state;
        _entity = entity;
        _viewPool = _world.GetPool<View>();
        _unitPool = _world.GetPool<Unit>();
        _healthPool = _world.GetPool<Health>();
        _damageEvent = _world.GetPool<DamageEvent>();
        _winCheckPool = _world.GetPool<WinCheck>();
        _openingPool = _world.GetPool<Opening>();
    }
#endregion

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent<UnitMB>(out var unitMB)) {
            ApplyDamageEvent(UnitType, unitMB);
        }

        // перенести в ецс к сундуку
        if (other.TryGetComponent<ChestMB>(out var chestMB)) {
            if (!_openingPool.Has(chestMB._entity)) _openingPool.Add(chestMB._entity);
            _winCheckPool.Add(_world.NewEntity());
        }
    }

    private void ApplyDamageEvent(UnitTypes unitType, UnitMB unitMB) {
        if (unitMB.UnitType != unitType) {
            ref var damageEvent = ref _damageEvent.Add(_world.NewEntity());
            damageEvent.Damage = UnitParameterConfig.Damage;
            damageEvent.EntityTarget = unitMB._entity;
        }
    }

    public UnitTypes UnitType;
    public UnitParameterConfig UnitParameterConfig;
}

public enum UnitTypes {FriendlyUnit, EnemyUnit}
