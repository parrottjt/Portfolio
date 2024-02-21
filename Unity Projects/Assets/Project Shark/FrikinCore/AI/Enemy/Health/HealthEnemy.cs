using System;
using System.Collections.Generic;
using FrikinCore.Enumeration;
using FrikinCore.Loot;
using FrikinCore.Player;
using FrikinCore.Player.Health;
using FrikinCore.Save;
using FrikinCore.Utils;
using UnityEngine;

namespace FrikinCore.AI.Enemy.Health
{
    public class HealthEnemy : HealthAbstract
    {
        readonly Dictionary<GameEnums.TypeOfEnemy, float> enemyHealths = new Dictionary<GameEnums.TypeOfEnemy, float>
        {
            { GameEnums.TypeOfEnemy.OrangeFish, 1f },
            { GameEnums.TypeOfEnemy.Crab, 5f },
            { GameEnums.TypeOfEnemy.CrabVarient, 5f },
            { GameEnums.TypeOfEnemy.NinjaStarFish, 5f },
            { GameEnums.TypeOfEnemy.MasterNinjaStarFish, 6f },
            { GameEnums.TypeOfEnemy.Octopus, 12f },
            { GameEnums.TypeOfEnemy.OctopusVarient, 14f },
            { GameEnums.TypeOfEnemy.Skellyfish, 3f },
            { GameEnums.TypeOfEnemy.SkellySniper, 3f },

            { GameEnums.TypeOfEnemy.Pufferfish, 7.5f },
            { GameEnums.TypeOfEnemy.VariantPufferfish, 9f },
            { GameEnums.TypeOfEnemy.Squid, 8f },
            { GameEnums.TypeOfEnemy.SmallSquid, 6f },
            { GameEnums.TypeOfEnemy.Snail, 6f },
            { GameEnums.TypeOfEnemy.SnailSniper, 5f },
            { GameEnums.TypeOfEnemy.Penguin, 10f },

            { GameEnums.TypeOfEnemy.GhostShotty, 20f },
            { GameEnums.TypeOfEnemy.GhostGassy, 20f },
            { GameEnums.TypeOfEnemy.Raptor, 12f },
            { GameEnums.TypeOfEnemy.VariantRaptor, 12f },

            { GameEnums.TypeOfEnemy.ParatrooperShrimp, 9f },
            { GameEnums.TypeOfEnemy.SwordAndBoard, 15f },
            { GameEnums.TypeOfEnemy.MineJelly, 12f },
            { GameEnums.TypeOfEnemy.MineJellySpawner, 12f },
            { GameEnums.TypeOfEnemy.MineJellyGassyBubble, 12f },

            { GameEnums.TypeOfEnemy.PopOutEel, 8f },
            { GameEnums.TypeOfEnemy.Stingray, 11f },
            { GameEnums.TypeOfEnemy.RamboRay, 12f },

            { GameEnums.TypeOfEnemy.Lionfish, 27f },
            { GameEnums.TypeOfEnemy.Turtle, 13f },
            { GameEnums.TypeOfEnemy.Pirate, 11f },
            { GameEnums.TypeOfEnemy.PirateCannonier, 13f },

            { GameEnums.TypeOfEnemy.BuffMuscle, 29f },
            { GameEnums.TypeOfEnemy.BuffMuscleVariant, 31f },
            { GameEnums.TypeOfEnemy.Surgeonfish, 15f },
            { GameEnums.TypeOfEnemy.SurgeonfishVariant, 15f },
            { GameEnums.TypeOfEnemy.Diver, 16f },

            { GameEnums.TypeOfEnemy.MissileSpawner, 10f },
            //Missile 1f
        };

        public bool IsDead { get; protected set; }
        [SerializeField] DataEnemy dataEnemy;
        Action _pawnDeath;

        LootSystem lootSystem;

        float WorldHealthChecker =>
            GameManager.instance.WorldNumber - (float)dataEnemy.WorldIntroducedIn >= 0
                ? GameManager.instance.WorldNumber - (float)dataEnemy.WorldIntroducedIn
                : 0;

        float EnemyMaxHealthCalculation =>
            (enemyHealths[dataEnemy.typeOfEnemy] +
             (enemyHealths[dataEnemy.typeOfEnemy] * WorldHealthChecker * .25f)) *
            10f;

        // Use this for initialization
        void Start()
        {
            SetMaxHealth(EnemyMaxHealthCalculation, 1);
            FullyHeal();
            lootSystem = dataEnemy.GetCurrentLootSystemSelected();
        }

        void OnDestroy()
        {
            if (IsDead) PlayerHealthController.CallOnPlayerDeath -= PlayerDeath;
        }

        public override void TakeDamage(float amount, bool cutThroughArmor = false)
        {
            base.TakeDamage(amount, cutThroughArmor);
            if (Health <= 0)
            {
                Invoke(nameof(Killed), 0f);
                PlayerHealthController.CallOnPlayerDeath += PlayerDeath;
            }
        }

        public virtual void Killed()
        {
            IsDead = true;

            _pawnDeath?.Invoke();
            DropItem(lootSystem);

            if (PlayerManager.instance.HealthController.IsPlayerHealthLow())
                GameManager.instance.loot.EnemiesKilledAtLowHealth++;

            SetComponentsEnableTo(false);

            GameManager.instance.loot.GenocideCheck();

            RunAchievementChecks();
        }

        public void Respawn()
        {
            transform.position = dataEnemy.PawnStartPosition;

            SetComponentsEnableTo(true);

            PlayerHealthController.CallOnPlayerDeath -= PlayerDeath;
        }

        public void DeclarePawnDeathFunctionality(Action pawnDeath)
        {
            _pawnDeath = pawnDeath;
        }

        void PlayerDeath(object sender, PlayerHealthController.PlayerDeathArgs args)
        {
            Invoke(nameof(Respawn), 2.5f);
        }

        protected void DropItem(LootSystem currLootProbability)
        {
            var itemDrop = UpdatedEnemyLootDrop.FindItemToDrop(currLootProbability.duckRarity,
                currLootProbability.pHDuckRarity, currLootProbability.superDuckRarity, currLootProbability.toothRarity,
                currLootProbability.teethRarity, currLootProbability.ammoFullSingleRarity,
                currLootProbability.ammoQuarterAllRarity);
            if (itemDrop == null) return;
            if (itemDrop.name == "SuperDuck_EnemyDrop(Clone)")
            {
                GameManager.instance.loot.EnemiesKilledAtLowHealth = 0;
                GameManager.instance.loot.SuperDuckHasDropped = true;
            }

            Instantiate(itemDrop, transform.position, Quaternion.identity);

            // var item = ObjectPooling.GetAvailableObject(itemDrop);
            // item.transform.position = transform.position;
            // item.SetActive(true);
        }

        protected void SetComponentsEnableTo(bool value)
        {
            var collider2Ds = dataEnemy.pawnHolder.GetComponentsInChildren<Collider2D>();
            var sprites = dataEnemy.pawnHolder.GetComponentsInChildren<SpriteRenderer>();
            var lineRenderers = dataEnemy.pawnHolder.GetComponentsInChildren<LineRenderer>();

            Handlers.SetEnableOnBehaviorComponentTo(value,
                dataEnemy.Movement, dataEnemy.EnemyFunctionality);
            Handlers.SetEnableOnBehaviorArrayComponentTo(value,
                collider2Ds);
            Handlers.SetEnableOnRendererArrayComponentTo(value,
                sprites, lineRenderers);
            if (dataEnemy.Animator) dataEnemy.Animator.enabled = value;
        }

        //The weapon kills system won't always work, if the player changes weapons it won't work or can work
        // with of weapon kills
        protected void RunAchievementChecks()
        {
            PersistentDataManager.DataIntArrayDictionary[PersistentDataManager.DataIntArrays.EnemyDeathCounts][
                (int)dataEnemy.typeOfEnemy]++;

            if (PersistentDataManager.DataBoolArrayDictionary[
                    PersistentDataManager.DataBoolArrays.TypesOfEnemiesThatHaveBeenDefeated][
                    (int)dataEnemy.typeOfEnemy] == false)
            {
                PersistentDataManager.DataBoolArrayDictionary[
                    PersistentDataManager.DataBoolArrays.TypesOfEnemiesThatHaveBeenDefeated][
                    (int)dataEnemy.typeOfEnemy] = true;
            }

            //All of this underneath has to deal with achievements and they need to be properly setup

            // if (PlayerDataManager.instance.deadIndex < PlayerDataManager.instance.hasEnemyDied.Length)
            // {
            //     PlayerDataManager.instance.achievementUnlockArray[(int) Achievements.ACH_KILL_EACH_ENEMY_TYPE] = false;
            // }
            // else PlayerDataManager.instance.achievementUnlockArray[(int) Achievements.ACH_KILL_EACH_ENEMY_TYPE] = true;
            //
            // if (!PlayerDataManager.instance.achievementUnlockArray[(int) Achievements.ACH_KILL_100_ENEMIES_WITH_MINI_BERTHA]
            // )
            // {
            //     if (PlayerManager.instance.WeaponManagement.CurrentWeapon.weaponName == WeaponNames.BakaCannon)
            //         PlayerDataManager.instance.miniBerthaKills++;
            // }
            //
            // if (!PlayerDataManager.instance.achievementUnlockArray[(int) Achievements.ACH_KILL_100_ENEMIES_FLAMETHROWER])
            // {
            //     if (PlayerManager.instance.WeaponManagement.CurrentWeapon.weaponName == WeaponNames.FlameThrower)
            //         PlayerDataManager.instance.flamethrowerKills++;
            // }
            //
            // if (!PlayerDataManager.instance.achievementUnlockArray[(int) Achievements.ACH_KILL_100_ENEMIES_WITH_POO_PEW])
            // {
            //     if (PlayerManager.instance.WeaponManagement.CurrentWeapon.weaponName == WeaponNames.PeaShooter)
            //         PlayerDataManager.instance.pooPewKills++;
            // }
            //
            // if (!PlayerDataManager.instance.achievementUnlockArray[
            //     (int) Achievements.ACH_KILL_100_ENEMIES_WITH_SHARK_REPELLENT])
            // {
            //     if (PlayerManager.instance.WeaponManagement.CurrentWeapon.weaponName == WeaponNames.SharkRepellent)
            //         PlayerDataManager.instance.sharkRepellentKills++;
            // }
            //
            // if (!PlayerDataManager.instance.achievementUnlockArray[(int) Achievements.ACH_KILL_100_ENEMIES_WITH_TEETH_GUN])
            // {
            //     if (PlayerManager.instance.WeaponManagement.CurrentWeapon.weaponName == WeaponNames.TeethGun)
            //         PlayerDataManager.instance.teethGunKills++;
            // }
            //
            // if (!PlayerDataManager.instance.achievementUnlockArray[
            //     (int) Achievements.ACH_KILL_3_ENEMIES_WITH_1_SHOTGUN_BLAST])
            // {
            //     if (PlayerManager.instance.WeaponManagement.CurrentWeapon.weaponName == WeaponNames.ShotgunLazer)
            //     {
            //         if (GameManager.instance.playerCode.ReloadCheck())
            //         {
            //             PlayerDataManager.instance.shottyTripleKillsCounter++;
            //             if (PlayerDataManager.instance.shottyTripleKillsCounter >= 3)
            //                 PlayerDataManager.instance.achievementUnlockArray[
            //                     (int) Achievements.ACH_KILL_3_ENEMIES_WITH_1_SHOTGUN_BLAST] = true;
            //         }
            //         else PlayerDataManager.instance.shottyTripleKillsCounter = 0;
            //     }
            // }
        }
    }
}