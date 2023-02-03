using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class InitSounds : IEcsInitSystem
    {
        private EcsSharedInject<GameState> _state = default;
        private EcsPoolInject<AudioSourceComponent> _audioPool = default;

        public void Init(EcsSystems systems)
        {
            var world = systems.GetWorld();
            var entity = world.NewEntity();
            _state.Value.SoundsEntity = entity;
            ref var audioComp = ref _audioPool.Value.Add(entity);

            var audioBehaviorGO = GameObject.FindGameObjectWithTag("AudioBehaviour");
            audioComp.AudioBehaviourMB = audioBehaviorGO.GetComponent<AudioBehaviourMB>();
            
            var UIAudioSourceGO = GameObject.FindGameObjectWithTag("UIAudioSource");
            audioComp.UIAudioSource = UIAudioSourceGO.GetComponent<AudioSource>();

            var playerAudioSourceGO = GameObject.FindGameObjectWithTag("PlayerAudioSource");
            audioComp.PlayerAudioSource = playerAudioSourceGO.GetComponent<AudioSource>();

            var enemyAudioSourceGO = GameObject.FindGameObjectWithTag("EnemyAudioSource");
            audioComp.EnemyAudioSource = enemyAudioSourceGO.GetComponent<AudioSource>();

            var environmentAudioSourceGO = GameObject.FindGameObjectWithTag("EnvironmentAudioSource");
            audioComp.EnvironmentAudioSource = environmentAudioSourceGO.GetComponent<AudioSource>();

            
            if(_state.Value.Sounds)
            {
                audioComp.AudioBehaviourMB.OnSoundsVol();
            }
            else
            {
                audioComp.AudioBehaviourMB.OffSoundsVol();
            }

            if(_state.Value.Music)
            {
                audioComp.AudioBehaviourMB.OnMusicVol();
            }
            else
            {
                audioComp.AudioBehaviourMB.OffMusicVol();
            }
        }
    }
}