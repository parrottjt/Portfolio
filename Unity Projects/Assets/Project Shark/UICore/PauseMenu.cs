using System;
using System.Collections.Generic;
using FrikinCore;
using FrikinCore.Input;
using FrikinCore.Loot.Teeth;
using FrikinCore.ScenesManagement;
using UnityEngine;
using UnityEngine.UI;

namespace UICore
{
    public class PauseMenu : MonoBehaviour
    {
        [Serializable]
        struct PanelAndButtonCombo
        {
            public GameObject panel;
            public Button panelActivationButton;
        }

        readonly Stack<GameObject> _activePanels = new Stack<GameObject>();

        [SerializeField] GameObject inGameMenu, sidePanel;
        [SerializeField] List<PanelAndButtonCombo> PanelObjects;

        public GameObject PauseMenuPanel => inGameMenu;
        public GameObject CurrentActivePanel => _activePanels.Count > 0 ? _activePanels.Peek() : null;

        //todo: is the Menu Game state still needed
        bool CanOpenPause => PauseManager.IsPaused() == false &&
                             GameManager.GameStatesDictionary[GameStates.Menu] == false;

        bool PauseMenuActive => inGameMenu.activeInHierarchy;

        void Awake() => inGameMenu.SetActive(false);

        void Update()
        {
            if (InputManager.instance.OpenMenuInput()) OpenPauseMenu();
            if (PauseMenuActive == false) return;
            if (InputManager.instance.CloseMenuInput()) ClosePauseMenu();
            if (InputManager.instance.MenuCancelInput()) CancelButton();
        }

        public void OpenMenu(GameObject panel)
        {
            if (_activePanels.Contains(panel)) return;
            if (CurrentActivePanel != null) CurrentActivePanel.SetActive(false);
            _activePanels.Push(panel);
            CurrentActivePanel.SetActive(true);
        }

        public void CloseMenu()
        {
            CurrentActivePanel.SetActive(false);
            _activePanels.Pop();
            if (CurrentActivePanel != null) CurrentActivePanel.SetActive(true);
        }

        void CancelButton()
        {
            if (CurrentActivePanel == sidePanel) ClosePauseMenu();
            else
            {
                CloseMenu();
                WithControllerUseSetSelectedGameObject(PanelObjects.Find(combo =>
                    combo.panel == CurrentActivePanel).panelActivationButton.gameObject);
            }
        }

        public void OpenPauseMenu()
        {
            if (CanOpenPause == false) return;
            SetPauseMenuActive(true);
            PauseManager.Pause();
            GameManager.GameSettings[Settings.PauseMenu] = true;
            //InputManager.instance.EnterInputMap(GameEnums.InputCategories.Menu);
            OpenMenu(sidePanel);
        }

        void ClosePauseMenu()
        {
            //InputManager.instance.LeaveInputMap(GameEnums.InputCategories.Menu);
            SetPauseMenuActive(false);
            GameManager.GameSettings[Settings.PauseMenu] = false;
            PauseManager.Unpause();

            while (_activePanels.Count > 0)
            {
                CloseMenu();
            }
        }

        void WithControllerUseSetSelectedGameObject(GameObject button) =>
            UIManager.instance.EventSystem.SetSelectedGameObject(button);

        void SetPauseMenuActive(bool value) => inGameMenu.SetActive(value);

        //OnClickButtonFunctions
        public void ResumeButton() => ClosePauseMenu();

        public void RestartButton()
        {
            UIManager.instance.SceneTransitionController.LoadSpecificScene(SceneManagement.CurrentLevelName);
            GameManager.GameSettings[Settings.HasSceneRestarted] = false;
            GameManager.GameSettings[Settings.MiniBossActive] = false;
            ClosePauseMenu();
        }

        public void MainMenuButton()
        {
            UIManager.instance.SceneTransitionController.LoadSpecificScene(SceneManagement.MainMenuName);
            ClosePauseMenu();
            ToothPlacement.DeactivateCurrentLevelTeeth();
        }
    }
}