using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace FrikinCore.Player.Weapons
{
    public class PlayerWeaponListInformation : MonoBehaviour
    {
        [SerializeField] PlayerWeapons normalLaser;
        [SerializeField] List<PlayerWeapons> storyWeaponList;
        [SerializeField] List<PlayerWeapons> progenWeaponList;

        public ReadOnlyCollection<PlayerWeapons> WeaponList(GameStates gameState)
        {
            switch (gameState)
            {
                case GameStates.Story:
                case GameStates.BossRush:
                    return storyWeaponList.AsReadOnly();
                case GameStates.ProGen:
                    return progenWeaponList.AsReadOnly();
                default:
                    return storyWeaponList.AsReadOnly();
            }
        }

        public PlayerWeapons NormalLaser => normalLaser;
        public ReadOnlyCollection<PlayerWeapons> StoryWeaponList => storyWeaponList.AsReadOnly();
        public ReadOnlyCollection<PlayerWeapons> ProgenWeaponList => progenWeaponList.AsReadOnly();
    }
}