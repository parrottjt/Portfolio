using FrikinCore.Player;
using Math = TSCore.Utils.Math;
using TSCore.Time;
using UnityEngine;

namespace FrikinCore.AI.Combat.Projectiles
{
    public class TrackingProjectile : AbstractProjectile
    {
        public int trackingMaxTime;
        int trackingTime;

        void OnTick(object sender, TickTimeTimer.OnTickEventArgs args)
        {
            trackingTime++;
        }

        void OnEnable()
        {
            trackingTime = 0;
            TickTimeTimer.OnTick += OnTick;
        }

        void OnDisable() => TickTimeTimer.OnTick -= OnTick;

        // Update is called once per frame
        void Update()
        {
            if (trackingTime < trackingMaxTime)
            {
                transform.rotation = Math.RotationLookAtYAxis(
                    GameManager.IsInitialized
                        ? PlayerManager.instance.Player.transform
                        : GameObject.Find("Player").transform, transform);
            }

            transform.Translate(transform.up * ModifiedSpeed(), Space.World);
        }
    }
}
