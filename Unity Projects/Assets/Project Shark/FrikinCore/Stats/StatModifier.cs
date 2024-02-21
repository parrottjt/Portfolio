namespace FrikinCore.Stats
{
    public class StatModifier
    {
        public enum StatModType
        {
            Flat = 10, //Don't know if this will be used at a later time so it will be here for now
            Percent_Additive = 20, //Additive Multiplication
            Percent_Multiply = 30, //Multiply Whole Value
        }

        public float Value { get; private set; }

        public readonly StatModType Type;
        public readonly int Order;
        public readonly object Source;


        public StatModifier(float value, StatModType type, int order, object source)
        {
            Value = value;
            Type = type;
            Order = order;
            Source = source;

        }

        public StatModifier(float value, StatModType type) : this(value, type, (int)type, null)
        {
        }

        public StatModifier(float value, StatModType type, int order) : this(value, type, order, null)
        {
        }

        public StatModifier(float value, StatModType type, object source) : this(value, type, (int)type, source)
        {
        }

        public void UpdateModifierValue(float value)
        {
            Value = value;
        }
    }
}