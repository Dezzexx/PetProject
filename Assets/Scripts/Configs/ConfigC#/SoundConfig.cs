using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SoundsConfig", menuName = "Configs/SoundsConfig", order = 2)]
public class SoundConfig : ScriptableObject
{
    public AudioClip LoseSound;
    public AudioClip WinSound;
    public AudioClip ClickSound;
    public AudioClip BuySound;
    public AudioClip FailClickSound;
    
    [Header("AttackSoundClip")]
    public AudioClip[] AttackSounds;
    public AudioClip MashinGunSound;
    [Header("Enemy Sounds")]
    public AudioClip EnemySoldierShoot;
    public AudioClip EnemyBomberShoot;
    public AudioClip EnemySniperShoot;
    public AudioClip EnemyHeliShoot;
    public AudioClip EnemyTankShoot;
    public AudioClip AGSGunSound;
    public AudioClip BigGunSound;
    public AudioClip BigGunSound2;
    public AudioClip AGSGranadeSound;


}

