namespace FrikinCore.Interfaces
{
    public interface IPhase
    {
        void EnterPhase();
        void ExecutePhase();
        void ExitPhase();
    }
}