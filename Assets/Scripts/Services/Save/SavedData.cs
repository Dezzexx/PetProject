using UnityEngine;

namespace Saver
{
    public class SavedData
    {
        static int _version = 1;

        public static int Version
        {
            get
            {
                return _version;
            }
        }

        // -------------------------------------

        public bool Sounds = true;
        public bool Music = true;
        public bool Vibration = true;
        public bool Tutorial = true;

        // -------------------------------------
        public int SceneNumber = 1;
        public int Level = 0;
        public int CurrentBiome = 1; // текущий биом

        // --------------------------------------

        public int PlayerResourceValue = 0;        //сколько денег у игрока
        public int PlayerLevelScore = 1;        //какого уровня игрок
        public int PlayerGrenade = 1;           //сколько гранат у игрока
        public int PlayerPaintScore;        //сколько краски у игрока
        public int FullGroupValue = 9;          //сколько всего в отряде может быть сейчас юнитов
        public int CurrentGroupValue;       //сколько юнитов в отряде на данный момент
        public int PlayerWeaponLevel = 1;       // уровень оружия у игрока
        public int WeaponUpdatePrice = 75;
        public int LevelUpdatePrice = 75;
        public int LevelProgressIndex = 1;
        

    }
}
