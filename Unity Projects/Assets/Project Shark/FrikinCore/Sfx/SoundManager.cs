using System.Collections.Generic;
using TSCore.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FrikinCore.Sfx
{
    public enum BossMusic
    {
        None = -1,
        Octoboss = 0,
        PuffDaddy,
        She_Rex,
        BionicPrawn,
        Eel,
        PirateShip,
        Killy,
        SharkFamily,
        Frakin,
        Miniboss
    }

    public class SoundManager : Singleton<SoundManager>
    {
        public AudioSource[] sfx;
        public AudioSource bgm;

        public float lowPitchRange = .95f, highPitchRange = 1.05f;
        float randomPitch;
        [Header("Laser SFX Clips")] public SoundClip NoAmmoSfx;

        public SoundClip RedLaserSfx,
            ShotgunSfx,
            ChargeLaserSfx,
            ChargingChargeSfx,
            FullyChargedShotSfx,
            HalfChargedShotSfx,
            NotChargedShotSfx,
            BurstShotSfx;

        [Header("Damage Related Clips")] public SoundClip EnemyDamagedSfx;

        public SoundClip PlayerDamagedSfx,
            PlayerDamaged2Sfx,
            StaticDamageSfx,
            EnemyShieldedSfx,
            DeathSoundSfx,
            EnemyDeathExplosionSfx;

        [Header("KAWAII Clips")] public SoundClip KawaiiEnemyDamagedSfx;
        public SoundClip KawaiiEmptySfx, KawaiiFunSfx;
        [Header("Respawn Checkpoint Clips")] public SoundClip CheckpointSignSfx;
        public SoundClip CheckpointSpraySfx;
        [Header("Collectable Clips")] public SoundClip DuckSfx;
        public SoundClip AmmoPickUpSfx, ExtraLifeSfx, FourHeartsCollectedSfx, HeartCollectedSfx, TeethSfx;
        [Header("Player Movement Clips")] public SoundClip DashSfx;
        public SoundClip ScrunchSfx, EelStunnedSfx, FrozenSfx;
        [Header("Menu SFX Clips")] public SoundClip MainMenuButtonsSfx;
        public SoundClip PauseMenuButtonsSfx, ButtonClickSfx;
        [Header("Environmental Hazard Clips")] public SoundClip GearSfx;

        public SoundClip GearDoorSfx,
            BigBerthaChargingSfx,
            SquidOiledSfx,
            SpikePoofSfx,
            BombSfx,
            CannonSfx,
            DoorSlamsSfx,
            BreakableObjectsSfx,
            FallingRoomSfx;

        [Header("Standard Enemy Clips")] public SoundClip GhostLaughSfx;
        [Header("Bosses Clips")] public SoundClip SheRexOuchSfx;

        public SoundClip SheRexBombSfx,
            SheRexShieldSfx,
            SheRexTeslaSfx,
            SheRexRocketSfx,
            PirateShipCannon,
            PirateShipMine;

        [Header("Background Music")] public SoundClip menu_MainTheme_Bgm;

        public SoundClip menu_LevelSelect_Bgm,
            menu_LevelComplete_Bgm,
            menu_Credits_Bgm,
            other_MiniBoss_Bgm,
            other_Fraken_Bgm,
            other_ToothGoblinShark_Bgm,
            other_HordeMode_Bgm,
            world1_OpenOcean_Bgm,
            boss1_OctoBoss_Bgm,
            world2_PuffDaddysCoolCoolSpot_Bgm,
            boss2_PuffDaddyBossTheme_Bgm,
            world3_AncientCity_Bgm,
            boss3_SheRex_Bgm,
            world4_UnderwaterSuperCity_Bgm,
            boss4_BionicPrawn_Bgm,
            world5_CaveTheme_Bgm,
            boss5_EelectricBoogaloo_Bgm,
            world6_ShipGraveyard_Bgm,
            boss6_PirateShipBoss_Bgm,
            world7_KillysTsundereWorld_Bgm,
            boss7_KillyBossTheme_Bgm,
            world8_VolcanoLab_Bgm,
            boss8_SharkFamily_Bgm;

        public static List<SoundClip> _worldBgmClips;
        public static List<SoundClip> _otherBgmClips;
        public static List<SoundClip> _bossBgmClips;

        public void Start()
        {
            if (GameManager.GameStatesDictionary[GameStates.Menu]) PlayBackgroundMusic();
        }

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);

            _worldBgmClips = new List<SoundClip>
            {
                world1_OpenOcean_Bgm,
                world2_PuffDaddysCoolCoolSpot_Bgm,
                world3_AncientCity_Bgm,
                world4_UnderwaterSuperCity_Bgm,
                world5_CaveTheme_Bgm,
                world6_ShipGraveyard_Bgm,
                world7_KillysTsundereWorld_Bgm,
                world8_VolcanoLab_Bgm,
            };
            _otherBgmClips = new List<SoundClip>
            {
                other_ToothGoblinShark_Bgm,
                other_HordeMode_Bgm
            };
            _bossBgmClips = new List<SoundClip>
            {
                boss1_OctoBoss_Bgm,
                boss2_PuffDaddyBossTheme_Bgm,
                boss3_SheRex_Bgm,
                boss4_BionicPrawn_Bgm,
                boss5_EelectricBoogaloo_Bgm,
                boss6_PirateShipBoss_Bgm,
                boss7_KillyBossTheme_Bgm,
                boss8_SharkFamily_Bgm,
                other_Fraken_Bgm,
                other_MiniBoss_Bgm,
            };
        }

        public void PlayBackgroundMusic(AudioClip bgmClipToBePlayed = null)
        {
            var clip = bgmClipToBePlayed == null ? SetClip() : bgmClipToBePlayed;

            if (clip == null)
            {
                print($"{GameManager.gameState.ToString()}, World Number: {GameManager.instance.WorldNumber}" +
                      $"doesn't have a music clip to play; STOPING MUSIC");
                bgm.Stop();
                return;
            }

            bgm.clip = clip;

            bgm.Play();
        }

        AudioClip SetClip()
        {
            switch (GameManager.gameState)
            {
                case GameStates.Menu:
                    return menu_MainTheme_Bgm.clip;

                case GameStates.Story:
                    if (GameManager.instance == null)
                    {
                        print("Gamemanager isn't instanced: Check the Level Info Dictionary");
                        return null;
                    }

                    return _worldBgmClips[GameManager.instance.WorldNumber].clip;

                case GameStates.ProGen:
                    return null;

                case GameStates.BossRush:
                    return _bossBgmClips[GameManager.instance.BossMusicNumber].clip;
                default:
                    return null;
            }
        }

        public void RandomizeSfx(params SoundClip[] clips)
        {
            int randomIndex = Random.Range(0, clips.Length);
            int sfxIndex = 0;

            if (clips[randomIndex].pitchable)
            {
                randomPitch = Random.Range(clips[randomIndex].pitchRangeMin, clips[randomIndex].pitchRangeMax);
            }
            else randomPitch = 1;

            sfx[sfxIndex].pitch = randomPitch;
            sfx[sfxIndex].volume = clips[randomIndex].volume;
            sfx[sfxIndex].clip = clips[randomIndex].clip;
            sfx[sfxIndex].PlayOneShot(clips[randomIndex].clip);
        }
    }
}