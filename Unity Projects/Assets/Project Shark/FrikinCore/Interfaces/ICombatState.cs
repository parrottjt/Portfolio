
namespace FrikinCore.Interfaces
{
    public interface ICombatState : IAttack
    {
        void EnterCombat();
        void ExecuteCombat();
        void StopCombat();
    }
}