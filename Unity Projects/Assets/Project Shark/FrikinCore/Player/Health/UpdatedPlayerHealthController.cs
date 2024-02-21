using System;
using FrikinCore.Enumeration;
using FrikinCore.Interfaces;
using FrikinCore.Sfx;
using TSCore.Utils.GameEvent;
using UnityEngine;

namespace FrikinCore.Player.Health
{
    public class UpdatedPlayerHealthController : MonoBehaviour, ITakeDamage<int>, IHaveHealthBar<int>
    {
        [SerializeField] GameEvent _onPlayerDeath;

        static PlayerHealth PlayerHealth => PlayerManager.instance.Player.PlayerHealth;

        public static event Action OnDamageTaken;
        public static event Action OnPlayerDeath;
        
        public int Health => PlayerHealth.Health;
        public int MaxHealth => PlayerHealth.MaxHealth;
        
        void Start()
        {
            OnDamageTaken += DamageTaken;
            OnPlayerDeath += PlayerDeath;
        }

        void OnDestroy()
        {
            OnDamageTaken -= DamageTaken;
            OnPlayerDeath -= PlayerDeath;
        }
        
        public void TakeDamage(int damage, bool cutThroughArmor = false)
        {
            if (PlayerHealth.Health == 0) return;
            if (!PlayerManager.instance.Player.IsInvulnerable) return;
            
            PlayerHealth.TakeDamage(damage, cutThroughArmor);
            OnDamageTaken?.Invoke();
            
            if (PlayerHealth.Health <= 0)
            {
                OnPlayerDeath?.Invoke();
                _onPlayerDeath.Raise();
            }
        }
        void DamageTaken()
        {
            PlayerManager.instance.Player.PlayerInvulnerability.TurnInvulnerable();
            PlayerManager.instance.Player.Animator.SetBool(PlayerDataModel.IsRespawningHash, true);
            SoundManager.instance.RandomizeSfx(SoundManager.instance.PlayerDamagedSfx, 
                SoundManager.instance.PlayerDamaged2Sfx);
        }

        void PlayerDeath()
        {
            SoundManager.instance.RandomizeSfx(SoundManager.instance.DeathSoundSfx);
            PlayerManager.instance.Player.PlayerMoveSpeed.ActivateStatusEffect(GameEnums.PlayerMovementEffects
                .Death);
            
        }
    }
}
