using UnityEngine;
using FrikinCore.Player;

namespace FrikinCore.AI.Boss
{
	public class BossReset : MonoBehaviour
	{

		public GameObject bossPrefab;
		private GameObject boss;
		public bool triggerOnce = false;

		// Use this for initialization
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{
			if (PlayerManager.instance.HealthController.GetHealth() <= 0.0f && triggerOnce == false)
			{
				Invoke("BossRespawn", 2.2f);
				triggerOnce = true;
			}

			boss = GameObject.FindGameObjectWithTag("Boss");
		}

		public void BossRespawn()
		{
			Destroy(boss);
			GameObject Octoboss = (GameObject)Instantiate(bossPrefab, transform.position, transform.rotation);
			Invoke("BossDisable", 0.3f);
		}

		public void BossDisable()
		{
			boss.SetActive(false);
			GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");
			foreach (GameObject projectile in projectiles)
				Destroy(projectile);
			triggerOnce = false;
		}
	}
}
