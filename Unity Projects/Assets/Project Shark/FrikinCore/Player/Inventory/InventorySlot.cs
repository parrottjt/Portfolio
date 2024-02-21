using FrikinCore.Player.Items;
using TSCore.Utils;

namespace FrikinCore.Player.Inventory
{
    public class InventorySlot<T>
    {
        Item<T> _item;

        public InventorySlot(Item<T> item) => _item = item;

        public bool SlotEmpty => _item.IsNull();
        public T Read() => _item.Get();
        public void ChangeSlot(Item<T> item) => _item = item;
    }
}
