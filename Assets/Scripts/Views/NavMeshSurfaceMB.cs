using UnityEngine;
using UnityEngine.AI;
using Leopotam.EcsLite;
using Client;

// public class BakingAtStartMB : MonoBehaviour
// {
//     private NavMeshSurface _surface;
//     // private void Start() {
//     //     _surface = GetComponent<NavMeshSurface>();
//     //     _surface.BuildNavMesh();
//     // }
//     // private void Update() {
//     //     _surface.BuildNavMesh();
//     // }
// }
[RequireComponent(typeof(NavMeshSurface))]
public class NavMeshSurfaceMB : MonoBehaviour
{
    #region ECS
    private EcsWorld _world;
    private GameState _state;
    private EcsPool<View> _viewPool = default;
    private EcsPool<NavMeshSurfaceComp> _navMeshSurfacePool = default;

    public int _entity;

    public void Init(EcsWorld world, GameState state, int entity)
    {
        _world = world;
        _state = state;
        _entity = entity;
        _viewPool = _world.GetPool<View>();
        _navMeshSurfacePool = _world.GetPool<NavMeshSurfaceComp>();

        var surface = GetComponent<NavMeshSurface>();
        GetSetSurface = surface;
    }
#endregion

    private NavMeshSurface _surface { get; set; }

    public NavMeshSurface GetSetSurface {
        get { return _surface; }
        set { _surface = value; }
    }
}
