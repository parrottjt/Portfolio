namespace FrikinCore.Interfaces
{
    public interface IHaveHealthBar<out T>
    {
        T Health { get; }
    }
}