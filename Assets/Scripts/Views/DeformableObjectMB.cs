using UnityEngine;
using Leopotam.EcsLite;
using Client;

public class DeformableObjectMB : MonoBehaviour
{
    #region ECS
    private EcsWorld _world;
    private GameState _state;
    private EcsPool<View> _viewPool = default;
    private EcsPool<DeformableObject> _deformableObjectPool = default;

    public int _entity;

    public void Init(EcsWorld world, GameState state, int entity)
    {
        _world = world;
        _state = state;
        _entity = entity;
        _viewPool = _world.GetPool<View>();
        _deformableObjectPool = _world.GetPool<DeformableObject>();
    }
#endregion

    private MeshFilter _meshFilter { get; set; }
    private Mesh _mesh { get; set; }
    private Vector3[] _vertices { get; set; }

    public MeshFilter GetMeshFilter {
        get { return _meshFilter; }
        // set { _meshFilter = value; }
    }

    public Mesh GetMesh {
        get { return _mesh; }
        set { _mesh = value; }
    }
    
    public Vector3[] GetSetVertices {
        get { return _vertices; }
        set { _vertices = value; }
    }

    private void Awake() {
        _meshFilter = GetComponent<MeshFilter>();
        _mesh = _meshFilter.mesh;
        _vertices = _mesh.vertices;
    }
}
