using FrikinCore.Player;
using FrikinCore.Sfx;
using UnityEngine;

namespace FrikinCore.Collectable
{
    public class CollectableHealth : Collectable
    {
        [SerializeField] int healthAmount;

        protected override void OnPickUp()
        {
            SetEnableOnComponentsTo(false);
            SoundManager.instance.RandomizeSfx(SoundManager.instance.DuckSfx);
            if (PlayerManager.instance.HealthController.GetIsHealthFull() == false)
            {
                PlayerManager.instance.HealthController.AddHealth(healthAmount);
            }
            else
            {
                AddScore();
            }

            if (healthAmount >= 10)
            {
                GameManager.instance.loot.SuperDuckHasDropped = false;
            }
        }
    }
}
