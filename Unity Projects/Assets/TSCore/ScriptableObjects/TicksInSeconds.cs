using TSCore.Time;
using UnityEngine;

namespace TSCore.ScriptableObject
{
    [CreateAssetMenu(menuName = "TSCore/Variables/Ticks In Seconds", fileName = "Ticks In Seconds")]
    public class TicksInSeconds : FloatVariable
    {
        public override float Value => Mathf.RoundToInt(value / TickTimeTimer.TickTimerMax);

        void OnValidate()
        {
            name = $"{value} Seconds";
        }
    }
}