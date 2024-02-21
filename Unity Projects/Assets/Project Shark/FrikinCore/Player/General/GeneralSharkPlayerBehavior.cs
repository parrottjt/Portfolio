using FrikinCore.Player.Inventory;
using FrikinCore.Player.Managers;
using FrikinCore.Player.Weapons;
using FrikinCore.Sfx;
using FrikinCore.Stats;
using Sirenix.OdinInspector;
using TSCore.Time;
using UnityEngine;

namespace FrikinCore.Player
{
    /// GeneralSharkBehavior Explanation
    /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    /// This instance gathers information from the scripts that require inputs
    /// and sends the information to the GameManager for use in the other 
    /// scripts that require this information
    /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public class GeneralSharkPlayerBehavior : AbstractPlayerBehavior, IPause
    {
        [ReadOnly] [SerializeField] float speed;

        int _perfectDodgeTicks;

        void OnDestroy()
        {
            TickTimeTimer.OnTick -= OnTick;
            PauseManager.OnPause -= OnPause;
            PauseManager.OnUnpause -= OnUnpause;
            WeaponInventory.WeaponChange -= OnWeaponChange;
        }

        void Start()
        {
            TickTimeTimer.OnTick += OnTick;
            PauseManager.OnPause += OnPause;
            PauseManager.OnUnpause += OnUnpause;
            WeaponInventory.WeaponChange += OnWeaponChange;

            PlayerManager.instance.SetPlayer(Model);

            //PlayerManager.instance.WeaponManagement.UpdateWeaponIndex(0);
        }

        // Update is called once per frame
        void Update()
        {
            if(GameManager.IsInitialized) Model.IsDead = PlayerManager.instance.Player.IsDead;
            if (Model.GracePeriodActive) Model.GracePeriod.CheckToSeeIfRespawnEffectCanTurnOff();

            //This is just to see speed in inspector
            speed = Model.Speed;

            if (Model.PerfectDodge) PerfectDodge();

            Model.Poison.Run(Model.IsPoisoned);
        }

        void PerfectDodge()
        {
            UpdatedStatManager.instance.AddPerfectDodgeModifiers();
            if (_perfectDodgeTicks >= Model.PerfectDodgeTime)
            {
                UpdatedStatManager.instance.RemovePerfectDodgeModifiers();
                Model.PerfectDodge = false;
            }
        }

        #region Collisions

        void OnTriggerEnter2D(Collider2D collision)
        {
            #region Particle Systems

            if (collision.CompareTag("PickUp") &&
                PlayerManager.instance.HealthController.GetIsHealthFull() == false)
            {
                Model.AddHealth.Run();
            }

            if (collision.CompareTag("AmmoAll") &&
                PlayerManager.instance.WeaponManagement.CurrentWeapon.WeaponInfo.WeaponName != WeaponName.NormalLazer &&
                PlayerManager.instance.WeaponManagement.CurrentWeaponFull() == false)
            {
                Model.AddAmmo.Run();
            }

            if (collision.CompareTag("DamagingSeaweed") && Model.PrimaryUtilityActive == false) Model.IsPoisoned = true;

            #endregion
        }

        void OnTriggerStay2D(Collider2D c)
        {
            if (c.CompareTag("Slowing")) Model.IsSlowed = true;
            if (c.CompareTag("Current")) Model.InCurrent = true;
            if (c.CompareTag("DamagingSeaweed") && Model.PrimaryUtilityActive == false) Model.IsPoisoned = true;
        }

        void OnTriggerExit2D(Collider2D c)
        {
            if (c.CompareTag("Slowing")) Model.IsSlowed = false;
            if (c.CompareTag("Current")) Model.InCurrent = false;
            if (c.CompareTag("DamagingSeaweed")) Model.IsPoisoned = false;
        }

        #endregion

        #region Public Status Effect Functions

        public void Stun(float newTime)
        {
            // Use new way to do camera shake
            //camShake.shakeCamera = true;

            //SetNormSpeed(stopSpeed);
            //shooting.CancelBurstOnStun();
            Model.IsStunned = true;
            //stunTime = newTime;
            Model.Animator.SetBool(PlayerDataModel.IsStunnedHash, true);
            SoundManager.instance.RandomizeSfx(SoundManager.instance.EelStunnedSfx);
        }

        public void Freeze()
        {
            Model.IsSlowed = true;
            //freezeTime = 2f;
            Model.IsFrozen = true;
            Model.Freezing.Run();
            // frozenSoundTimer += Time.deltaTime;
            // if (frozenSoundTimer >= 1f)
            // {
            SoundManager.instance.RandomizeSfx(SoundManager.instance.FrozenSfx);
            // frozenSoundTimer = 0;
            // }
        }

        //Adding Oiled
        public void Oil(float oilTime)
        {
            //OiledSFX.Play();
            SoundManager.instance.RandomizeSfx(SoundManager.instance.SquidOiledSfx);
            Model.IsOiled = true;
            //oilTimer = oilTime;
            Model.Animator.SetBool(PlayerDataModel.IsOiledHash, true);
        }

        public void MiniOil(float miniOilTime)
        {
            Model.IsMiniOiled = true;
            //miniOilTimer = miniOilTime;
            Model.Animator.SetBool(PlayerDataModel.IsMiniOilHash, true);
        }

        #endregion

        void OnTick(object sender, TickTimeTimer.OnTickEventArgs args)
        {
            //Model.GracePeriod.SpawningInGracePeriod();

            #region Perfect Dodge

            if (Model.PerfectDodge == false) _perfectDodgeTicks = 0;
            if (Model.PerfectDodge) _perfectDodgeTicks += 1;

            #endregion
        }

        public void OnPause() => Model.Rigidbody.gravityScale = 0;

        public void OnUnpause()
        {
            if (Model.GracePeriodActive) return;
            Model.Rigidbody.gravityScale = Model.DefaultValues.GravityScale;
        }

        void OnWeaponChange() =>
            Model.WeaponHolderSpriteRenderer.sprite =
                PlayerManager.instance.WeaponManagement.CurrentWeapon.WeaponInfo.GameSprite;
    }
}