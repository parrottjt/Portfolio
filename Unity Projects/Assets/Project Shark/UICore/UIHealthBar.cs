using System.Collections;
using FrikinCore.Player;
using FrikinCore.Player.Health;
using UnityEngine;
using UnityEngine.UI;

namespace UICore
{
    public class UIHealthBar : MonoBehaviour
    {
        Slider _healthBar;
        Image _healthBarImage;
        PlayerHealthController playerHealthController;

        float _lastHealthValue;

        void Awake()
        {
            _healthBar = GetComponent<Slider>();
            _healthBarImage = _healthBar.image;
            _healthBarImage.material.color = Color.white;
        }

        void Start()
        {
            PlayerHealthController.OnHealthChange += UpdateHealthBarValue;
            PlayerHealthController.OnMaxHealthChange += UpdateHealthBarMaxValue;

            playerHealthController = PlayerManager.instance.HealthController;
            _lastHealthValue = playerHealthController.GetHealth();
            _healthBar.maxValue = playerHealthController.MaxHealth;

            UpdateHealthBarMaxValue();
        }

        void OnDestroy()
        {
            PlayerHealthController.OnHealthChange -= UpdateHealthBarValue;
            PlayerHealthController.OnMaxHealthChange -= UpdateHealthBarMaxValue;
        }

        public void PlayDamageFlash()
        {
            if (_lastHealthValue > _healthBar.value) StartCoroutine(nameof(DamageFlash));
            _lastHealthValue = playerHealthController.GetHealth();
        }

        void UpdateHealthBarMaxValue()
        {
            UpdateHealthBarValue();
            _healthBar.maxValue = PlayerManager.instance.HealthController.MaxHealth;
        }

        void UpdateHealthBarValue()
        {
            _healthBar.value = PlayerManager.instance.HealthController.Health;
        }

        IEnumerator DamageFlash()
        {
            _healthBarImage.color = Color.white;
            yield return new WaitForSeconds(.15f);
            _healthBarImage.color = Color.red;
        }
    }
}