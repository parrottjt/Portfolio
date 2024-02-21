namespace FrikinCore.Player.Items
{
    public abstract class Item<T>
    {
        IItemInfo _itemInfo;
        protected Item(IItemInfo itemInfo)
        {
            _itemInfo = itemInfo;
        }

        public IItemInfo ItemInfo() => _itemInfo;
        public abstract T Get();
    }
}
