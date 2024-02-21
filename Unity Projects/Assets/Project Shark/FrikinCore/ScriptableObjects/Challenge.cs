using System;
using UnityEngine;

namespace FrikinCore.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Challenge", menuName = "Game Data/Variables/Challenge")]
    public class Challenge : ScriptableObject
    {
        [SerializeField] protected string _challengeName;
        [SerializeField] protected string _challengeDescription;
        [SerializeField] protected int _maxProgress;

        protected bool _isActive;
        protected bool _isCompleted;
        protected bool _rewardCollected;
        protected int _progress;

        public string ChallengeName => _challengeName;
        public string ChallengeDescription => _challengeDescription;
        public bool IsActive => _isActive;
        public bool CanRewardBeCollected => _isCompleted && !_rewardCollected;

        public void SetActive()
        {
            _isActive = !_isCompleted;
        }

        public void IncrementProgress(int amount, bool canIncrement = true)
        {
            if (!canIncrement) return;
            _progress += Mathf.Abs(amount);
            Mathf.Clamp(_progress, 0, _maxProgress);
            CheckChallengeComplete();
        }

        public void DecrementProgress(int amount)
        {
            _progress -= Mathf.Abs(amount);
            Mathf.Clamp(_progress, 0, _maxProgress);
        }

        public void ResetProgress()
        {
            DecrementProgress(_progress);
        }

        public void CollectReward(Action reward)
        {
            if (!CanRewardBeCollected) return;
            reward?.Invoke();
            _rewardCollected = true;
        }

        void CheckChallengeComplete()
        {
            if (_progress >= _maxProgress)
            {
                _isCompleted = true;
                SetActive();
            }
        }
    }
}