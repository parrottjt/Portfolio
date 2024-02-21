using System.Linq;
using FrikinCore.ScenesManagement;
using TSCore.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FrikinCore.Managers
{
	public class BossRushManager : MonoBehaviour
	{
		bool[] completedScenes;
		[SerializeField] int numberOfScenesToComplete;

		bool CheckCompletedCount()
		{
			int count = completedScenes.Count(t => t);
			DebugScript.Log(nameof(BossRushManager), count.ToString());
			return count >= numberOfScenesToComplete + 1;
		}

		public string NextLevelName()
		{
			return SceneManagement.ChooseNextScene_BossRush(CheckCompletedCount(), completedScenes);
		}

		public void SetLevelAsCompleted() =>
			completedScenes[GameManager.gameStateLevelInfo[SceneManager.GetActiveScene().name].StateSceneNumber] = true;

		void EnablePlayerModifersForBossRush()
		{

		}

		void OnEnable()
		{
			completedScenes = new bool[11];
			for (var index = 0; index < completedScenes.Length; index++)
			{
				completedScenes[index] = false;
			}
		}

		void OnDisable()
		{
			completedScenes = new bool[11];
		}
	}
}
