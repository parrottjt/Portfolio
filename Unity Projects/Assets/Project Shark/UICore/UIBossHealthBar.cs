using System;
using FrikinCore;
using FrikinCore.AI.Boss;
using FrikinCore.Player.Health;
using FrikinCore.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UICore
{
    public class UIBossHealthBar : MonoBehaviour
    {
        [Serializable]
        struct BossHealthBarBackgrounds
        {
            public Sprite bossBackgroundImage;
            public Material bossBackgroundMaterial;
        }

        [SerializeField] GameObject _background, _fill;
        [SerializeField] TMP_Text _nameText;
        [SerializeField] Image _bossBackground;

        [SerializeField] BossHealthBarBackgrounds[] _bossHealthBarBackgrounds;
        [SerializeField] BossHealthBarBackgrounds _miniBossHealthBarBackgrounds;

        Slider _healthBarSlider;
        BossHealth bossHealth;
        public Image BossBackground => _bossBackground;

        void Awake()
        {
            _healthBarSlider = GetComponent<Slider>();
        }

        void Start()
        {
            PlayerHealthController.CallOnPlayerDeath += OnPlayerDeath;
        }

        void OnDestroy()
        {
            PlayerHealthController.CallOnPlayerDeath -= OnPlayerDeath;
        }

        void OnPlayerDeath(object sender, PlayerHealthController.PlayerDeathArgs args)
        {
            Invoke(nameof(TurnOffHealthBar), 1f);
        }

        public void TurnOnHealthBar(GameObject bossHolder, bool isMiniBoss = false)
        {
            if (bossHolder == null) return;
            Handlers.SetActiveOnGameObjectsTo(true, gameObject, _nameText.gameObject,
                _background, _fill, _bossBackground.gameObject);

            SetupHealthBarValues(bossHolder, isMiniBoss);
        }

        public void OnBossHealthValueChange()
        {
            if (_healthBarSlider.value > 0) return;
            TurnOffHealthBar();
        }

        void TurnOffHealthBar()
        {
            Handlers.SetActiveOnGameObjectsTo(false, gameObject, _nameText.gameObject,
                _background, _fill, _bossBackground.gameObject);

            if (GameManager.GameSettings[Settings.MiniBossActive])
            {
                var mini = bossHealth as MiniBossHealth;
                mini.DeactiveBoss();
            }

            if (GameManager.GameStatesDictionary[GameStates.BossRush] && _healthBarSlider.value <= 0)
            {
                GameManager.instance.bossRushManager.SetLevelAsCompleted();
                UIManager.instance.SceneTransitionController.SetLevelToLoadNext_BossRush();
            }

            bossHealth.OnHealthChange -= UpdateHealthBar;

        }

        void UpdateHealthBar()
        {
            _healthBarSlider.value = bossHealth.Health;
        }

        void SetupHealthBarValues(GameObject bossHolder, bool isMiniBoss)
        {
            bossHealth = bossHolder.GetComponentInChildren<BossHealth>();

            _healthBarSlider.maxValue = bossHealth.MaxHealth;
            _healthBarSlider.value = bossHealth.Health;
            _nameText.text = bossHealth.GetNameOfBoss();

            _bossBackground.sprite = isMiniBoss
                ? _miniBossHealthBarBackgrounds.bossBackgroundImage
                : _bossHealthBarBackgrounds[GameManager.instance.WorldNumber].bossBackgroundImage;

            _bossBackground.material = isMiniBoss
                ? _miniBossHealthBarBackgrounds.bossBackgroundMaterial
                : _bossHealthBarBackgrounds[GameManager.instance.WorldNumber].bossBackgroundMaterial;

            bossHealth.OnHealthChange += UpdateHealthBar;
        }
    }
}