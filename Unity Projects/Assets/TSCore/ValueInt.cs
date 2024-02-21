using System;
using UnityEngine;

namespace TSCore
{
    public class ValueInt
    {
        int value;
        int maxValue;

        public int Value => value;
        public int MaxValue => maxValue;
        public bool ValueAtMax => Value == MaxValue;
        
        public ValueInt(int amount, bool setValue = true)
        {
            IncreaseMax(amount);
            if(setValue) SetValueToMax();
        }

        public void Add(int amount) => ClampValue(value + Mathf.Abs(amount));
        public void Subtract(int amount) => ClampValue(value - Mathf.Abs(amount));
        public void IncreaseMax(int amount) => ClampMaxValue(maxValue + Mathf.Abs(amount));
        public void DecreaseMax(int amount) => ClampMaxValue(MaxValue - Mathf.Abs(amount));
        public void SetValue(int amount) => ClampValue(amount);
        public void SetValueToMax() => SetValue(MaxValue);
        public void SetMaxValue(int amount) => ClampMaxValue(amount);
        void ClampValue(int amount) => value = Mathf.Clamp(amount, 0, MaxValue);
        void ClampMaxValue(int amount)
        {
            maxValue = Mathf.Clamp(amount, 0, 9999);
            ClampValue(Value);
        }
    }

    public class ValueFloat
    {
        float value;
        float maxValue;

        public float Value => value;
        public float MaxValue => maxValue;
        public bool ValueAtMax => Math.Abs(Value - MaxValue) < 0.1f;
        
        public ValueFloat(float amount, bool setValue = true)
        {
            IncreaseMax(amount);
            if(setValue) SetValueToMax();
        }

        public void Add(float amount) => ClampValue(Value + Mathf.Abs(amount));
        public void Subtract(float amount) => ClampValue(Value - Mathf.Abs(amount));
        public void IncreaseMax(float amount) => ClampMaxValue(MaxValue + Mathf.Abs(amount));
        public void DecreaseMax(float amount) => ClampMaxValue(MaxValue - Mathf.Abs(amount));
        public void SetValue(float amount) => ClampValue(amount);
        public void SetValueToMax() => SetValue(MaxValue);
        public void SetMaxValue(float amount) => ClampMaxValue(amount);
        void ClampValue(float amount) => value = Mathf.Clamp(amount, 0, MaxValue);
        void ClampMaxValue(float amount)
        {
            maxValue = Mathf.Clamp(amount, 0, 9999);
            ClampValue(Value);
        }
    }
}