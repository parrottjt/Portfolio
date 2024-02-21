using FrikinCore.Interfaces;
using TSCore.Utils;
using UnityEngine;

namespace FrikinCore.AI.Combat
{
    public class CombatStateMachine
    {
        public enum CombatStates
        {
            Idle,
            StartCombat,
            RunCombat,
            StopCombat
        }

        CombatStates _combatStatesController;
        ICombatState _combatState;

        public CombatStates GetCurrentCombatState() => _combatStatesController;

        bool _happenOneTime;
        readonly string _gameObjectName;

        public CombatStateMachine(ICombatState combatState, GameObject gameObject)
        {
            _combatState = combatState;
            _gameObjectName = gameObject.name;
            DebugScript.LogYellowText(this, $"{_gameObjectName} is using {_combatState}");
        }

        public void RunCombat()
        {
            switch (_combatStatesController)
            {
                case CombatStates.Idle:
                    break;
                case CombatStates.StartCombat:
                    EnterCombat();
                    _combatStatesController = CombatStates.RunCombat;
                    break;
                case CombatStates.RunCombat:
                    if (PauseManager.IsPaused() == false) ExecuteCombat();
                    break;
                case CombatStates.StopCombat:
                    ExitCombat();
                    _combatStatesController = CombatStates.Idle;
                    break;
                default:
                    Debug.LogError($"[{_combatState}] Combat state when out of range");
                    break;
            }
        }

        public void StartCombat()
        {
            if (_happenOneTime) return;
            _combatStatesController = CombatStates.StartCombat;
            _happenOneTime = true;
            DebugScript.LogGreenText(this, $"{_gameObjectName} has Entered Combat");
        }

        public void StopCombat()
        {
            _combatStatesController = CombatStates.StopCombat;
            _happenOneTime = false;
            DebugScript.LogRedText(this, $"{_gameObjectName} has Exited Combat");
        }

        protected virtual void EnterCombat()
        {
            _combatState?.EnterCombat();
        }

        protected virtual void ExecuteCombat() => _combatState?.ExecuteCombat();

        protected virtual void ExitCombat()
        {
            _combatState?.StopCombat();
        }
    }
}