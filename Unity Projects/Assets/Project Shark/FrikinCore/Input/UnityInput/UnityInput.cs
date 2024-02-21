using FrikinCore.Player.Weapons;
using TSCore.Utils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace FrikinCore.Input
{
    public class UnityInput : IInput
    {
        readonly PlayerInputActions _input;

        public UnityInput(PlayerInputActions inputActions)
        {
            _input = inputActions;
            _input.Enable();
            _input.Play.OpenMenu.performed += PlayToMenu;
            _input.Menu.CloseMenu.performed += MenuToPlay;
            _input.Menu.Disable();
        }
        
        //Play Inputs
        public float HorizontalMoveAxis() => _input.Play.Movement.ReadValue<Vector2>().x;
        public float VerticalMoveAxis() => _input.Play.Movement.ReadValue<Vector2>().y;
        public bool MovementInput() => HorizontalMoveAxis().Abs() >= 0.1f || VerticalMoveAxis().Abs() >= 0.1f;
        public float HorizontalAimAxis() => _input.Play.Aim.ReadValue<Vector2>().x;
        public float VerticalAimAxis() => _input.Play.Aim.ReadValue<Vector2>().y;
        public bool AimInput() => HorizontalAimAxis().Abs() >= 0.1f || VerticalAimAxis().Abs() >= 0.1f;
        public bool AttackInput() => _input.Play.Attack.IsPressed();
        public bool AttackInputRelease() => _input.Play.Attack.WasReleasedThisFrame();
        public bool UtilityInput() => _input.Play.Utility.IsPressed();
        public bool UtilityHoldInput() => _input.Play.UtilityHold.IsPressed();
        public bool AnyInput() => MovementInput() || AimInput() || AttackInput() || UtilityInput();
        public bool WeaponSwapInput() => _input.Play.WeaponSwap.WasPressedThisFrame();
        public bool EquipmentInput() => _input.Play.Equipment.WasPressedThisFrame();
        public bool InventoryInput() => _input.Play.Inventory.WasPressedThisFrame();
        public bool InteractInput() => _input.Play.Interact.WasPressedThisFrame();

        //Menu Inputs
        public bool OpenMenuInput() => _input.Play.OpenMenu.WasPressedThisFrame();
        public bool CloseMenuInput() => _input.Menu.CloseMenu.WasPressedThisFrame();
        public bool MenuCancelInput() => _input.Menu.MenuCancel.WasPressedThisFrame();
        public void SwitchFromMenu()
        {
            _input.Play.Enable();
            _input.Menu.Disable();
        }
        
        //Action Event Subscriptions
        void PlayToMenu(InputAction.CallbackContext callbackContext)
        {
            _input.Play.Disable();
            _input.Menu.Enable();
        }
        void MenuToPlay(InputAction.CallbackContext callbackContext)
        {
            _input.Play.Enable();
            _input.Menu.Disable();
        }
    }
}
