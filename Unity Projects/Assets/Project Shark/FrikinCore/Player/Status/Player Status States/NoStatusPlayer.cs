namespace FrikinCore.Player.Status.States
{
    public class NoStatusPlayer : PlayerStatusBaseState
    {
        public NoStatusPlayer(PlayerStatusContext ctx, PlayerStatusFactory stateMachine) :
            base(ctx, stateMachine)
        {
        }

        public override void EnterState()
        {
        }

        public override void ExecuteState()
        {
        }

        public override void ExitState()
        {
        }

        public override void CheckSwitchStates()
        {
            if (CTX.Model.IsStunned) SwitchState(Factory.Stunned());
            if (CTX.Model.IsFreezing) SwitchState(Factory.Freeze());
            if (CTX.Model.IsDead) SwitchState(Factory.Death());
        }

        public override void OnTick()
        {
        }
    }
}