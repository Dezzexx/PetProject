using UnityEngine;
namespace Client
{
    struct SoundEvent
    {
        public enum SoundValue
        {
            Click, 
            BuyClick, 
            FailClick, 
        }
        public SoundValue Sound;
        public AudioSource EventSource;
    }
}