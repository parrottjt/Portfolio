using System;
using System.Collections;
using System.Collections.Generic;
using FrikinCore;
using FrikinCore.Enumeration;
using FrikinCore.Input;
using FrikinCore.Loot;
using FrikinCore.Save;
using FrikinCore.ScenesManagement;
using TMPro;
using TSCore.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace UICore
{
    public class RewardPanelAnimationEvents : MonoBehaviour
    {
        [Serializable]
        struct ParalaxBackgroundImages
        {
            public Image farBackgroundImage, middleBackgroundImage, closeBackgroundImage;
        }

        [SerializeField] Animation anim;
        [SerializeField] AnimationClip fadeClip, frikinSwimmingClip;
        [SerializeField] TextMeshProUGUI rewardText, rewardValueText;
        [SerializeField] ParalaxBackgroundImages rewardPanelBackgrounds;

        float _timer;

        int methodIndex;
        int totalRewardTeeth;

        bool stopCount;

        public delegate void RewardSwapper();

        readonly List<RewardSwapper> rewardMethods = new ();
        readonly List<LevelReward> activeRewards = new ();

        public void StartRewards()
        {
            stopCount = false;
            UIManager.instance.SceneTransitionController.TimeStop();
            GetActiveRewards();
            SetupBackgroundImageSprites();
            StartCoroutine(RunRewards());
        }

        IEnumerator RunRewards()
        {
            bool startRewards = true;
            bool pauseToRead = true;

            PlayAnimation(anim, frikinSwimmingClip);

            while (startRewards)
            {
                if (stopCount)
                {
                    pauseToRead = true;
                    yield return new WaitForSeconds(1f);
                    _timer = 0;
                    ResetStopCount();
                    if (methodIndex >= rewardMethods.Count)
                    {
                        startRewards = false;
                    }

                    stopCount = false;
                }

                if (pauseToRead)
                {
                    yield return new WaitForSeconds(1f);
                    pauseToRead = false;
                }

                if (!stopCount)
                {
                    _timer += Time.unscaledDeltaTime;
                    if (methodIndex < rewardMethods.Count) rewardMethods[methodIndex]();
                    else
                    {
                        stopCount = true;
                    }
                }

                yield return new WaitForSeconds(Time.deltaTime);
            }

            yield return null;
        }

        void GetActiveRewards()
        {
            for (int i = 0; i < LootManager.levelRewards.Count; i++)
            {
                var levelReward = LootManager.levelRewards[(GameEnums.LevelRewards)i];
                if (levelReward.RewardTriggered)
                {
                    activeRewards.Add(levelReward);
                }
            }

            SetupRewards();
        }

        void SetupRewards()
        {
            if (GameManager.GameSettings[Settings.IsBossScene] == false) rewardMethods.Add(LevelTeethReward);

            foreach (var activeReward in activeRewards)
            {
                rewardMethods.Add(() => IncreaseTeethEarned(activeReward.RewardValue, activeReward.RewardName));
            }

            rewardMethods.Add(IncreaseTotalTeethValue);

            rewardText.text = "Level Teeth Collected";
            rewardValueText.text = GameManager.instance.scoringManagerCode.score.ToString();
        }

        void ResetRewards()
        {
            GameManager.instance.loot.ResetLevelRewardsTrigger();
            rewardMethods.Clear();
            activeRewards.Clear();
        }

        void ResetStopCount()
        {
            if (methodIndex < rewardMethods.Count)
            {
                methodIndex++;
            }

            if (methodIndex >= rewardMethods.Count)
            {
                ResetRewards();
                GameManager.instance.scoringManagerCode.ResetScore();
                PlayAnimation(anim, fadeClip);

                stopCount = false;
                methodIndex = 0;
                _timer = 0;
            }
        }

        void LevelTeethReward()
        {
            var score = GameManager.instance.scoringManagerCode.score;
            IncreaseTeethEarned(score, "Level Teeth Collected");
        }

        void IncreaseTotalTeethValue()
        {
            IncreaseTotalTeeth("Total Teeth");
        }

        void IncreaseTeethEarned(int teethEarnedFromReward, string rewardName)
        {
            rewardText.text = rewardName;

            var lastTotal = totalRewardTeeth;
            var newTotal = teethEarnedFromReward + lastTotal;

            //Helper_Debug.Log_QuickTest();

            var decrementingTotalRewardTeeth = Mathf.Clamp(Mathf.Lerp(teethEarnedFromReward, 0, _timer * .25f), 0,
                teethEarnedFromReward);
            var countingTotal = Mathf.Clamp(Mathf.Lerp(lastTotal, newTotal, _timer * .25f), lastTotal, newTotal);

            UIManager.instance.InfoHolder.teethEarnedPanel.GetComponent<TextMeshProUGUI>().text =
                ((int)countingTotal).ToString();
            rewardValueText.text = ((int)decrementingTotalRewardTeeth).ToString();

            if ((int)countingTotal == newTotal || InputManager.instance.AttackInput())
            {
                totalRewardTeeth = newTotal;
                decrementingTotalRewardTeeth = 0;

                UIManager.instance.InfoHolder.teethEarnedPanel.GetComponent<TextMeshProUGUI>().text =
                    totalRewardTeeth.ToString();
                rewardValueText.text = ((int)decrementingTotalRewardTeeth).ToString();

                stopCount = true;
            }
        }

        void IncreaseTotalTeeth(string rewardName)
        {
            rewardText.text = rewardName;
            if (!stopCount)
            {
                DebugScript.Log_QuickTest(typeof(RewardPanelAnimationEvents));
                var newTotal = totalRewardTeeth +
                               PersistentDataManager.DataIntDictionary[PersistentDataManager.DataInts.TotalTeeth];

                var decrementingTotalRewardTeeth =
                    Mathf.Clamp(Mathf.Lerp(totalRewardTeeth, 0, _timer * .25f), 0, totalRewardTeeth);
                var countingTotal = Mathf.Clamp(Mathf.Lerp(
                        PersistentDataManager.DataIntDictionary[PersistentDataManager.DataInts.TotalTeeth], newTotal,
                        _timer * .25f),
                    PersistentDataManager.DataIntDictionary[PersistentDataManager.DataInts.TotalTeeth], newTotal);

                UIManager.instance.InfoHolder.teethEarnedPanel.GetComponent<TextMeshProUGUI>().text =
                    ((int)decrementingTotalRewardTeeth).ToString();
                rewardValueText.text = ((int)countingTotal).ToString();

                if ((int)countingTotal == newTotal || InputManager.instance.AttackInput())
                {
                    PersistentDataManager.DataIntDictionary[PersistentDataManager.DataInts.TotalTeeth] = newTotal;
                    totalRewardTeeth = 0;

                    UIManager.instance.InfoHolder.teethEarnedPanel.GetComponent<TextMeshProUGUI>().text =
                        totalRewardTeeth.ToString();
                    rewardValueText.text = PersistentDataManager
                        .DataIntDictionary[PersistentDataManager.DataInts.TotalTeeth].ToString();

                    stopCount = true;
                }
            }
        }

        void SetupBackgroundImageSprites()
        {
            rewardPanelBackgrounds.farBackgroundImage.material =
                UIManager.instance.BackgroundMaterialChanger.GetWorldBackgroundMaterials().far;
            rewardPanelBackgrounds.middleBackgroundImage.material =
                UIManager.instance.BackgroundMaterialChanger.GetWorldBackgroundMaterials().mid;
            rewardPanelBackgrounds.closeBackgroundImage.material =
                UIManager.instance.BackgroundMaterialChanger.GetWorldBackgroundMaterials().close;
        }

        void PlayAnimation(Animation _anim, AnimationClip clip)
        {
            _anim.Stop();
            _anim.clip = clip;
            _anim.Play();
        }

        //Animation Functions
        public void NextStep()
        {
            FindObjectOfType<SceneTransitionController>().LoadScene();
        }

        public void TurnOff()
        {
            UIManager.instance.InfoHolder.rewardPanel.SetActive(false);
        }
    }
}