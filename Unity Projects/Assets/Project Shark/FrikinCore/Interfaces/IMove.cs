namespace FrikinCore.Interfaces
{
    public interface IMove
    {
        bool CanMove { get; set; }
        void Movement();
    }
}