using System.Linq;
//using Rewired;
using UnityEngine;

namespace FrikinCore.Player.Input
{
    public class PlayerInputGatherer : MonoBehaviour
    {
        [SerializeField] int playerID = 0;

        public int GetPlayerID() => playerID;
        //public ControllerMapEnabler GetPlayerMapEnabler() => GetPlayer().controllers.maps.mapEnabler;
        // public Player GetPlayer() => ReInput.players.GetPlayer(playerID);
        // public float GetHorizontalMoveAxis() => GetPlayer().GetAxis("MoveHorizontal");
        // public float GetVerticalMoveAxis() => GetPlayer().GetAxis("MoveVertical");
        // public float GetHorizontalAimAxis() => GetPlayer().GetAxis("AimHorizontal");
        // public float GetVerticalAimAxis() => GetPlayer().GetAxis("AimVertical");
        // public bool GetFireInput() => GetPlayer().GetButton("Fire");
        // public bool GetDashInput() => GetPlayer().GetButton("Dash");
        // public bool GetSprintInput() => GetPlayer().GetButtonTimedPress("Dash", .5f);
        // public bool GetWeaponWheelActiveInput() => GetPlayer().GetButtonLongPress("Weapon Wheel");
        // public bool GetSwapWeaponInput() => GetPlayer().GetButtonShortPressDown("Weapon Wheel");
        // public bool GetWeaponIndexIncreaseInput() => GetPlayer().GetButtonDown("Weapon Index Increase");
        // public bool GetWeaponIndexDecreaseInput() => GetPlayer().GetButtonDown("Weapon Index Decrease");
        // public bool GetOpenMenuInput() => GetPlayer().GetButtonDown("OpenMenu");
        // public bool GetCloseMenuInput() => GetPlayer().GetButtonDown("CloseMenu");
        // public bool GetMenuCancelInput() => GetPlayer().GetButtonDown("Cancel");
        // public bool PlayerHasPressedInputButton() =>
        //     GetDashInput() || GetFireInput() || GetHorizontalMoveAxis() > 0 || GetVerticalMoveAxis() > 0;
        //
        // public void SwitchPlayerInputMapsTo(GameEnums.InputCategories inputCategories)
        // {
        //     var ruleSet = GetPlayerMapEnabler().ruleSets.FirstOrDefault();
        //     foreach (var rule in ruleSet)
        //     {
        //         rule.enable = false;
        //     }
        //     ruleSet.Find(item => item.tag == inputCategories.ToString()).enable = true;
        //     
        //     GetPlayerMapEnabler().Apply();
        //
        //     print($"{GameEnums.InputCategories.Play} set to " +
        //           $"{GetPlayer().controllers.maps.GetMap((int)GameEnums.InputCategories.Play).enabled}");
        //     print($"{GameEnums.InputCategories.Menu} enabled set to " +
        //           $"{GetPlayer().controllers.maps.GetMap((int)GameEnums.InputCategories.Menu).enabled}");
        // }

        void Start()
        {
            //GetPlayer().isPlaying = true;

            //This section is a holdover from multiplayer code, if we decide to have coop then this may need to come back

            //if (playerID != 0)
            //{
            //    ReInput.controllers.AutoAssignJoysticks();
            //    foreach (var joystick in control.controllers.Joysticks)
            //    {
            //        Debug.Log("Player ID " + playerID + " has " + joystick.name);
            //    }
            //}
        }
    }

}