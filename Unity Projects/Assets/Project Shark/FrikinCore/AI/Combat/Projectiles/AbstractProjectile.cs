using FrikinCore.Interfaces;
using UnityEngine;
using FrikinCore.Player;
using TSCore;
using TSCore.Time;
using TSCore.Utils;


namespace FrikinCore.AI.Combat.Projectiles
{
    public abstract class AbstractProjectile : MonoBehaviour, IDoDamage
    {
        public float speed;
        public int damage = 1;

        public StatusType statusType;

        protected float ModifiedSpeed() => speed * TimeManager.GlobalDelta;

        public void DisableGameObject()
        {
            GameObject projectile = gameObject;
            projectile.SetActive(false);
            EnsureHolderObjectPoolHolderOnDisable(projectile);
        }

        void EnsureHolderObjectPoolHolderOnDisable(GameObject projectile)
        {
            var holder = ObjectPooling.GetObjectHolder(projectile);
            if (holder != null)
            {
                if (holder.name != $"{projectile.name} Holder")
                {
                    projectile.transform.parent = holder;
                    projectile.transform.position = Vector3.zero;
                    projectile.transform.rotation = Quaternion.Euler(Vector3.zero);
                }
            }
            else
            {
                DebugScript.LogError(typeof(AbstractProjectile), "Projecile Holder not Found");
            }
        }

        public virtual void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(GameTags.Player.ToString())
                && !collision.gameObject.CompareTag(GameTags.Bangarang.ToString()))
            {
                DoDamage();
            }

            DisableGameObject();
        }

        //todo: change call to status 
        public void DoDamage(Collider2D objectCollider2D = null)
        {
            //todo: Once Oil and Mini Oil done put new code in this section
            switch (statusType)
            {
                case StatusType.Oil:
                    //GameManager.instance.playerCode.Oil(1.5f); //This needs to be changed over to Tick Time and not hard coded
                    break;

                case StatusType.MiniOil:
                    //GameManager.instance.playerCode.MiniOil(1.5f); //This needs to be changed over to Tick Time and not hard coded
                    break;

                case StatusType.Stun:
                    PlayerManager.instance.Player.IsStunned =
                        true; //This needs to be changed over to Tick Time and not hard coded
                    break;
                case StatusType.None:
                    PlayerManager.instance.HealthController.RemoveHealth(damage);
                    break;
            }
        }
    }
}
