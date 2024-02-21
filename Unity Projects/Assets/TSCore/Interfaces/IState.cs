namespace TSCore.States
{
    public interface IState
    {
        void Enter();
        void Execute();
        void Exit();
    }

    public class StateMachine
    {
        IState currentState;

        public IState Current => currentState;

        public StateMachine(IState state)
        {
            EnterState(state);
        }

        public void EnterState(IState state)
        {
            ExitState();
            currentState = state;
            currentState?.Enter();
        }

        public void ExecuteState()
        {
            currentState?.Execute();
        }
        
        public void ExitState()
        {
            currentState?.Exit();
        }
    }
}
