using System;
using System.Collections.Generic;
using FrikinCore.Player.Input;
using TMPro;
using TSCore.Utils;
using UnityEngine.InputSystem;

namespace FrikinCore.Input
{
    public class InputRebind
    {
        public static Action RebindComplete;
        public static Action RebindCanceled;
        public static Action<InputAction, int> RebindStarted;
        
        readonly PlayerInputActions InputActions;
        PlayerKeyBinds _playerKeyBinds;

        public InputRebind(PlayerInputActions inputActions, PlayerKeyBinds playerKeyBinds)
        {
            InputActions = inputActions;
            _playerKeyBinds = playerKeyBinds;
        }

        public void StartRebind(string actionMap, string actionName, int bindingIndex, TMP_Text statusText,
            bool excludeMouse)
        {
            var action = InputActions.asset.FindAction(actionName);
            if (action.IsNull() || action.bindings.Count <= bindingIndex)
            {
                DebugScript.Log(this, "Couldn't find action or binding");
                return;
            }

            if (action.bindings[bindingIndex].isComposite)
            {
                var firstPartIndex = bindingIndex + 1;
                if (firstPartIndex < action.bindings.Count && action.bindings[firstPartIndex].isPartOfComposite)
                {
                    DoRebind(actionMap, action, firstPartIndex, statusText, excludeMouse, true);
                }
            }
            else DoRebind(actionMap, action, bindingIndex, statusText, excludeMouse);
        }

        void DoRebind(string actionMap, InputAction actionToRebind, int bindingIndex, TMP_Text statusText,
            bool excludeMouse, bool allCompositeParts = false)
        {
            if (actionToRebind.IsNull() || bindingIndex < 0) return;
            
            statusText.text = StatusText(allCompositeParts, actionToRebind, bindingIndex);

            actionToRebind.Disable();

            var rebind = actionToRebind.PerformInteractiveRebinding(bindingIndex);

            rebind.OnComplete(operation =>
            {
                actionToRebind.Enable();
                operation.Dispose();

                if (allCompositeParts)
                {
                    var nextBindingIndex = bindingIndex + 1;
                    if (nextBindingIndex < actionToRebind.bindings.Count &&
                        actionToRebind.bindings[nextBindingIndex].isPartOfComposite)
                    {
                        DoRebind(actionMap, actionToRebind, nextBindingIndex, statusText, excludeMouse, allCompositeParts);
                    }
                    else
                    {
                        RebindComplete?.Invoke();
                    }
                }

                SaveBindingOverride(actionMap, actionToRebind);
                if(actionToRebind.bindings[bindingIndex].isPartOfComposite == false &&
                   actionToRebind.bindings[bindingIndex].isComposite == false)
                    RebindComplete?.Invoke();
            });

            rebind.OnCancel(operation =>
            {
                actionToRebind.Enable();
                operation.Dispose();

                RebindCanceled?.Invoke();
            });


            if (excludeMouse) rebind.WithControlsExcluding("Mouse");

            rebind.WithCancelingThrough("*/{Cancel}");

            RebindStarted?.Invoke(actionToRebind, bindingIndex);
            rebind.Start();
        }

        public string GetBindingName(string actionName, int bindingIndex)
        {
            var action = InputActions.asset.FindAction(actionName);
            return action.GetBindingDisplayString(bindingIndex);
        }

        void SaveBindingOverride(string actionMap, InputAction action)
        {
            var overridePaths = new List<string>();
            for (int i = 0; i < action.bindings.Count; i++)
            {
                overridePaths.Add(action.bindings[i].path != action.bindings[i].overridePath
                    ? action.bindings[i].overridePath
                    : "");
            }

            _playerKeyBinds.SaveBindingOverride(actionMap, action.name, overridePaths);
        }

        public void LoadBindingOverride(string actionMap, string actionName)
        {
            var playerKeyBind = _playerKeyBinds.LoadBindingOverride(actionMap, actionName);
            var action = InputActions.asset.FindActionMap(actionMap).FindAction(actionName);

            if (action.IsNull()) return;
            if (string.IsNullOrEmpty(playerKeyBind.InputAction)) return;
            if (playerKeyBind.BindingOverridePaths.Count <= 0) return;
            for (int i = 0; i < action.bindings.Count; i++)
            {
                var overridePath = playerKeyBind.BindingOverridePaths[i];
                if (!string.IsNullOrEmpty(overridePath))
                {
                    action.ApplyBindingOverride(i, overridePath);
                }
            }
        }

        public void ResetBinding(string actionMap, string actionName, int bindingIndex)
        {
            var action = InputActions.asset.FindActionMap(actionMap).FindAction(actionName);

            if (action.IsNull() || action.bindings.Count <= bindingIndex)
            {
                DebugScript.Log(this, "Could not find action or binding");
                return;
            }

            if (action.bindings[bindingIndex].isComposite || action.bindings[bindingIndex].isPartOfComposite)
            {
                for (int i = bindingIndex;
                     i < action.bindings.Count &&
                     (action.bindings[i].isComposite || action.bindings[i].isPartOfComposite);
                     i++)
                {
                    action.RemoveBindingOverride(i);
                }
            }
            else
            {
                action.RemoveBindingOverride(bindingIndex);
            }

            SaveBindingOverride(actionMap, action);
        }

        string StatusText(bool composite, InputAction action, int index)
        {
            var text = "Press Input";

            if (composite)
            {
                text = $"Binding {action.bindings[index].name}";
            }
            
            return text;
        }
    }
}