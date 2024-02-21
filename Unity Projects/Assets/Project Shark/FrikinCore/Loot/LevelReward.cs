namespace FrikinCore.Loot
{
	public class LevelReward
	{
		public readonly string RewardName;
		public int RewardValue { get; private set; }
		public bool RewardTriggered { get; private set; }
		bool rewardTriggerBaseValue;

		public LevelReward(string rewardName, int rewardValue, bool rewardTriggerBaseValue = true)
		{
			RewardName = rewardName;
			RewardValue = rewardValue;
			this.rewardTriggerBaseValue = rewardTriggerBaseValue;
		}
		
		public void TriggerReward() => RewardTriggered = true;
		public void RevokeReward() => RewardTriggered = false;
		public void ResetRewardTrigger() => RewardTriggered = rewardTriggerBaseValue;
		public void UpdateRewardValue(int value) => RewardValue = value;
	}
}
