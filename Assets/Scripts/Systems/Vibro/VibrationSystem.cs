using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Lofelt.NiceVibrations;

namespace Client
{
    sealed class VibrationSystem : IEcsRunSystem
    {
        readonly EcsSharedInject<GameState> _state = default;
        readonly EcsFilterInject<Inc<VibrationEvent>> _filter = default;

        private float _coolDown = 0.5f;
        private float _timer = 0;

        public void Run(EcsSystems systems)
        {
            _timer -= Time.deltaTime;

            foreach (var entity in _filter.Value)
            {
                if (_state.Value.Vibration)
                {
                    if(_timer > 0)
                    {
                        _filter.Pools.Inc1.Del(entity);
                        return;
                    }

                    _timer = _coolDown;

                    ref var vibroComp = ref _filter.Pools.Inc1.Get(entity);
                    switch (vibroComp.Vibration)
                    {
                        case VibrationEvent.VibrationType.HeavyImpact:
                            Lofelt.NiceVibrations.HapticPatterns.PlayPreset(HapticPatterns.PresetType.HeavyImpact);
                            //Debug.Log("сильная вибрация");
                            break;
                        case VibrationEvent.VibrationType.LightImpack:
                            Lofelt.NiceVibrations.HapticPatterns.PlayPreset(HapticPatterns.PresetType.LightImpact);
                            //Debug.Log("легкая вибрация");
                            break;
                        case VibrationEvent.VibrationType.MediumImpact:
                            Lofelt.NiceVibrations.HapticPatterns.PlayPreset(HapticPatterns.PresetType.MediumImpact);
                            //Debug.Log("средняя вибрация");
                            break;
                        case VibrationEvent.VibrationType.RigitImpact:
                            Lofelt.NiceVibrations.HapticPatterns.PlayPreset(HapticPatterns.PresetType.RigidImpact);
                            break;
                        case VibrationEvent.VibrationType.Selection:
                            Lofelt.NiceVibrations.HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                            break;
                        case VibrationEvent.VibrationType.SoftImpact:
                            Lofelt.NiceVibrations.HapticPatterns.PlayPreset(HapticPatterns.PresetType.SoftImpact);
                            break;
                        case VibrationEvent.VibrationType.Success:
                            Lofelt.NiceVibrations.HapticPatterns.PlayPreset(HapticPatterns.PresetType.Success);
                            break;
                        case VibrationEvent.VibrationType.Warning:
                            Lofelt.NiceVibrations.HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);
                            break;
                    }
                }
                _filter.Pools.Inc1.Del(entity);
            }
        }
    }
}