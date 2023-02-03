using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Leopotam.EcsLite;
using Client;
using DG.Tweening;

public class SettingsPanelBehaviourMB : MonoBehaviour
{
    #region ECS
    private EcsWorld _world;
    private GameState _state;
    private EcsPool<CreditsEvent> _creditsPool;
    private EcsPool<ClickEvent> _clickPool;
    

    public void Init(EcsWorld world, GameState state)
    {
        world = state.EcsWorld;
        _world = world;
        _state = state;
        _creditsPool = _world.GetPool<CreditsEvent>();
        _clickPool = _world.GetPool<ClickEvent>();
    }
    #endregion

        [SerializeField] private float _speed;
        private Vector2 _startPos;
        [SerializeField] private Vector2 _activePos;
        [SerializeField] private RectTransform _thisPanel;
        private bool _wasClicked = false;
        [SerializeField] private Image _musicButton;
        [SerializeField] private Image _vibroButton;
        [SerializeField] private Image _soundsButton;
        [SerializeField] private Sprite _musicOnImage;
        [SerializeField] private Sprite _musicOffImage;
        [SerializeField] private Sprite _vibroOnImage;
        [SerializeField] private Sprite _vibroOffImage;
        [SerializeField] private Sprite _soundOnImage;
        [SerializeField] private Sprite _soundOffImage;
        [SerializeField] private AudioBehaviourMB _audioBehaviour;
        [SerializeField] private GameObject _creditsPanel;

        void Awake()
        {
            _startPos = new Vector2 (_thisPanel.anchoredPosition.x, _thisPanel.anchoredPosition.y);            
        }

        public void OpenCredits()
        {
            _creditsPool.Add(_world.NewEntity());
            PlayClickSound();
        }

        public void OpenFromSystem()
        {
            _creditsPanel.SetActive(true);
        }

        public void CloseCredits()
        {
            _creditsPanel.SetActive(false);
            PlayClickSound();
        }

        public void MoveSettingsPanel()
        {
            PlayClickSound();
            //LightVibration();            
            if(!_wasClicked)
            {
                _thisPanel.DOAnchorPosX(_activePos.x, _speed, false);
                _wasClicked = true;
            }
            else
            {
                _thisPanel.DOAnchorPosX(_startPos.x, _speed, false);
                _wasClicked = false;
            }
        }

        public void ChangeVibration()
        {
            PlayClickSound();
            //LightVibration();
            switch (_state.Vibration)
            {
                case true:
                    _state.Vibration = false;
                    break;
                case false:
                    _state.Vibration = true;
                    break;
            }
            _vibroButton.sprite = GetVibroButtonSprite(_state.Vibration);
        }

        public void ChangeMusic()
        {
            PlayClickSound();
            //LightVibration();
            switch (_state.Music)
            {
                case true:
                    _state.Music = false;
                    _audioBehaviour.OffMusicVol();
                    break;
                case false:
                    _state.Music = true;
                    _audioBehaviour.OnMusicVol();
                    break;
            }
            _musicButton.sprite = GetMusicButtonSprite(_state.Music);
        }

        public void ChangeSound()
        {
            PlayClickSound();
            //LightVibration();
            switch (_state.Sounds)
            {
                case true:
                    _state.Sounds = false;
                    _audioBehaviour.OffSoundsVol();
                    break;
                case false:
                    _state.Sounds = true;
                    _audioBehaviour.OnSoundsVol();
                    break;
            }
            _soundsButton.sprite = GetSoundsButtonSprite(_state.Sounds);
        }

        public void GetOnStartButtonSprite()
        {
            _vibroButton.sprite = GetVibroButtonSprite(_state.Vibration);
            _soundsButton.sprite = GetSoundsButtonSprite(_state.Sounds);
            _musicButton.sprite = GetMusicButtonSprite(_state.Music);
        }
        private Sprite GetSoundsButtonSprite(bool value)
        {
            Sprite sprite = null;
            switch(value)
            {
                case true:
                    sprite = _soundOnImage;
                    break;
                case false:
                    sprite = _soundOffImage;
                    break;
            }
            return sprite;
        }

        private Sprite GetMusicButtonSprite(bool value)
        {
            Sprite sprite = null;
            switch(value)
            {
                case true:
                    sprite = _musicOnImage;
                    break;
                case false:
                    sprite = _musicOffImage;
                    break;
            }
            return sprite;
        }

        private Sprite GetVibroButtonSprite(bool value)
        {
            Sprite sprite = null;
            switch(value)
            {
                case true:
                    sprite = _vibroOnImage;
                    break;
                case false:
                    sprite = _vibroOffImage;
                    break;
            }
            return sprite;
        }
/*
        private void LightVibration()
        {
            if(!_vibrationEvent.Has(_state.VibrationEntity))
            {
                ref var vibroComp = ref _vibrationEvent.Add(_state.VibrationEntity);
                vibroComp.Vibration = VibrationEvent.VibrationType.LightImpack;
            }
        }
*/
        private void PlayClickSound()
        {
            _clickPool.Add(_world.NewEntity());
        }
}
