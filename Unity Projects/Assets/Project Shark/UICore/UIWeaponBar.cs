using System.Collections;
using FrikinCore;
using FrikinCore.Player;
using FrikinCore.Player.Inventory;
using FrikinCore.Player.Managers;
using FrikinCore.Player.Weapons;
using TMPro;
using TSCore.Time;
using UnityEngine;
using UnityEngine.UI;

namespace UICore
{
    public class UIWeaponBar : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI laserName;
        [SerializeField] Slider ammoSlider;
        [SerializeField] Image ammoSliderFillImage;
        [SerializeField] Image weaponOutline;
        [SerializeField] Image weaponBackground;

        Color laserNameColor = Color.white;

        void Start()
        {
            WeaponInventory.WeaponChange += OnWeaponChange;
        }

        void OnDisable()
        {
            WeaponInventory.WeaponChange -= OnWeaponChange;
        }

        void Update()
        {
            UpdateSliderValues();
        }

        void OnWeaponChange()
        {
            if (!PlayerManager.IsInitialized) return;
            ChangeWeaponBarImage();
            ShowWeaponName();
            ammoSlider.wholeNumbers = PlayerManager.instance.WeaponManagement.CurrentWeapon.WeaponInfo.UseWholeNumberOnSlider;
        }

        void UpdateSliderValues()
        {
            ammoSlider.maxValue = PlayerManager.instance.WeaponManagement.CurrentWeaponMaxAmmo;

            var ammoSliderValue = 0f;
            if (PlayerManager.instance.Player.FiringController.FireType == PlayerWeapons.FireFunctionType.Charge)
            {
                ammoSliderValue = PlayerManager.instance.Player.FiringController.GetChargeTimer;
            }
            else if (PlayerManager.instance.WeaponManagement.CurrentWeapon.WeaponName == WeaponName.NormalLazer)
            {
                ammoSliderValue = PlayerManager.instance.Player.FiringController.ReloadCheck()
                    ? PlayerManager.instance.Player.FiringController.GetFireTimer * 2
                    : 1;
            }
            else
            {
                ammoSliderValue = PlayerManager.instance.WeaponManagement.CurrentWeaponAmmo;
            }

            ammoSlider.value = ammoSliderValue;
        }

        void ChangeWeaponBarImage()
        {
            weaponOutline.sprite = PlayerManager.instance.WeaponManagement.CurrentWeapon.WeaponInfo.UISprites.uiWeaponOutline;
            ammoSliderFillImage.sprite =
                PlayerManager.instance.WeaponManagement.CurrentWeapon.WeaponInfo.UISprites.uiAmmoSprite;
            weaponBackground.sprite = PlayerManager.instance.WeaponManagement.CurrentWeapon.WeaponInfo.UISprites.uiAmmoSprite;

            ammoSliderFillImage.color = PlayerManager.instance.WeaponManagement.CurrentWeapon.WeaponInfo.WeaponColor;
            weaponBackground.color =
                PlayerManager.instance.WeaponManagement.CurrentWeapon.WeaponInfo.UISprites.uiBackgroundColor;
        }

        void ShowWeaponName()
        {
            laserName.text = PlayerManager.instance.WeaponManagement.CurrentWeapon.WeaponInfo.WeaponNameForUI;
            laserName.color = laserNameColor;
            StopCoroutine(FadeLaserName());
            StartCoroutine(FadeLaserName());
        }

        IEnumerator FadeLaserName()
        {
            float timer = 0;
            while (timer < 1)
            {
                timer += TimeManager.Delta;
                laserName.color = Color.Lerp(laserNameColor, Color.clear, timer);

                yield return new WaitForSeconds(TimeManager.Delta);
            }

            EndLaserNameFade();
        }

        void EndLaserNameFade()
        {
            laserName.color = Color.clear;
        }
    }
}
