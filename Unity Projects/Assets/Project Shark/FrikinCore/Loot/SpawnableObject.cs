using TSCore.Utils;
using UnityEngine;

namespace FrikinCore.Loot
{
    public interface ISpawn
    {
        public int ID { get; set; }
        public GameObject Spawnable { get; }
    }
    
    public class SpawnableObject : MonoBehaviour, ISpawn
    {
        [SerializeField] GameObject _holder;
        public int ID { get; set; }
        public GameObject Spawnable => _holder.IsNull() ? gameObject : _holder;
    }
}
