using FrikinCore.Player.Inventory;
using FrikinCore.Player.Items.NewWeapon;
using FrikinCore.Save;
using TSCore.Time;
using TSCore.Utils;
using UnityEngine;

namespace FrikinCore.Player.Weapons
{
    public class PlayerWeaponManagement : MonoBehaviour
    {
        AmmoHandler _ammoHandler;
        WeaponInventory _weaponInventory;
        
        [SerializeField] float passiveReloadDelay;
        [SerializeField] float passiveReloadModifier;

        public Weapon CurrentWeapon => _weaponInventory.CurrentWeapon;
        public int CurrentWeaponAmmo => _ammoHandler.CurrentAmmoAmountOfWeapon(CurrentWeapon);
        public int CurrentWeaponMaxAmmo => _ammoHandler.MaxAmmoAmountOfWeapon(CurrentWeapon);
        public bool CurrentWeaponFull() => _ammoHandler.IsAmmoTypeFull(CurrentWeapon);

        void Awake() => _ammoHandler = new AmmoHandler(passiveReloadDelay, passiveReloadModifier);

        void Start()
        {
            TickTimeTimer.OnTick += OnTick;
            PersistentDataManager.OnPresetChange += OnPresetChange;
        }

        void OnDestroy()
        {
            TickTimeTimer.OnTick -= OnTick;
            PersistentDataManager.OnPresetChange -= OnPresetChange;
            
            if (PlayerManager.IsInitialized) _weaponInventory = PlayerManager.instance.Inventory.Weapon;
        }

        void Update()
        {
            //todo: This need to be moved into the Attack Controller on the player which handles weapon inputs
            if(_weaponInventory.IsNotNull()) _weaponInventory.WeaponScroll();
        }

        #region Updating/Increasing/Reloading Ammo

        public bool AreWeaponsFull() => _ammoHandler.AllWeaponsFull();

        public void WeaponReload(AmmoHandler.ReloadAmmoAmount ammoType)
        {
            _ammoHandler.Reload(CurrentWeapon, ammoType);
        }

        public void DecreaseCurrentWeaponAmmo(int amount = 1)
        {
            _ammoHandler.DecreaseAmmo(CurrentWeapon, amount);
        }

        #endregion

        void OnTick(object sender, TickTimeTimer.OnTickEventArgs onTickEventArgs)
        {
            _ammoHandler.OnTick();
        }

        void OnPresetChange()
        {
            
        }
    }
}