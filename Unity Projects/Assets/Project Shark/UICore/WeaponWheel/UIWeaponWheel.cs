using FrikinCore.Input;
using FrikinCore.Player;
using UnityEngine;
using UnityEngine.UI;

namespace UICore.WeaponWheel
{
    public class UIWeaponWheel : MonoBehaviour
    {
        [Header("Radial Weapon Wheel Info")] [SerializeField]
        GameObject weaponWheel;

        [SerializeField] GameObject normalSlot;

        GameObject selectedSlot;
        bool raycastTargetIsBeenSet, weaponWheelHasBeenSetup;

        CircleCreater weaponWheelCreator;
        Image normalSlotImage;
        WeaponObjectSelect normalSlotSelect;

        Image[] weaponSlotImages;
        WeaponObjectSelect[] weaponSlotObjectSelects;
        FinderScriptForWeaponWheelSlots[] weaponSlots;

        int maxWeaponsInWheel;

        [Header("Slider Weapon Wheel")] [SerializeField]
        GameObject weaponWheelHolder;

        [SerializeField] Image currentWeaponEquipped, oneWeaponForward, twoWeaponForward, oneWeaponBack;

        void Awake()
        {
            // weaponWheelCreator = weaponWheel.GetComponentInChildren<CircleCreater>();
            // normalSlotImage = normalSlot.GetComponent<Image>();
            // normalSlotSelect = normalSlot.GetComponent<WeaponObjectSelect>();
            //
            // selectedSlot = weaponWheel;
            //
            // weaponWheel.SetActive(false);
        }

        void Update()
        {
            //WeaponWheelRadial();
            WeaponWheelSliding();
        }

        #region Radial Weapon Wheel

        // public void SelectWeapon(GameObject test)
        // {
        //     selectedSlot = test;
        //     for (int i = 0; i < PlayerManager.instance.WeaponManagement.ListOfPlayerWeaponsReadOnly.Count; i++)
        //     {
        //         if (selectedSlot.GetComponent<WeaponObjectSelect>().newWeapon ==
        //             PlayerManager.instance.WeaponManagement.ListOfPlayerWeaponsReadOnly[i])
        //         {
        //             PlayerManager.instance.WeaponManagement.UpdateWeaponIndex(i);
        //             PlayerManager.instance.WeaponManagement.quickSwapLocked = true;
        //         }
        //     }
        // }
        //
        // void WeaponWheelRadial()
        // {
        //     maxWeaponsInWheel = PlayerManager.instance.WeaponManagement.NumberOfWeaponsPlayerHas - 1;
        //     if (EventSystem.current != null)
        //         EventSystem.current.sendNavigationEvents = !InputManager.instance.GetWeaponWheelActiveInput();
        //     if (InputManager.instance.GetWeaponWheelActiveInput())
        //     {
        //         weaponWheel.SetActive(true);
        //         normalSlotImage.sprite = PlayerManager.instance.WeaponManagement.GetPlayerWeaponFromIndex(0).weaponGameSprite;
        //         normalSlotSelect.UpdateWeapon(PlayerManager.instance.WeaponManagement.GetPlayerWeaponFromIndex(0));
        //
        //         if (weaponWheelCreator.numberOfCircles != maxWeaponsInWheel)
        //         {
        //             WeaponWheelResetFunction();
        //         }
        //         else WeaponWheelSetup();
        //
        //         if (weaponWheelHasBeenSetup)
        //         {
        //             if (JP_MenuManager.instance.useController)
        //             {
        //                 ControllerWeaponSelection(maxWeaponsInWheel,
        //                     GameManager.instance.playerCode.GetLookAngle(),
        //                     GameManager.instance.playerCode.GetStickReset());
        //             }
        //             else
        //             {
        //                 if (!raycastTargetIsBeenSet)
        //                 {
        //                     normalSlotImage.raycastTarget = !JP_MenuManager.instance.useController;
        //                     foreach (var t in weaponSlotImages)
        //                     {
        //                         t.raycastTarget = !JP_MenuManager.instance.useController;
        //                     }
        //
        //                     raycastTargetIsBeenSet = true;
        //                 }
        //             }
        //         }
        //     }
        //     else
        //     {
        //         weaponWheel.SetActive(false);
        //         raycastTargetIsBeenSet = false;
        //     }
        // }
        //
        // void WeaponWheelResetFunction()
        // {
        //     weaponWheelHasBeenSetup = false;
        //     if (weaponSlots.Length > 0)
        //     {
        //         foreach (var t in weaponSlots)
        //         {
        //             if (t != null) Destroy(t.gameObject);
        //         }
        //     }
        //
        //     Array.Resize(ref weaponSlots, 0);
        //     weaponWheelCreator.ResetCount();
        //     weaponWheelCreator.numberOfCircles = maxWeaponsInWheel;
        // }
        //
        // void WeaponWheelSetup()
        // {
        //     if (GameManager.GameStatesDictionary[GameStates.Menu]) return;
        //     if (!weaponWheelHasBeenSetup)
        //     {
        //         weaponSlots = weaponWheelCreator.GetComponentsInChildren<FinderScriptForWeaponWheelSlots>();
        //         weaponSlotImages = new Image[weaponSlots.Length];
        //         weaponSlotObjectSelects = new WeaponObjectSelect[weaponSlots.Length];
        //         for (int i = 0; i < weaponSlots.Length; i++)
        //         {
        //             weaponSlots[i].GetComponent<Image>().sprite =
        //                 PlayerManager.instance.WeaponManagement.ListOfPlayerWeaponsReadOnly[i + 1].weaponGameSprite;
        //             weaponSlots[i].GetComponent<WeaponObjectSelect>()
        //                 .UpdateWeapon(PlayerManager.instance.WeaponManagement.ListOfPlayerWeaponsReadOnly[i + 1]);
        //         }
        //
        //         for (int i = 0; i < weaponSlots.Length; i++)
        //         {
        //             weaponSlotImages[i] = weaponSlots[i].GetComponent<Image>();
        //             weaponSlotObjectSelects[i] = weaponSlots[i].GetComponent<WeaponObjectSelect>();
        //         }
        //
        //         if (weaponSlots.Length > 0)
        //         {
        //             weaponWheelHasBeenSetup = true;
        //         }
        //     }
        // }
        //
        // void ControllerWeaponSelection(int slotCount, float rightStickAngle, bool stickReset)
        // {
        //     switch (slotCount)
        //     {
        //         #region Just Normal Lazer
        //
        //         default:
        //             PlayerManager.instance.WeaponManagement.UpdateWeaponIndex(0);
        //             EventSystem.current.SetSelectedGameObject(normalSlot);
        //             break;
        //
        //         #endregion
        //
        //         #region 1 Wheel Slot
        //
        //         case 1:
        //             StickAnglesAnd(rightStickAngle, -15, 15, 0);
        //             break;
        //
        //         #endregion
        //
        //         #region 2 Wheel Slots
        //
        //         case 2:
        //             StickAnglesAnd(rightStickAngle, -15, 15, 1);
        //             StickAnglesOr(rightStickAngle, 165, -165, 0);
        //             if (rightStickAngle <= 15 && rightStickAngle >= -15)
        //             {
        //                 EventSystem.current.SetSelectedGameObject(weaponSlots[1].gameObject);
        //             }
        //
        //             if (rightStickAngle <= -165 || rightStickAngle >= 165)
        //             {
        //                 EventSystem.current.SetSelectedGameObject(weaponSlots[0].gameObject);
        //             }
        //
        //             break;
        //
        //         #endregion
        //
        //         #region 3 Wheel Slots
        //
        //         case 3:
        //             if (rightStickAngle <= 15 && rightStickAngle >= -15)
        //             {
        //                 EventSystem.current.SetSelectedGameObject(weaponSlots[2].gameObject);
        //             }
        //
        //             if (rightStickAngle <= 130 && rightStickAngle >= 100)
        //             {
        //                 EventSystem.current.SetSelectedGameObject(weaponSlots[1].gameObject);
        //             }
        //
        //             if (rightStickAngle <= -110 && rightStickAngle >= -140)
        //             {
        //                 EventSystem.current.SetSelectedGameObject(weaponSlots[0].gameObject);
        //             }
        //             else if (stickReset)
        //             {
        //                 EventSystem.current.SetSelectedGameObject(normalSlot);
        //             }
        //
        //             break;
        //
        //         #endregion
        //
        //         #region 4 Wheel Slots
        //
        //         case 4:
        //             if (rightStickAngle <= 15 && rightStickAngle >= -15)
        //             {
        //                 EventSystem.current.SetSelectedGameObject(weaponSlots[3].gameObject);
        //             }
        //
        //             if (rightStickAngle <= 105 && rightStickAngle >= 75)
        //             {
        //                 EventSystem.current.SetSelectedGameObject(weaponSlots[2].gameObject);
        //             }
        //
        //             if (rightStickAngle <= -165 || rightStickAngle >= 165)
        //             {
        //                 EventSystem.current.SetSelectedGameObject(weaponSlots[1].gameObject);
        //             }
        //
        //             if (rightStickAngle <= -75 && rightStickAngle >= -105)
        //             {
        //                 EventSystem.current.SetSelectedGameObject(weaponSlots[0].gameObject);
        //             }
        //             else if (stickReset)
        //             {
        //                 EventSystem.current.SetSelectedGameObject(normalSlot);
        //             }
        //
        //             break;
        //
        //         #endregion
        //
        //         #region 5 Wheel Slots
        //
        //         case 5:
        //             if (rightStickAngle <= 15 && rightStickAngle >= -15)
        //             {
        //                 EventSystem.current.SetSelectedGameObject(weaponSlots[4].gameObject);
        //             }
        //
        //             if (rightStickAngle <= 90 && rightStickAngle >= 60)
        //             {
        //                 EventSystem.current.SetSelectedGameObject(weaponSlots[3].gameObject);
        //             }
        //
        //             if (rightStickAngle <= 165 && rightStickAngle >= 135)
        //             {
        //                 EventSystem.current.SetSelectedGameObject(weaponSlots[2].gameObject);
        //             }
        //
        //             if (rightStickAngle <= -120 && rightStickAngle >= -150)
        //             {
        //                 EventSystem.current.SetSelectedGameObject(weaponSlots[1].gameObject);
        //             }
        //
        //             if (rightStickAngle <= -45 && rightStickAngle >= -75)
        //             {
        //                 EventSystem.current.SetSelectedGameObject(weaponSlots[0].gameObject);
        //             }
        //
        //             break;
        //
        //         #endregion
        //
        //         #region 6 Wheel Slots
        //
        //         case 6:
        //             if (rightStickAngle <= 15 && rightStickAngle >= -15)
        //             {
        //                 EventSystem.current.SetSelectedGameObject(weaponSlots[5].gameObject);
        //             }
        //
        //             if (rightStickAngle <= 80 && rightStickAngle >= 50)
        //             {
        //                 EventSystem.current.SetSelectedGameObject(weaponSlots[4].gameObject);
        //             }
        //
        //             if (rightStickAngle <= 115 && rightStickAngle >= 85)
        //             {
        //                 EventSystem.current.SetSelectedGameObject(weaponSlots[3].gameObject);
        //             }
        //
        //             if (rightStickAngle <= -165 || rightStickAngle >= 165)
        //             {
        //                 EventSystem.current.SetSelectedGameObject(weaponSlots[2].gameObject);
        //             }
        //
        //             if (rightStickAngle <= -85 && rightStickAngle >= -115)
        //             {
        //                 EventSystem.current.SetSelectedGameObject(weaponSlots[1].gameObject);
        //             }
        //
        //             if (rightStickAngle <= -50 && rightStickAngle >= -85)
        //             {
        //                 EventSystem.current.SetSelectedGameObject(weaponSlots[0].gameObject);
        //             }
        //             else if (stickReset)
        //             {
        //                 EventSystem.current.SetSelectedGameObject(normalSlot);
        //             }
        //
        //             break;
        //
        //         #endregion
        //
        //         #region 7 Wheel Slots
        //
        //         case 7:
        //             if (rightStickAngle <= 15 && rightStickAngle >= -15)
        //             {
        //                 EventSystem.current.SetSelectedGameObject(weaponSlots[6].gameObject);
        //             }
        //
        //             if (rightStickAngle <= 70 && rightStickAngle >= 40)
        //             {
        //                 EventSystem.current.SetSelectedGameObject(weaponSlots[5].gameObject);
        //             }
        //
        //             if (rightStickAngle <= 105 && rightStickAngle >= 75)
        //             {
        //                 EventSystem.current.SetSelectedGameObject(weaponSlots[4].gameObject);
        //             }
        //
        //             if (rightStickAngle <= 160 && rightStickAngle >= 130)
        //             {
        //                 EventSystem.current.SetSelectedGameObject(weaponSlots[3].gameObject);
        //             }
        //
        //             if (rightStickAngle <= -131 && rightStickAngle >= -161)
        //             {
        //                 EventSystem.current.SetSelectedGameObject(weaponSlots[2].gameObject);
        //             }
        //
        //             if (rightStickAngle <= -77 && rightStickAngle >= -107)
        //             {
        //                 EventSystem.current.SetSelectedGameObject(weaponSlots[1].gameObject);
        //             }
        //
        //             if (rightStickAngle <= -41 && rightStickAngle >= -71)
        //             {
        //                 EventSystem.current.SetSelectedGameObject(weaponSlots[0].gameObject);
        //             }
        //             else if (stickReset)
        //             {
        //                 EventSystem.current.SetSelectedGameObject(normalSlot);
        //             }
        //
        //             break;
        //
        //         #endregion
        //
        //         #region 8 Wheel Slots
        //
        //         case 8:
        //             StickAnglesAnd(rightStickAngle, 15, -15, 0);
        //             StickAnglesAnd(rightStickAngle, 70, 40, 1);
        //             StickAnglesAnd(rightStickAngle, 105, 75, 2);
        //             StickAnglesAnd(rightStickAngle, 135, 105, 3);
        //             StickAnglesOr(rightStickAngle, -165, 165, 4);
        //             StickAnglesAnd(rightStickAngle, -115, -145, 5);
        //             StickAnglesAnd(rightStickAngle, -75, -105, 6);
        //             StickAnglesAnd(rightStickAngle, -30, -55, 7);
        //             break;
        //
        //         #endregion
        //     }
        //
        //     if (stickReset)
        //     {
        //         ResetToDefaultPosition();
        //     }
        // }
        //
        // void StickAnglesAnd(float angle, float lessThan, float greaterThan, int slotId)
        // {
        //     if (angle <= lessThan && angle >= greaterThan)
        //     {
        //         EventSystem.current.SetSelectedGameObject(weaponSlots[slotId].gameObject);
        //     }
        // }
        //
        // void StickAnglesOr(float angle, float lessThan, float greaterThan, int slotId)
        // {
        //     if (angle <= lessThan || angle >= greaterThan)
        //     {
        //         EventSystem.current.SetSelectedGameObject(weaponSlots[slotId].gameObject);
        //     }
        // }
        //
        // void ResetToDefaultPosition() => EventSystem.current.SetSelectedGameObject(normalSlot);

        #endregion

        #region Sliding Weapon Wheel

        void WeaponWheelSliding()
        {
            if (InputManager.instance.WeaponWheelActiveInput())
            {
                weaponWheelHolder.SetActive(true);
                TestWeaponWheelSlidingFunctionality();
            }
            else
            {
                weaponWheelHolder.SetActive(false);
            }
        }

        void TestWeaponWheelSlidingFunctionality()
        {
            currentWeaponEquipped.sprite = GetWeaponSprite(0);
            oneWeaponBack.sprite = GetWeaponSprite(-1);
            oneWeaponForward.sprite = GetWeaponSprite(1);
            twoWeaponForward.sprite = GetWeaponSprite(2);
        }

        Sprite GetWeaponSprite(int weaponIndexPositionAdditive)
        {
            // int weaponIndexPosition = PlayerManager.instance.WeaponManagement.WeaponIndex + weaponIndexPositionAdditive;
            // if (weaponIndexPosition == PlayerManager.instance.WeaponManagement.NumberOfWeaponsPlayerHas)
            // {
            //     weaponIndexPosition = 0;
            // }
            //
            // if (weaponIndexPosition == PlayerManager.instance.WeaponManagement.NumberOfWeaponsPlayerHas + 1)
            // {
            //     weaponIndexPosition = 1;
            // }
            //
            // if (weaponIndexPosition < 0)
            // {
            //     weaponIndexPosition = PlayerManager.instance.WeaponManagement.ListOfPlayerWeaponsReadOnly.Count - 1;
            // }
            //
            // return PlayerManager.instance.WeaponManagement
            //     .ListOfPlayerWeaponsReadOnly[weaponIndexPosition].WeaponGameSprite;
            return PlayerManager.instance.Inventory.Weapon.CurrentWeapon.WeaponInfo.GameSprite;
        }

        void OnWeaponAddition()
        {
            // if (PlayerManager.instance.WeaponManagement.ListOfPlayerWeaponsReadOnly.Count < 2) return;
            // oneWeaponBack.gameObject.SetActive(true);
            // oneWeaponBack.gameObject.SetActive(true);
            // if (PlayerManager.instance.WeaponManagement.ListOfPlayerWeaponsReadOnly.Count >= 3)
            // {
            //     twoWeaponForward.gameObject.SetActive(true);
            // }
        }

        #endregion
    }
}