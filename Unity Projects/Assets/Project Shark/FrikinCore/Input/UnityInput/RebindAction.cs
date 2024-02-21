using Sirenix.OdinInspector;
using TMPro;
using TSCore.Utils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace FrikinCore.Input
{
    public class RebindAction : MonoBehaviour
    {
        [SerializeField] InputActionReference _inputActionReference;

        [SerializeField] bool _excludeMouse = true;

        [Range(0, 10)] [SerializeField] int _selectedBinding;
        [SerializeField] InputBinding.DisplayStringOptions _displayStringOptions;

        [Header("Binding Info")] 
        [ReadOnly] [SerializeField] InputBinding _inputBinding;
        [ReadOnly][SerializeField] int _bindingIndex;
        string _actionMap;
        string _actionName;

        [Header("UI Fields")] 
        [SerializeField] TMP_Text _actionText;
        [SerializeField] Button _rebindButton;
        [SerializeField] TMP_Text _rebindText;
        [SerializeField] Button _resetButton;

        void OnEnable()
        {
            _rebindButton.onClick.AddListener(DoRebind);
            _resetButton.onClick.AddListener(ResetBinding);

            if (_inputActionReference.IsNotNull())
            {
                GetBindingInfo();
                InputManager.instance.Rebind.LoadBindingOverride(_actionMap, _actionName);
                UpdateUI();
            }
            
            InputRebind.RebindComplete += UpdateUI;
            InputRebind.RebindCanceled += UpdateUI;
        }

        void OnDisable()
        {
            InputRebind.RebindComplete -= UpdateUI;
            InputRebind.RebindCanceled -= UpdateUI;
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
            if (_actionText.IsNotNull()) _actionText.text = _actionName;

            if (_rebindText.IsNotNull())
            {
                if (Application.isPlaying && InputManager.IsInitialized)
                {
                    _rebindText.text = InputManager.instance.Rebind.GetBindingName(_actionName, _bindingIndex);
                }
                else
                {
                    _rebindText.text = _inputActionReference.action.GetBindingDisplayString(_bindingIndex);
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
    }
}
