using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine.UI;

namespace Client
{
    public class HealthbarMB : MonoBehaviour
    {
        private EcsWorld _world;
        private GameState _state;
        [SerializeField] private Transform _holderHealthbar;
        [SerializeField] private Slider healthSlider;

        public void Init(EcsWorld world, GameState state)
        {
            _world = world;
            _state = state;
        }
        public void SliderUpdate(float value)
        {
            healthSlider.value = value;
        }
        public Transform GetHolderHealhbar()
        {
            return _holderHealthbar;
        }
    }
}
