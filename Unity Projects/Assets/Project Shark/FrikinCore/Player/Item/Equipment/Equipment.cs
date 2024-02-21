
namespace FrikinCore.Player.Items.Equipment
{
    public class Equipment : Item<Equipment>
    {
        public Equipment(IItemInfo itemInfo) : base(itemInfo)
        {
        }

        public override Equipment Get() => this;
       
    }
}
