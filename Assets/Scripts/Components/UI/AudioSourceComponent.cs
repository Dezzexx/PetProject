using UnityEngine;

namespace Client
{
    struct AudioSourceComponent
    {
        public AudioBehaviourMB AudioBehaviourMB;
        public AudioSource UIAudioSource;
        public AudioSource PlayerAudioSource;
        public AudioSource EnemyAudioSource;
        public AudioSource EnvironmentAudioSource;
    }
}