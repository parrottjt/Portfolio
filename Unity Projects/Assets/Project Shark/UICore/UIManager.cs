using FrikinCore;
using FrikinCore.Input;
using FrikinCore.ScenesManagement;
using TSCore.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UICore
{
	public class UIManager : Singleton<UIManager>
	{
		public BackgroundMaterialChanger BackgroundMaterialChanger;
		public PauseMenu PauseMenu;
		public SceneTransitionController SceneTransitionController;
		public RewardPanelAnimationEvents RewardPanelAnimationEvents;
		public InfoHolder_IGM InfoHolder;
		public UIBossHealthBar BossHealthBar;
		public UIAddTransparentToSection UITransparentSection;
		public UIPlayerHUDController UiPlayerHudController;

		[SerializeField] EventSystem eventSystem;

		public EventSystem EventSystem => eventSystem;

		protected override void Awake()
		{
			base.Awake();
			DontDestroyOnLoad(gameObject);
		}

		void Update()
		{
			//todo: IS THE MENU MANAGER NEEDED
			Cursor.visible = InputManager.instance.ControllerInUse == false;
		}

		public void ActivateRewardPanel()
		{
			InfoHolder.rewardPanel.SetActive(!InfoHolder.rewardPanel.activeSelf);
			//GameManager.instance.loot.RewardsNumber();
			InfoHolder.rewardPanel.GetComponentInChildren<RewardPanelAnimationEvents>().StartRewards();
		}
	}
}
