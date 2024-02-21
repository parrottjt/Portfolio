using System;
using FrikinCore.Input;
using FrikinCore.Utils;
using Sirenix.OdinInspector;
using TMPro;
using TSCore.Utils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UICore.Menu
{
    [Serializable]
    public struct InputBindingStartingIndex
    {
        public int Keyboard;
        public int Gamepad;
    }
    public class RebindControlsUI : MonoBehaviour
    {
        [SerializeField] InputActionReference _inputActionReference;
        [SerializeField] bool _excludeMouse;
        [SerializeField] InputBindingStartingIndex _bindingStartingIndex;

        [Range(0, 10)][SerializeField] int _selectedBinding;
        [SerializeField] InputBinding.DisplayStringOptions _displayStringOptions;

        [Header("Binding Info")] 
        [ReadOnly] [SerializeField] InputBinding _inputBinding;
        [ReadOnly] [SerializeField] int _bindingIndex;

        [Header("UI Fields")] 
        [SerializeField] TMP_Text _actionText;
        [SerializeField] Button _rebindButton;
        [SerializeField] TMP_Text _rebindText;
        [SerializeField] Button _resetButton;

        string _actionMap;
        string _actionName;

        void OnEnable()
        {
            _rebindButton.onClick.AddListener(DoRebind);
            _resetButton.onClick.AddListener(ResetBinding);

            InputRebind.RebindComplete += UpdateUI;
            InputRebind.RebindCanceled += UpdateUI;
            InputManager.ControlSchemeChanged += ControlSchemeChanged;
        }

        void OnDisable()
        {
            InputRebind.RebindComplete -= UpdateUI;
            InputRebind.RebindCanceled -= UpdateUI;
            InputManager.ControlSchemeChanged -= ControlSchemeChanged;
        }

        void Start()
        {
            if (_inputActionReference.IsNotNull())
            {
                GetBindingInfo();
                InputManager.instance.Rebind.LoadBindingOverride(_actionMap, _actionName);
                UpdateUI();
            }
        }

        void OnValidate()
        {
            if(_inputActionReference.IsNull()) return;
            GetBindingInfo();
            UpdateUI();
        }
        
        void GetBindingInfo()
        {
            if (_inputActionReference.action.IsNotNull())
            {
                _actionMap = _inputActionReference.action.actionMap.name;
                _actionName = _inputActionReference.action.name;
            }
            if (_inputActionReference.action.bindings.Count > _selectedBinding)
            {
                _inputBinding = _inputActionReference.action.bindings[_selectedBinding];
                _bindingIndex = _selectedBinding;
            }
        }
        
        void UpdateUI()
        {
            if (_actionText.IsNotNull()) _actionText.text = RebindNameConversion.ActionNameConvert(_actionName);

            if (_rebindText.IsNotNull())
            {
                if (Application.isPlaying && InputManager.IsInitialized)
                {
                    var bindingText = InputManager.instance.Rebind.GetBindingName(_actionName, _bindingIndex);
                    _rebindText.text = RebindNameConversion.BindingNameConvert(bindingText);
                }
                else
                {
                    var bindingText = _inputActionReference.action.GetBindingDisplayString(_bindingIndex, _displayStringOptions);
                    _rebindText.text = RebindNameConversion.BindingNameConvert(bindingText);
                }
            }
        }

        void DoRebind()
        {
            InputManager.instance.Rebind.StartRebind(_actionMap, _actionName, _bindingIndex, _rebindText,_excludeMouse);
        }
        
        void ResetBinding()
        {
            InputManager.instance.Rebind.ResetBinding(_actionMap, _actionName, _bindingIndex);
            UpdateUI();
        }

        //Todo: Remove this is we don't wont controls to change on control scheme change
        void ControlSchemeChanged()
        {
            switch (InputManager.instance.CurrentControlScheme)
            {
                case "KeyboardMouse":
                    _selectedBinding = _bindingStartingIndex.Keyboard;
                    break;
                case "Gamepad":
                    _selectedBinding = _bindingStartingIndex.Gamepad;
                    break;
            }
            GetBindingInfo();
            UpdateUI();
        }
    }
}
