using FrikinCore.Player;
using FrikinCore.Player.Weapons;
using FrikinCore.Sfx;
using UnityEngine;

namespace FrikinCore.Collectable
{
    public class CollectableAmmo : Collectable
    {
        [SerializeField] AmmoHandler.ReloadAmmoAmount ammoType;

        protected override void OnPickUp()
        {
            PlayerManager.instance.WeaponManagement.WeaponReload(ammoType);
            SoundManager.instance.RandomizeSfx(SoundManager.instance.AmmoPickUpSfx);
            if (PlayerManager.instance.WeaponManagement.AreWeaponsFull())
            {
                AddScore();
            }
        }
    }
}
