using System.Collections.Generic;
using UnityEngine;

namespace FrikinCore.Stats
{
    public class Stat
    {
        float _statValue = 1;

        readonly List<StatModifier> statModifiers;

        public float GetStatValue(float baseValue)
        {
            if (hasBeenChanged)
            {
                _statValue = CalculateStatModifier(baseValue);
                hasBeenChanged = false;
            }

            return _statValue = CalculateStatModifier(baseValue);
        }

        bool hasBeenChanged = true;

        public Stat()
        {
            statModifiers = new List<StatModifier>();
        }

        public void AddModifier(StatModifier modifier)
        {
            if (statModifiers.Contains(modifier)) return;
            hasBeenChanged = true;
            statModifiers.Add(modifier);
            statModifiers.Sort(CompareModifierOrder);
        }

        int CompareModifierOrder(StatModifier a, StatModifier b)
        {
            if (a.Order < b.Order) return -1;
            return a.Order > b.Order ? 1 : 0;
        }

        public void RemoveModifier(StatModifier modifier)
        {
            if (!statModifiers.Contains(modifier)) return;
            hasBeenChanged = true;
            statModifiers.Remove(modifier);
        }

        public void UpdateModifierValue(StatModifier modifier, float updatedValue)
        {
            var index = statModifiers.FindIndex(x => x == modifier);
            statModifiers[index] = new StatModifier(updatedValue, modifier.Type);
            Debug.Log(statModifiers[index].Value);
        }

        float CalculateStatModifier(float baseValue)
        {
            float finalValue = baseValue;
            float sumPercentAdd = 0;

            for (int i = 0; i < statModifiers.Count; i++)
            {
                var mod = statModifiers[i];
                switch (mod.Type)
                {
                    case StatModifier.StatModType.Flat:
                        finalValue += mod.Value;
                        break;
                    case StatModifier.StatModType.Percent_Additive:
                        sumPercentAdd += mod.Value;
                        if (i + 1 >= statModifiers.Count ||
                            statModifiers[i + 1].Type != StatModifier.StatModType.Percent_Additive)
                        {
                            finalValue *= 1 + sumPercentAdd;
                        }

                        break;
                    case StatModifier.StatModType.Percent_Multiply:
                        finalValue *= 1 + mod.Value;
                        break;
                }
            }

            return finalValue;
        }
    }
}