using UnityEngine;
using Client;
using Leopotam.EcsLite;

public class ChestMB : MonoBehaviour
{
#region ECS
    private EcsWorld _world;
    private GameState _state;
    private EcsPool<Chest> _chestPool = default;
    private EcsPool<CreateEffectEvent> _createEffectEvent = default;

    public int _entity;

    public void Init(EcsWorld world, GameState state, int entity)
    {
        _world = world;
        _state = state;
        _entity = entity;
        _chestPool = _world.GetPool<Chest>();
        _createEffectEvent = _world.GetPool<CreateEffectEvent>();
    }
#endregion

    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _particlePlayTransform;

    public Animator GetAnimator() {
        return _animator;
    }

    public Vector3 GetParticlePlayPosition() {
        return _particlePlayTransform.position;
    }
}
