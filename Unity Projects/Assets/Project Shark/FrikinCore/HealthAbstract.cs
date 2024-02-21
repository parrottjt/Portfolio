using System;
using FrikinCore.AI.Combat;
using FrikinCore.Interfaces;
using Sirenix.OdinInspector;
using TSCore;
using UnityEngine;

namespace FrikinCore
{
    public abstract class HealthAbstract : MonoBehaviour, ITakeDamage<float>, ITakePlayerDamage
    {
        [SerializeField] float _startingHealth;
        [SerializeField] int _startingArmor;

        [ReadOnly][SerializeField] float _healthValue;
        [ReadOnly][SerializeField] float _maxHealthValue;
        [ReadOnly][SerializeField] int _armorValue;
        
        ValueFloat _health;
        ValueInt _armor;

        public event Action ArmorValueChange;
        public event Action HealthValueChange;

        protected virtual void Awake()
        {
            _health = new ValueFloat(_startingHealth);
            _armor = new ValueInt(_startingArmor);
        }

        public float Health => _health.Value;
        public float MaxHealth => _health.MaxValue;
        public bool HealthFull => _health.ValueAtMax;
        public int Armor => _armor.Value;

        public void FullyHeal()
        {
            _health.SetValueToMax();
            HealthValueChange?.Invoke();
        }

        public void ReduceHealth(float amount)
        {
            _health.Subtract(amount);
            HealthValueChange?.Invoke();
        }

        public virtual void TakeDamage(float damage, bool cutThroughArmor = false)
        {
            if (_armor.Value > 0 && !cutThroughArmor)
            {
                _armor.Subtract(1);
                ArmorValueChange?.Invoke();
            }
            else
            {
                _health.Subtract(damage);
                HealthValueChange?.Invoke();
            }
        }

        public virtual void AddArmor(int number)
        {
            _armor.Add(number);
            ArmorValueChange?.Invoke();
        }

        public virtual void AddHealth(float number)
        {
            _health.Add(number);
            HealthValueChange?.Invoke();
        }

        public virtual void SetHealth(float amount)
        {
            _health.SetValue(amount);
            HealthValueChange?.Invoke();
        }

        protected void SetMaxHealth(float value, float adjustment)
        {
            _health.SetMaxValue(value);
            SetMaxHealthWithAdjustment(adjustment);
        }
        
        protected void SetMaxHealthWithAdjustment(float adjustment)
        {
            var withAdjustment = MaxHealth * adjustment;
            var adjustmentValue = withAdjustment - MaxHealth;
            switch (adjustmentValue)
            {
                case < 0: _health.DecreaseMax(adjustmentValue); break;
                case > 0: _health.IncreaseMax(adjustmentValue); break;
                default: break;
            }
            FullyHeal();
        }
    }
}