namespace FrikinCore.Interfaces
{
    public interface ITakeDamage<T>
    {
        T Health { get; }
        void TakeDamage(T damage, bool cutThroughArmor = false);
    }
}