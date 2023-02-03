using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.EcsLite;
using Client;

public class TutorialPanelBehaviourMB : MonoBehaviour
{
    #region ECS
    private EcsWorld _world;
    private GameState _state;
    

    public void Init(EcsWorld world, GameState state)
    {
        world = state.EcsWorld;
        _world = world;
        _state = state;
    }
    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
