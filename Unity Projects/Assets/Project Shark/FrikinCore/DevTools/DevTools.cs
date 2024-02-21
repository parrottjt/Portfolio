using System;
using System.Collections.Generic;
using FrikinCore.Player;
using FrikinCore.Player.Weapons;
using FrikinCore.Save;
using TSCore.Time;
using UICore;
using UnityEngine;

namespace FrikinCore.DevelopmentTools
{
    public enum DevTool
    {
        DevTools,
        Invulnerability,
        UnlimitedDash,
        UnlimitedAmmo,
        SuperShot,
        KawaiiMode,
        SaveLoadTest,
        DEBUG_FOR_STEAM_ACHIEVEMENTS
    }

    public class DevTools : MonoBehaviour
    {
        string on = "On", off = "Off";

        public static event Action DevToolToggle;

        GameManager gameManager;

        public static Dictionary<DevTool, bool> devToolsDictionary = new Dictionary<DevTool, bool>()
        {
            { DevTool.DevTools, false },
            { DevTool.Invulnerability, false },
            { DevTool.KawaiiMode, false },
            { DevTool.SuperShot, false },
            { DevTool.UnlimitedAmmo, false },
            { DevTool.UnlimitedDash, false },
            { DevTool.SaveLoadTest, false },
            { DevTool.DEBUG_FOR_STEAM_ACHIEVEMENTS, false }
        };

        void SetDevToolBoolValueToOpposite(DevTool devTool) =>
            devToolsDictionary[devTool] = !devToolsDictionary[devTool];

        bool GetExtraIdentifierForActivation(KeyCode keyCodePrime, KeyCode keyCodeSecondary)
        {
            return UnityEngine.Input.GetKey(keyCodePrime) && UnityEngine.Input.GetKey(keyCodeSecondary);
        }

        bool GetExtraIdentifierForActivation(KeyCode keyCodePrime)
        {
            return UnityEngine.Input.GetKey(keyCodePrime);
        }

        void GetKeyPressToToggleBool(bool activationControl, KeyCode keyCode, DevTool devtool)
        {
            if (UnityEngine.Input.GetKeyDown(keyCode) && activationControl)
            {
                if (keyCode == KeyCode.I) DevToolToggle?.Invoke();
                SetDevToolBoolValueToOpposite(devtool);
                print($"Dev Tool: {devtool.ToString()} is {(devToolsDictionary[devtool] ? on : off)}");
            }
        }

        void GetKeyPressToActiveFunction(bool activationControl, KeyCode keyCode,
            params Action[] functions)
        {
            if (!UnityEngine.Input.GetKeyDown(keyCode) || !activationControl) return;
            foreach (var function in functions)
            {
                function();
            }
        }

        void GetDevToolBoolToActivateFunction(DevTool devTool, params Action[] functions)
        {
            if (!devToolsDictionary[devTool]) return;
            foreach (var function in functions)
            {
                function();
            }
        }

        void Start()
        {
            gameManager = GetComponent<GameManager>();
        }

        void Update()
        {
            //Dev Tools won't work unless Dev Tools is true
            GetKeyPressToToggleBool(GetExtraIdentifierForActivation(KeyCode.LeftControl), KeyCode.I, DevTool.DevTools);
            if (!devToolsDictionary[DevTool.DevTools]) return;

            //Dev Tool Bools
            GetKeyPressToToggleBool(GetExtraIdentifierForActivation(KeyCode.LeftShift), KeyCode.O, DevTool.SuperShot);
            GetKeyPressToToggleBool(GetExtraIdentifierForActivation(KeyCode.LeftControl), KeyCode.J,
                DevTool.UnlimitedDash);
            GetKeyPressToToggleBool(GetExtraIdentifierForActivation(KeyCode.LeftControl), KeyCode.L,
                DevTool.Invulnerability);
            GetKeyPressToToggleBool(GetExtraIdentifierForActivation(KeyCode.LeftControl), KeyCode.U,
                DevTool.UnlimitedAmmo);
            GetKeyPressToToggleBool(GetExtraIdentifierForActivation(KeyCode.LeftShift), KeyCode.P, DevTool.KawaiiMode);
            GetKeyPressToToggleBool(GetExtraIdentifierForActivation(KeyCode.LeftControl), KeyCode.Y,
                DevTool.SaveLoadTest);
            GetKeyPressToToggleBool(GetExtraIdentifierForActivation(KeyCode.LeftControl), KeyCode.T,
                DevTool.DEBUG_FOR_STEAM_ACHIEVEMENTS);

            //Dev Tool Functions
            GetKeyPressToActiveFunction(GetExtraIdentifierForActivation(KeyCode.LeftShift), KeyCode.K,
                () => PlayerManager.instance.HealthController.SetHealth(1),
                () => print($"Player Health set to {PlayerManager.instance.HealthController.GetHealth()}"));
            GetKeyPressToActiveFunction(GetExtraIdentifierForActivation(KeyCode.LeftControl), KeyCode.N,
                () => PlayerManager.instance.HealthController.SetHealth(100),
                () => PlayerManager.instance.HealthController.SetMaxHealth(100),
                () => print("Player Health and Max Health Set to 100"));
            GetKeyPressToActiveFunction(GetExtraIdentifierForActivation(KeyCode.LeftShift), KeyCode.B,
                () => UIManager.instance.SceneTransitionController.LoadScene(),
                () => print("Skipping to the next level"));
            GetKeyPressToActiveFunction(GetExtraIdentifierForActivation(KeyCode.LeftControl), KeyCode.R,
                () => PersistentDataManager.instance.ClearGameData(),
                () => print("Resetting the game data"));
            GetKeyPressToActiveFunction(GetExtraIdentifierForActivation(KeyCode.LeftControl), KeyCode.Alpha0,
                () => PersistentDataManager.DataIntDictionary[PersistentDataManager.DataInts.LevelsUnlocked]++,
                () => print($"Levels Unlocked int increased by 1. " +
                            $"New value: {PersistentDataManager.DataIntDictionary[PersistentDataManager.DataInts.LevelsUnlocked]}"));
            GetKeyPressToActiveFunction(GetExtraIdentifierForActivation(KeyCode.LeftControl), KeyCode.Semicolon,
                () => UIManager.instance.ActivateRewardPanel(),
                () => print("Reward Panel Activated"));

            //Save and Load Tests
            GetKeyPressToActiveFunction(GetExtraIdentifierForActivation(KeyCode.LeftControl), KeyCode.Alpha1, () =>
                GetDevToolBoolToActivateFunction(DevTool.SaveLoadTest, PersistentDataManager.instance.SaveData));
            GetKeyPressToActiveFunction(GetExtraIdentifierForActivation(KeyCode.LeftControl), KeyCode.Alpha2, () =>
                GetDevToolBoolToActivateFunction(DevTool.SaveLoadTest, PersistentDataManager.instance.LoadData));

            //Info Methods
            GetKeyPressToActiveFunction(GetExtraIdentifierForActivation(KeyCode.LeftControl), KeyCode.F12,
                () => print(TickTimeTimer.GetOnTickSubCount()));

            //Do Stuff if bools are true
            GetDevToolBoolToActivateFunction(DevTool.Invulnerability,
                () => PlayerManager.instance.HealthController.SetHealthToMax());
            if (devToolsDictionary[DevTool.UnlimitedAmmo])
                PlayerManager.instance.WeaponManagement.WeaponReload(AmmoHandler.ReloadAmmoAmount.ReloadAllFull);
        }
    }
}