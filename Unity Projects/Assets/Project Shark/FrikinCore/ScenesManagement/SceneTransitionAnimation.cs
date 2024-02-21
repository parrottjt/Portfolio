using FrikinCore.Sfx;
using UICore;
using UnityEngine;

namespace FrikinCore.ScenesManagement
{
	public class SceneTransitionAnimation : MonoBehaviour
	{
		public void LevelRewardsActivate()
		{
			UIManager.instance.InfoHolder.rewardPanel.SetActive(true);
			//GameManager.instance.loot.RewardsNumber();
			UIManager.instance.InfoHolder.rewardPanel.GetComponentInChildren<RewardPanelAnimationEvents>()
				.StartRewards();
			SoundManager.instance.PlayBackgroundMusic(SoundManager.instance.menu_LevelComplete_Bgm.clip);
		}
	}
}
