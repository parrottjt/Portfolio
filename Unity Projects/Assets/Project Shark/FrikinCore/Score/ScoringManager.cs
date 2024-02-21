using UICore;
using UnityEngine;

namespace FrikinCore.Score
{
    public class ScoringManager : MonoBehaviour
    {
        public int score;

        [SerializeField] public Animator exAnim;

        void Start() => ScoreHandler.OnScoreChange += UpdateUIScore;
        void OnDestroy() => ScoreHandler.OnScoreChange -= UpdateUIScore;

        void UpdateUIScore()
        {
            UIManager.instance.InfoHolder.teethText.text = score.ToString();
        }

        public void IncreaseScore(int amount)
        {
            //increment the score when an enemy dies or pickup
            //make this an int function
            score += amount;
        }

        public void RemovePercentage(float percent)
        {
            var percentToRemove = 1 - percent;
            var newTotal = score * percentToRemove;
            score = (int)newTotal;
        }

        public void ResetScore()
        {
            score = 0;
        }
    }
}