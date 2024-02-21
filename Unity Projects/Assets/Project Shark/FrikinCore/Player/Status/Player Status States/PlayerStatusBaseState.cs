namespace FrikinCore.Player.Status.States
{
    public abstract class PlayerStatusBaseState
    {
        protected PlayerStatusContext CTX { get; }
        protected PlayerStatusFactory Factory { get; }

        protected PlayerStatusBaseState(PlayerStatusContext ctx, PlayerStatusFactory stateMachine)
        {
            CTX = ctx;
            Factory = stateMachine;
        }

        public abstract void EnterState();
        public abstract void ExecuteState();
        public abstract void ExitState();
        public abstract void CheckSwitchStates();
        public abstract void OnTick();

        protected void SwitchState(PlayerStatusBaseState state)
        {
            ExitState();
            state.EnterState();

            CTX.Current = state;
        }
    }
}
