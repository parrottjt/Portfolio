using UnityEngine;

namespace FrikinCore.Player
{
    public abstract class AbstractPlayerBehavior : MonoBehaviour
    {
        protected PlayerDataModel Model { get; private set; }

        protected virtual void Awake()
        {
            Model = GetComponent<PlayerDataModel>();
        }
    }
}
