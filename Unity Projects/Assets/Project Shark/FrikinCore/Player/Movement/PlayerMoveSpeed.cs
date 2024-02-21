using System.Collections.Generic;
using FrikinCore.Enumeration;
using FrikinCore.Stats;
using TSCore.Utils;

namespace FrikinCore.Player.Movement
{
    public class PlayerMoveSpeed
    {
        Stat _speed = new();

        Dictionary<GameEnums.PlayerMovementEffects, StatModifier> _statusModifier = new()
        {
            {
                GameEnums.PlayerMovementEffects.Freeze,
                new StatModifier(0f, StatModifier.StatModType.Percent_Multiply)
            },
            {
                GameEnums.PlayerMovementEffects.Stun,
                new StatModifier(0f, StatModifier.StatModType.Percent_Multiply)
            },
            {
                GameEnums.PlayerMovementEffects.Slow,
                new StatModifier(-.5f, StatModifier.StatModType.Percent_Additive)
            },
            {
                GameEnums.PlayerMovementEffects.Sprint,
                new StatModifier(.5f, StatModifier.StatModType.Percent_Additive)
            },
            {
                GameEnums.PlayerMovementEffects.Death,
                new StatModifier(0, StatModifier.StatModType.Percent_Multiply)
            }
        };

        public void ActivateStatusEffect(GameEnums.PlayerMovementEffects type)
        {
            if (!_statusModifier.ContainsKey(type))
            {
                DebugScript.LogYellowText(this, $"Status {type} doesn't effect movement");
            }
            else _speed.AddModifier(_statusModifier[type]);
        }

        public void DeactivateStatusEffect(GameEnums.PlayerMovementEffects type)
        {
            if (!_statusModifier.ContainsKey(type))
            {
                DebugScript.LogYellowText(this, $"Status {type} doesn't effect movement");
            }
            else _speed.RemoveModifier(_statusModifier[type]);
        }

        public Stat Speed => _speed;
    }
}