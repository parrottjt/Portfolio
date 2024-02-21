using System;
using FrikinCore.Player.Input;
using TSCore.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FrikinCore.Input
{
    public interface IInput
    {
        //Play Inputs
        public float HorizontalMoveAxis();
        public float VerticalMoveAxis();
        public float HorizontalAimAxis();
        public float VerticalAimAxis();
        public bool MovementInput();
        public bool AimInput();
        public bool AttackInput();
        public bool AttackInputRelease();
        public bool UtilityInput();
        public bool UtilityHoldInput();
        public bool AnyInput();
        public bool WeaponSwapInput();
        public bool EquipmentInput();
        public bool InventoryInput();
        public bool InteractInput();

        //Menu Inputs
        public bool OpenMenuInput();
        public bool CloseMenuInput();
        public bool MenuCancelInput();
        public void SwitchFromMenu();
    }

    [RequireComponent(typeof(PlayerInput))]
    public class InputManager : Singleton<InputManager>
    {
        static PlayerInputActions _inputActions;
        PlayerInput _playerInput;
        InputRebind _rebind;
        IInput _input;

        string _lastControlScheme;

        public static event Action ControlSchemeChanged;
        
        public InputRebind Rebind => _rebind;
        public string CurrentControlScheme => _playerInput.currentControlScheme;
        public bool ControllerInUse => CurrentControlScheme != "KeyboardMouse";

        //Play Inputs
        public float HorizontalMoveAxis() => _input.HorizontalMoveAxis();
        public float VerticalMoveAxis() => _input.VerticalMoveAxis();
        public float HorizontalAimAxis() => _input.HorizontalAimAxis();
        public float VerticalAimAxis() => _input.VerticalAimAxis();
        public bool AnyInput() => _input.AnyInput() || MovementInput() || AimInput();
        public bool MovementInput() => _input.MovementInput();
        public bool AimInput() => _input.AimInput();
        public bool AttackInput() => _input.AttackInput();
        public bool AttackInputRelease() => _input.AttackInputRelease();
        public bool UtilityInput() => _input.UtilityInput();
        public bool UtilityHoldInput() => _input.UtilityHoldInput();
        public bool SwapWeaponInput() => _input.WeaponSwapInput();
        public bool EquipmentInput() => _input.EquipmentInput();
        public bool InventoryInput() => _input.InventoryInput();
        public bool InteractInput() => _input.InteractInput();

        //Todo: Remove references
        public bool WeaponWheelActiveInput() => false;
        public bool WeaponModeChange() => false;
        public bool WeaponIndexIncreaseInput() => false;
        public bool WeaponIndexDecreaseInput() => false;

        //Menu Inputs
        public bool OpenMenuInput() => _input.OpenMenuInput();
        public bool CloseMenuInput() => _input.CloseMenuInput();
        public bool MenuCancelInput() => _input.MenuCancelInput();
        public void SwitchFromMenu() => _input.SwitchFromMenu();

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);

            var keyBinds = Resources.Load<PlayerKeyBinds>("Input/PlayerKeyBinds");

            if (_playerInput.IsNull()) _playerInput = GetComponent<PlayerInput>();
            if (_inputActions.IsNull()) _inputActions = new PlayerInputActions();
            _input = new UnityInput(_inputActions);
            _rebind = new InputRebind(_inputActions, keyBinds);
        }

        void Update()
        {
            if (_lastControlScheme != CurrentControlScheme)
            {
                ControlSchemeChanged?.Invoke();
                _lastControlScheme = _playerInput.currentControlScheme;
            }
        }
    }
}