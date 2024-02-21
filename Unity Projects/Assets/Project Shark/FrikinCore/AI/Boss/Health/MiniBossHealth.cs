using System;
using UnityEngine;

namespace FrikinCore.AI.Boss
{
    public class MiniBossHealth : BossHealth
    {
        [SerializeField] GameObject _exitPortal;
        Vector3 _startPos;
        Action _functionForBossDeath, _methodForPlayerDeath;

        bool _isExitPortalNotNull;

        void Start()
        {
            _isExitPortalNotNull = _exitPortal != null;
            if (GameManager.GameStatesDictionary[GameStates.Story])
            {
                GameManager.GameSettings[Settings.MiniBossActive] = true;
            }

            _startPos = transform.position;
        }

        public override void TakeDamage(float amount, bool cutThroughArmor = false)
        {
            base.TakeDamage(amount, cutThroughArmor);

            if (!(Health <= 0)) return;
            _functionForBossDeath?.Invoke();
        }

        public void DeclareMethodForBossDeath(Action functionForBossDeath) =>
            _functionForBossDeath = functionForBossDeath;

        public void DeclareMethodForPlayerDeath(Action methodForPlayerDeath) =>
            _methodForPlayerDeath = methodForPlayerDeath;

        public void DeactiveBoss()
        {
            gameObject.transform.position = _startPos;
            gameObject.SetActive(false);
            if (Health > 0)
            {
                ResetPhaseNumber();
                GameManager.GameSettings[Settings.MiniBossActive] = false;
                _methodForPlayerDeath?.Invoke();
            }
            else
            {
                _functionForBossDeath?.Invoke();
                if (_isExitPortalNotNull)
                {
                    _exitPortal.SetActive(true);
                }
            }
        }

        public void HealBoss()
        {
            FullyHeal();
        }

        public void ResetMiniBossValues()
        {
            if (gameObject.name == "SenseiStarfish")
            {
                //Todo Sensei is need for this
                //gameObject.GetComponent<SSF_Controller>().SenseiResetValues();
            }
        }
    }
}