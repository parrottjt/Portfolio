using System;
using TSCore;
using UnityEngine;

namespace FrikinCore.Player.Health
{
    public class PlayerHealth : MonoBehaviour
    {
        readonly ValueInt _health;
        readonly ValueInt _armor;

        public static event Action ArmorValueChange;
        public static event Action HealthValueChange;
        
        public PlayerHealth(int startingHealth, int startingArmor)
        {
            _health = new ValueInt(startingHealth);
            _armor = new ValueInt(startingArmor);
        }

        public int Health => _health.Value;
        public int MaxHealth => _health.MaxValue;
        public bool HealthFull => _health.ValueAtMax;
        public int Armor => _armor.Value;

        public void FullyHeal()
        {
            _health.SetValueToMax();
            HealthValueChange?.Invoke();
        }

        public virtual void AddHealth(int number)
        {
            _health.Add(number);
            HealthValueChange?.Invoke();
        }
        public void ReduceHealth(int amount)
        {
            _health.Subtract(amount);
            HealthValueChange?.Invoke();
        }
        public void SetHealth(int amount)
        {
            _health.SetValue(amount);
            HealthValueChange?.Invoke();
        }
        public void AddMaxHealth(int amount)
        {
            _health.IncreaseMax(amount);
            HealthValueChange?.Invoke();
        }
        public void ReduceMaxHealth(int amount)
        {
            _health.DecreaseMax(amount);
            if (MaxHealth < Health)
            {
                FullyHeal();
            }
            HealthValueChange?.Invoke();
        }

        public void TakeDamage(int damage, bool cutThroughArmor = false)
        {
            if (_armor.Value > 0 && !cutThroughArmor) ReduceArmor(1);
            else ReduceHealth(damage);
        }
        
        public void AddArmor(int number)
        {
            _armor.Add(number);
            ArmorValueChange?.Invoke();
        }

        public void ReduceArmor(int number)
        {
            _armor.Subtract(number);
            ArmorValueChange?.Invoke();
        }
    }
}
