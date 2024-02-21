using FrikinCore.DevelopmentTools;
using FrikinCore.Enumeration;
using FrikinCore.Input;
using FrikinCore.Player.Items.NewWeapon;
using FrikinCore.Player.Weapons;
using FrikinCore.Sfx;
using FrikinCore.Stats;
using TSCore.Time;
using TSCore.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FrikinCore.Player.Combat
{
    public class FiringController : AbstractPlayerBehavior
    {
        PlayerWeaponManagement laser;
        readonly WeaponController weaponController = new ();

        [SerializeField] Transform[] laserPositions;
        [SerializeField] LineRenderer taserLineRenderer;

        [SerializeField] GameObject taserSpriteHolder;

        bool emptySoundPlay;
        bool shadowShot;
        float fireTime;
        float fireTimer;
        float chargeTimer;

        //Ticks
        int emptySoundTick;

        NewWeaponInfo CurrentWeaponInformation => weaponController.CurrentWeapon.WeaponInfo;

        //GameObject WeaponModeProjectile => weaponController.WeaponModeProjectile;
        //GameObject WeaponModeShadowProjectile => weaponController.WeaponModeShadowProjectile;

        public bool ReloadCheck() => fireTimer < fireTime;
        public float GetFireTimer => fireTimer;
        //public float GetChargeTimer => weaponController.CurrentWeapon.GetChargeTimer();
        public float GetChargeTimer => 0;

        public PlayerWeapons.FireFunctionType FireType => CurrentWeaponInformation.AttackType;

        public void DecreaseAmmoByOne() => laser.DecreaseCurrentWeaponAmmo();
        public void ResetFireTimeToZero() => fireTimer = 0;

        bool GeneralFireCheck() =>
            Model.PrimaryUtilityActive == false && Model.IsDead == false && 
            !GameManager.GameSettings[Settings.PauseMenu];

        bool FireTypeConditions()
        {
            switch (FireType)
            {
                case PlayerWeapons.FireFunctionType.Click:
                    return fireTimer >= fireTime && laser.CurrentWeaponAmmo > 0;
                //case PlayerWeapons.FireFunctionType.Charge:
                    //return chargeTimer >= CurrentWeaponInformation.ChargeTime;
                case PlayerWeapons.FireFunctionType.Hold:
                    return laser.CurrentWeaponAmmo > 0;
                default:
                    return false;
            }
        }

        bool CanThePlayerFire() => GeneralFireCheck() && FireTypeConditions();

        void Start()
        {
            TickTimeTimer.OnTick += OnTick;
            laser = PlayerManager.instance.WeaponManagement;
            weaponController.projectileSpawns = laserPositions;
            //weaponController.ChangeWeapon(WeaponList.WeaponDictionary[laser.CurrentWeapon.WeaponName].weapon_Function);

            //Todo line below needs to be changed to Dev Preset
            //weaponController.CurrentWeapon.UnlockWeaponLevel(BaseWeapon.WeaponLevel.Upgraded);
        }

        void Update()
        {
            fireTimer += Time.deltaTime;
            GetVariableInformationForCurrentWeapon();
            taserSpriteHolder.SetActive(laser.CurrentWeapon.WeaponName == WeaponName.TaserLazer);
            //weaponController.ChangeWeapon(WeaponList.WeaponDictionary[laser.CurrentWeapon.WeaponName].weapon_Function);
            WeaponModeKeyPress();
            //FireMode(WeaponModeProjectile);
            //if (shadowShot) ShadowShot(WeaponModeShadowProjectile);
        }

        void WeaponModeKeyPress()
        {
            if (InputManager.instance.WeaponModeChange())
            {
                //weaponController.CurrentWeapon.WeaponModeSetToUpgrade.Invert();
            }
        }

        void FireMode(GameObject projectile)
        {
            switch (FireType)
            {
                case PlayerWeapons.FireFunctionType.Click:
                    ClickFire(projectile);
                    break;
                case PlayerWeapons.FireFunctionType.Charge:
                    ChargeFire(projectile);
                    break;
                case PlayerWeapons.FireFunctionType.Hold:
                    HoldFire(projectile);
                    break;
            }
        }

        void ClickFire(GameObject projectile)
        {
            weaponController.ExecuteFunctionality(CanThePlayerFire() && InputManager.instance.AttackInput(),
                projectile, out shadowShot, taserLineRenderer);
        }

        void ChargeFire(GameObject projectile)
        {
            if (InputManager.instance.AttackInput())
            {
                chargeTimer += Time.deltaTime;
            }

            if (!InputManager.instance.AttackInputRelease()) return;
            weaponController.ExecuteFunctionality(CanThePlayerFire(),
                projectile, out shadowShot, taserLineRenderer);
            chargeTimer = 0;
        }

        void HoldFire(GameObject projectile)
        {
            weaponController.ExecuteFunctionality(CanThePlayerFire() && InputManager.instance.AttackInput(),
                projectile, out shadowShot, taserLineRenderer);
        }

        void ShadowShot(GameObject projectile)
        {
            FireMode(projectile);
            shadowShot = false;
        }

        void OnDestroy()
        {
            TickTimeTimer.OnTick -= OnTick;
        }

        void GetVariableInformationForCurrentWeapon()
        {
            //fireTime = UpdatedStatManager.GetStat(GameEnums.PermanentStats.ReloadCooldownAdjust).GetStatValue(laser.CurrentWeapon.FireTime);

            if (laser.CurrentWeaponAmmo <= 0)
            {
                if (emptySoundPlay)
                {
                    SoundManager.instance.RandomizeSfx(!DevTools.devToolsDictionary[DevTool.KawaiiMode]
                        ? SoundManager.instance.NoAmmoSfx
                        : SoundManager.instance.KawaiiEmptySfx);
                    emptySoundPlay = false;
                }
            }
        }

        void OnTick(object sender, TickTimeTimer.OnTickEventArgs args)
        {
            laserPositions[5].localRotation = Quaternion.Euler(0f, 0f, Random.Range(-7.5f, 7.5f));
            //Empty Ammo Sound Play
            if (!emptySoundPlay)
            {
                emptySoundTick++;
                if (emptySoundTick >= 12)
                {
                    emptySoundTick = 0;
                    emptySoundPlay = true;
                }
            }
        }
    }
}