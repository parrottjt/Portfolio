namespace FrikinCore.Interfaces
{
    public interface IState
    {
        void EnterState();
        void ExecuteState();
        void ExitState();
    }
}