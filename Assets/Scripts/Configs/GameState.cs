using UnityEngine;
using Leopotam.EcsLite;
using Saver;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Client
{
#if UNITY_EDITOR
    [System.Serializable]
#endif
    public class GameState
    {
        private static GameState _gameState = null;

        public EcsSystems EcsSystems;
        public EcsWorld EcsWorld;

        public GameMode GameMode;

#region Input;
        public GraphicRaycaster Raycaster;
        public EventSystem EventSystem;
#endregion

#region Mask
        public LayerMask DeformableMask;
#endregion

#region Configs
        public SoundConfig SoundConfig;
        public GameConfig GameConfig;
        public DeformableObjectsConfig DeformableObjectsConfig;
#endregion

        public AllPools AllPools;
        public AllPools ActivePools;

        public bool Sounds = true;
        public bool Music = true;
        public bool Vibration = true;

        public int PlayerResourceValue;
        public int SceneNumber;
#region entity
        public bool Tutorial;               
        public int InterfaceEntity;
        public int InputEntity;          
        public int SoundsEntity;          
        public int VibrationEntity;                 
        public int CameraEntity;
        public int NavMeshSurfaceEntity;
        public int ClipperEntity;
        public int UnitsHolderEntity;
        public const int NULL_ENTITY = -1;

#endregion
        public int LevelProgressIndex;  //индекс уровня, если уровни пошли по 2 кругу

        private GameState(in EcsStartup ecsStartup)
        {
            EcsWorld = ecsStartup.World;
            AllPools = ecsStartup.AllPools;
            SoundConfig = ecsStartup.SoundConfig;
            GameConfig = ecsStartup.GameConfig;
            DeformableObjectsConfig = ecsStartup.DeformableObjectsConfig;

            DeformableMask = ecsStartup.DeformableMask; 

            Load();
        }

        public static void Clear()
        {
            _gameState = null;
        }

        public static GameState Initialize(in EcsStartup ecsStartup)
        {
            if (_gameState is null)
            {
                _gameState = new GameState(in ecsStartup);
            }

            return _gameState;
        }

        public static GameState Get()
        {
            return _gameState;
        }

        #region Save
        public void Load()
        {
            var saver = new JsonSaver();
            SavedData data = saver.Load();

            Sounds = data.Sounds;
            Music = data.Music;
            Vibration = data.Vibration;
            PlayerResourceValue = data.PlayerResourceValue;
            SceneNumber = data.SceneNumber;
            Tutorial = data.Tutorial;
            LevelProgressIndex = data.LevelProgressIndex;
        }

        public void Save()
        {
            SavedData data = new SavedData();
            data.Sounds = Sounds;
            data.Music = Music;
            data.Vibration = Vibration;
            data.PlayerResourceValue = PlayerResourceValue;
            data.SceneNumber = SceneNumber;
            data.Tutorial = Tutorial;
            data.LevelProgressIndex = LevelProgressIndex;
            

            var saver = new JsonSaver();
            saver.Save(data);
            Debug.Log("Save");
        }
        #endregion Save

    }

    public enum GameMode { beforePlay, play, win, lose }
}