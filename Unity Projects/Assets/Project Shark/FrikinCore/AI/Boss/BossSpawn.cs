using FrikinCore.Sfx;
using UICore;
using UnityEngine;
using UnityEngine.Serialization;

namespace FrikinCore.AI.Boss
{
    public class BossSpawn : MonoBehaviour
    {

        [FormerlySerializedAs("Boss")] public GameObject boss;
        UIBossHealthBar bossHealthBar;

        // Use this for initialization
        void Start()
        {
            bossHealthBar = UIManager.instance.InfoHolder.bossHealthBar.GetComponent<UIBossHealthBar>();
            bossHealthBar.gameObject.SetActive(false);
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag(GameTags.Player.ToString()))
            {
                if (boss != null)
                {
                    SoundManager.instance.PlayBackgroundMusic(SoundManager
                        ._bossBgmClips[GameManager.instance.BossMusicNumber].clip);
                    boss.SetActive(true);
                    bossHealthBar.TurnOnHealthBar(boss);
                }
            }
        }
    }
}
