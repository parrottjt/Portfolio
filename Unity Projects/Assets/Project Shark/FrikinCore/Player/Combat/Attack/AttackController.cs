using FrikinCore.Player.Items.NewWeapon;
using FrikinCore.Player.Weapons;
using TSCore.Time;
using UnityEngine;

namespace FrikinCore.Player.Combat
{
    public abstract class AttackController : MonoBehaviour
    {
        readonly WeaponController _weaponController = new ();
        PlayerWeaponManagement _weaponManagement;

        NewWeaponInfo CurrentWeaponInformation => _weaponController.CurrentWeapon.WeaponInfo;
        
        protected void Start()
        {
            TickTimeTimer.OnTick += OnTick;

            _weaponManagement = PlayerManager.instance.WeaponManagement;
        }

        protected void OnDestroy() => TickTimeTimer.OnTick -= OnTick;

        protected abstract void OnTick(object sender, TickTimeTimer.OnTickEventArgs e);
    }
}
