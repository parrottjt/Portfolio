using FrikinCore.Player.Health;
using FrikinCore.Player.Inventory;
using FrikinCore.Player.Weapons;
using TSCore.Utils;

namespace FrikinCore.Player
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        public PlayerWeaponManagement WeaponManagement;
        public PlayerHealthController HealthController;
        public CharacterInventory Inventory;
        public PlayerDataModel Player { get; private set; }
        public bool PlayerActive => Player.IsNotNull();

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }

        public void SetPlayer(PlayerDataModel data) => Player = data;
    }
}
