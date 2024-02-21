namespace FrikinCore.Player.Status.States
{
    public class FreezePlayerState : PlayerStatusBaseState
    {
        int _freezingTicks, _frozenTicks;

        public FreezePlayerState(PlayerStatusContext ctx, PlayerStatusFactory stateMachine) : base(ctx, stateMachine)
        {
        }

        public override void EnterState()
        {
            StartFreezing();
        }

        public override void ExecuteState()
        {
            CTX.Model.Freezing.Run(CTX.Model.IsFreezing || CTX.Model.IsFrozen);
        }

        public override void ExitState()
        {
        }

        public override void CheckSwitchStates()
        {
            var canSwitch = CTX.Model.IsFrozen == false && CTX.Model.IsFreezing;
            if (canSwitch) SwitchState(Factory.NoStatus());
        }

        public override void OnTick()
        {

        }

        void StartFreezing()
        {
            CTX.Model.IsSlowed = true;

        }

        void StartFreeze()
        {
            CTX.Model.IsFrozen = true;
        }
    }
}