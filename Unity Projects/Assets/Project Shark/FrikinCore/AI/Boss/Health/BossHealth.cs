using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FrikinCore.AI.Boss
{
    public abstract class BossHealth : HealthAbstract
    {
        public event Action OnHealthChange;

        public bool usingPercentages;

        [DrawIf("usingPercentages", true)] [SerializeField]
        int numberOfPhases;

        [SerializeField] float[] healthGates;
        [ReadOnly] [SerializeField] int phaseNumber = 1;
        bool hasDamageReduction, usingHealthGate;
        float damageReductionValue = .5f;
        [SerializeField] string nameOfBoss;

        protected Action PhaseChangeFunctionality { get; private set; }
        protected Action BossDeathFunctionality { get; private set; }

        public int GetPhaseNumber() => phaseNumber;
        public string GetNameOfBoss() => nameOfBoss;
        protected bool GetHasDamageReduction() => hasDamageReduction;

        public void SetHasDamageReduction(bool value) => hasDamageReduction = value;
        public void SetDamageReductionValue(float damageReduction) => damageReductionValue = damageReduction;

        protected override void Awake()
        {
            base.Awake();
            SetMaxHealthWithAdjustment(1);
            if (usingPercentages)
            {
                var percentage = 1f / numberOfPhases;
                healthGates = new float[numberOfPhases + 1];
                for (int i = 1; i < healthGates.Length; i++)
                {
                    healthGates[i] = Mathf.Clamp(MaxHealth - (MaxHealth * (percentage * i)), 0, MaxHealth);
                }
            }

            usingHealthGate = healthGates.Length > 0;
            if (usingHealthGate)
            {
                healthGates[0] = MaxHealth;
            }
        }

        protected void ResetPhaseNumber() => phaseNumber = 1;

        public override void TakeDamage(float amount, bool cutThroughArmor = false)
        {
            var damage = hasDamageReduction ? amount * (1 - damageReductionValue) : amount;

            base.TakeDamage(damage, cutThroughArmor);
            OnHealthChange?.Invoke();
            if (usingHealthGate)
            {
                if (phaseNumber < healthGates.Length - 1)
                {
                    if (Health < healthGates[phaseNumber])
                    {
                        PhaseChangeFunctionality?.Invoke();
                        phaseNumber++;
                    }
                }
            }

            if (Health <= 0)
            {
                BossDeathFunctionality?.Invoke();
            }
        }

        public void SetPhaseChangeFunctionality(Action functionality)
        {
            PhaseChangeFunctionality = functionality;
        }

        public void SetBossDeathFunctionality(Action functionality)
        {
            BossDeathFunctionality = functionality;
        }
    }
}