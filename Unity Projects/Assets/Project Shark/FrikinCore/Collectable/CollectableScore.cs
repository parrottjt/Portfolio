using FrikinCore.Loot.Teeth;
using FrikinCore.Sfx;

namespace FrikinCore.Collectable
{
    public class CollectableScore : Collectable
    {
        ToothMovementForExplosion toothMovement;

        protected override void Awake()
        {
            base.Awake();
            toothMovement = GetComponent<ToothMovementForExplosion>();
        }

        protected override void OnPickUp()
        {
            AddScore();
            SetEnableOnComponentsTo(false);

            if (toothMovement != null) toothMovement.enabled = false;

            ActivateChildNumber();
            SoundManager.instance.RandomizeSfx(SoundManager.instance.TeethSfx);
        }
    }
}
