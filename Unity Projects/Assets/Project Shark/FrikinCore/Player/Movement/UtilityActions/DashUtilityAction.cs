using FrikinCore.Enumeration;
using FrikinCore.Sfx;
using TSCore.Time;
using UnityEngine;

namespace FrikinCore.Player.Movement
{
    public class DashUtilityAction : UtilityAction
    {
        int _dashTicks;
        bool _canDash;
        int _againTicks;

        protected override void Awake()
        {
            base.Awake();
            _againTicks = (int)Model.AgainTme;
        }

        protected override void AdditionalUpdateFunctionality(bool primary, bool secondary)
        {
            Timer();
            HandleDash();
            HandleSprint(secondary);
        }
        protected override void PrimaryUtilityFunctionality()
        {
            TriggerDash();
            print("Dash Triggered");
        }
        protected override void SecondaryUtilityFunctionality()
        {
            TriggerSprint();
        }
        protected override void OnTick(object sender, TickTimeTimer.OnTickEventArgs e)
        {
            if (Model.PrimaryUtilityActive) _dashTicks += 1;
            if (!_canDash) _againTicks += 1;
        }

        void HandleSprint(bool sprint)
        {
            if (!sprint && Model.SecondaryUtilityActive)
            {
                Model.PlayerMoveSpeed.DeactivateStatusEffect(GameEnums.PlayerMovementEffects.Sprint);
                Model.SecondaryUtilityActive = false;
            }

            Model.SecondaryUtilityParticleSystem.Run(Model.SecondaryUtilityActive);
        }
        void TriggerSprint()
        {
            if (!_canDash && !Model.SecondaryUtilityActive)
            {
                Model.PlayerMoveSpeed.ActivateStatusEffect(GameEnums.PlayerMovementEffects.Sprint);
                Model.SecondaryUtilityActive = true;
            }
        }
        void HandleDash()
        {
            if (!Model.PrimaryUtilityActive) return;

            if (Physics2D.OverlapCircle(Model.Transform.position, 1f, 1 << 14))
            {
                Model.PerfectDodge = true;
            }

            if (_dashTicks >= Model.DashTime)
            {
                Model.PrimaryUtilityActive = false;
                _dashTicks = 0;
            }
        }
        void TriggerDash()
        {
            if (_canDash && Model.IsDead == false && Model.SecondaryUtilityActive == false)
            {
                var vector = Model.MovementVector != Vector2.zero ? Model.MovementVector : Model.LookVector;
                Model.Rigidbody.AddRelativeForce(vector * Model.DashSpeed, ForceMode2D.Impulse);

                _againTicks = 0;
                Model.PrimaryUtilityActive = true;
                _canDash = false;
                
                
                Model.PrimaryUtilityParticleSystem.Run();
                
                //Todo: Uncomment this once health is better
                //PlayerManager.instance.HealthController.TurnInvulnerable();
                //SoundManager.instance.RandomizeSfx(SoundManager.instance.DashSfx);
            }
        }
        void Timer()
        {
            if (_canDash) return;
            if (_againTicks >= Model.AgainTme) _canDash = true;
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            if (!Model.PrimaryUtilityActive) return;
            if (col.gameObject.CompareTag(GameTags.Destructable.ToString())) Model.OnDestructibleHit?.Invoke();
        }
    }
}