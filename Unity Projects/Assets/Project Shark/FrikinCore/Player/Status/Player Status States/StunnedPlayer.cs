using FrikinCore.Enumeration;
using FrikinCore.Sfx;

namespace FrikinCore.Player.Status.States
{
    public class StunnedPlayer : PlayerStatusBaseState
    {
        int _stunTicks;

        public StunnedPlayer(PlayerStatusContext ctx, PlayerStatusFactory stateMachine) : base(ctx, stateMachine)
        {
        }

        public override void EnterState()
        {
            HandleStun();
        }

        public override void ExecuteState()
        {
            if (_stunTicks >= CTX.Model.StunTime) ExitState();
        }

        public override void ExitState()
        {
            EndStun();
        }

        public override void CheckSwitchStates()
        {
            if (CTX.Model.IsStunned == false)
            {
                SwitchState(Factory.NoStatus());
            }
        }

        public override void OnTick()
        {
            _stunTicks += 1;
        }

        void HandleStun()
        {
            CTX.Model.PlayerMoveSpeed.ActivateStatusEffect(GameEnums.PlayerMovementEffects.Stun);
            CTX.Model.Animator.SetBool(PlayerDataModel.IsStunnedHash, true);
            CTX.ShakeCamera();
            SoundManager.instance.RandomizeSfx(SoundManager.instance.EelStunnedSfx);
        }

        void EndStun()
        {
            CTX.Model.Animator.SetBool(PlayerDataModel.IsStunnedHash, false);
            CTX.ShakeCamera();
            _stunTicks = 0;
            SoundManager.instance.RandomizeSfx(SoundManager.instance.EelStunnedSfx);
            CTX.Model.PlayerMoveSpeed.DeactivateStatusEffect(GameEnums.PlayerMovementEffects.Stun);
        }
    }
}
