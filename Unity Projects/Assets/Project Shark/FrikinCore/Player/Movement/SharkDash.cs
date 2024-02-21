using FrikinCore.Enumeration;
using FrikinCore.Input;
using FrikinCore.Sfx;
using TSCore.Time;
using UnityEngine;

namespace FrikinCore.Player.Movement
{
    public class SharkDash : AbstractPlayerBehavior
    {
        int _dashTicks, _againTicks;
        bool canDash;

        protected override void Awake()
        {
            base.Awake();
            _againTicks = (int)Model.AgainTme;
        }

        void Start()
        {
            TickTimeTimer.OnTick += OnTick;
        }

        void OnDestroy()
        {
            TickTimeTimer.OnTick -= OnTick;
        }

        // Update is called once per frame
        void Update()
        {
            var dash = InputManager.instance.UtilityInput();

            Timer();

            HandleSprint(dash);

            TriggerDash(dash);

            HandleDash();
        }

        void HandleSprint(bool dash)
        {
            if (!canDash && !Model.SecondaryUtilityActive && InputManager.instance.UtilityHoldInput())
            {
                Model.PlayerMoveSpeed.ActivateStatusEffect(GameEnums.PlayerMovementEffects.Sprint);
                Model.SecondaryUtilityActive = true;
            }

            if (!dash && Model.SecondaryUtilityActive)
            {
                Model.PlayerMoveSpeed.DeactivateStatusEffect(GameEnums.PlayerMovementEffects.Sprint);
                Model.SecondaryUtilityActive = false;
            }

            Model.PrimaryUtilityParticleSystem.Run(Model.SecondaryUtilityActive);
        }

        void HandleDash()
        {
            if (!Model.PrimaryUtilityActive) return;
            if (Physics2D.OverlapCircle(transform.position, 1f, 1 << 14))
            {
                Model.PerfectDodge = true;
            }

            if (_dashTicks >= Model.DashTime)
            {
                //anim.SetBool("isDashing", false);
                Model.PrimaryUtilityActive = false;
                _dashTicks = 0;
            }
        }

        void TriggerDash(bool dash)
        {
            if (dash && canDash && Model.IsDead == false && Model.SecondaryUtilityActive == false)
            {
                var vector = Model.MovementVector != Vector2.zero ? Model.MovementVector : Model.LookVector;
                Model.Rigidbody.AddRelativeForce(vector * Model.DashSpeed, ForceMode2D.Impulse);

                _againTicks = 0;
                Model.PrimaryUtilityActive = true;
                canDash = false;
                Model.PrimaryUtilityParticleSystem.Run();
                //anim.SetBool("isDashing", true);

                PlayerManager.instance.HealthController.TurnInvulnerable();
                SoundManager.instance.RandomizeSfx(SoundManager.instance.DashSfx);
            }
        }

        void Timer()
        {
            if (canDash) return;
            if (_againTicks >= Model.AgainTme) canDash = true;
        }

        void OnTick(object sender, TickTimeTimer.OnTickEventArgs args)
        {
            if (Model.PrimaryUtilityActive) _dashTicks += 1;
            if (Model.PrimaryUtilityActive == false && canDash == false) _againTicks += 1;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (!Model.PrimaryUtilityActive) return;

            if (collision.gameObject.CompareTag("Destructable")) Model.OnDestructibleHit?.Invoke();
            //if (collision.gameObject.name == "Eel Egg") FindObjectOfType<BossStarter>().eggHit++;
        }

        void OnCollisionStay2D(Collision2D collision)
        {
            if (!Model.PrimaryUtilityActive) return;
            //if (collision.gameObject.name == "Eel Egg") FindObjectOfType<BossStarter>().eggHit++;
        }
    }
}
