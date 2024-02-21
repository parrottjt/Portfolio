using System;
using TSCore.Time;
using UnityEngine;

namespace FrikinCore.AI.Enemy.Combat.AttackHandlers
{
    public abstract class WeaponAttack : MonoBehaviour
    {
        void Start()
        {
            TickTimeTimer.OnTick += OnTickFunctionality;
        }

        void OnDestroy()
        {
            TickTimeTimer.OnTick -= OnTickFunctionality;
        }

        public abstract void Attack(Action methodForAnimation = null, bool additionalAttackCondition = true);
        protected abstract void OnTickFunctionality(object sender, TickTimeTimer.OnTickEventArgs e);
    }
}
