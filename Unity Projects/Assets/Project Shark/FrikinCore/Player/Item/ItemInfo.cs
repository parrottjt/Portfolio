using Sirenix.OdinInspector;
using UnityEngine;

namespace FrikinCore.Player.Items
{
    public interface IItemInfo
    {
        public string Name { get; }
    }
    
    public abstract class ItemInfo : SerializedScriptableObject, IItemInfo
    {
        [SerializeField] string _itemName;
        [ReadOnly] [SerializeField] public int ID { get; set; }
        
        public string Name => _itemName;
    }
}
