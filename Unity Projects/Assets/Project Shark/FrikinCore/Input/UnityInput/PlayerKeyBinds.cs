using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FrikinCore.Player.Input
{
    public struct PlayerKeyBind
    {
        public readonly string ActionMap;
        public readonly string InputAction;
        public List<string> BindingOverridePaths;

        public PlayerKeyBind(string actionMap, string inputAction)
        {
            ActionMap = actionMap;
            InputAction = inputAction;
            BindingOverridePaths = new List<string>{"", ""};
        }

        public void OverridePaths(List<string> bindingOverridePaths)
        {
            for (int i = 0; i < bindingOverridePaths.Count; i++)
            {
                if (bindingOverridePaths.Count <= i)
                {
                    BindingOverridePaths.Add(bindingOverridePaths[i]);
                }
                else
                {
                    BindingOverridePaths[i] = bindingOverridePaths[i];
                }
            }
        }
    }

    [CreateAssetMenu]
    public class PlayerKeyBinds : SerializedScriptableObject
    {
        [SerializeField] Dictionary<string, PlayerKeyBind> _playerKeyBinds = new ();

        public void SaveBindingOverride(string actionMap, string actionName, List<string> overridePath)
        {
            var keyBind = FindKeyBind(actionMap, actionName, overridePath);
            keyBind.OverridePaths(overridePath);
            Debug.Log("Save Hit");
        }

        public PlayerKeyBind LoadBindingOverride(string actionMap, string actionName)
        {
            Debug.Log("Load Hit");
            return FindKeyBind(actionMap, actionName, new List<string>());
        }

        PlayerKeyBind FindKeyBind(string actionMap, string actionName, List<string> overridePath)
        {
            var key = actionMap + ", " + actionName;
            return _playerKeyBinds.ContainsKey(key) ? _playerKeyBinds[key] : 
                CreateKeyBind(actionMap, actionName, overridePath);
        }

        PlayerKeyBind CreateKeyBind(string actionMap, string actionName, List<string> overridePath)
        {
            var keyBind = new PlayerKeyBind(actionMap, actionName);
            keyBind.OverridePaths(overridePath);
            _playerKeyBinds.Add(actionMap + ", " + actionName, keyBind);
            Debug.Log($"{_playerKeyBinds.Count}");
            return keyBind;
        }

        [Button(Name = "Resets Overrides")]
        public void CreateDefaults()
        {
            _playerKeyBinds.Clear();
        }
    }
}