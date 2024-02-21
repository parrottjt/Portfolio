using System;
using FrikinCore.Save;
using UnityEngine;

namespace FrikinCore.Score
{
    public static class ScoreHandler
    {
        public static event Action OnScoreChange;

        public enum ScoreValues
        {
            Zero = 0,
            One = 1, //tooth
            Five = 5, //teeth
            Ten = 10, //bronze tooth
            TwentyFive = 25, //silver tooth
            Fifty = 50, //Bronze Teeth
            OneHundred = 100, // gold tooth
            OneHundredTwentyFive = 125, //Silver Teeth
            FiveHundred = 500, //Gold Teeth
        }

        public static void AddScore(ScoreValues scoreValue)
        {
            var absoluteValue = Mathf.Abs((int)scoreValue);
            UpdatePlayerScore(absoluteValue);
        }

        public static void RemoveScore(ScoreValues scoreValue) => RemoveScore((int)scoreValue);

        public static void RemoveScore(int scoreValue)
        {
            var absoluteValue = -Mathf.Abs(scoreValue);
            UpdatePlayerScore(absoluteValue);
        }

        static void UpdatePlayerScore(int value)
        {
            PersistentDataManager.DataIntDictionary[PersistentDataManager.DataInts.TotalTeeth] += value;
            GameManager.instance.scoringManagerCode.score += value;
            OnScoreChange?.Invoke();
        }
    }
}
