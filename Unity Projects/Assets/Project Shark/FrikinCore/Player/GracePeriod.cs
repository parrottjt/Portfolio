using FrikinCore.Input;
using UnityEngine;

namespace FrikinCore.Player
{
    /// Update Log
    /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    /// - Jon 9/24/19 - Projectile issue, updated if with timeSinceLevelLoad >= 2f to have or(s)
    ///                 to allow for the grace period to end with certain inputs 
    ///
    /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public class GracePeriod : AbstractPlayerBehavior
    {
        //[Tooltip("In Seconds")]
        //public float graceTime;
        // Use this for initialization
        protected void Start()
        {
            ActivateRespawnEffectOnPlayer(); //todo make this work on level start
        }

        public void ActivateRespawnEffectOnPlayer()
        {
            Model.PlayerInvulnerability.TurnInvulnerable(false);
            Model.Rigidbody.gravityScale = 0;
            Model.GracePeriodActive = true;
        }

        public void RespawningOff()
        {
            Debug.Log("RespawningOff called in GracePeriod");
            Model.PlayerInvulnerability.ResetInvulnerability();
            Model.Animator.SetBool(PlayerDataModel.DeathFadeIn, false);
            Model.Rigidbody.gravityScale = Model.DefaultValues.GravityScale;
        }

        public void CheckToSeeIfRespawnEffectCanTurnOff()
        {
            if (InputManager.instance.AnyInput())
            {
                RespawningOff();
                Model.GracePeriodActive = false;
            }
        }
    }
}