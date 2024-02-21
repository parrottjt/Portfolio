using System;
using FrikinCore;
using UnityEngine;

namespace UICore
{
    //todo: Split this into two scripts one for UI and one for Background
    public class BackgroundMaterialChanger : MonoBehaviour
    {
        [Serializable]
        public struct MaterialsArray
        {
            public Material mid, far, close;
        }

        [SerializeField] MaterialsArray[] worldBackgroundMaterials;

        [SerializeField] Sprite[] pauseMenuBorderSprites;

        public MaterialsArray GetWorldBackgroundMaterials() =>
            worldBackgroundMaterials[GameManager.instance.WorldNumber];

        public Sprite GetWorldPauseMenuBorderSprite() => pauseMenuBorderSprites[GameManager.instance.WorldNumber];
    }
}