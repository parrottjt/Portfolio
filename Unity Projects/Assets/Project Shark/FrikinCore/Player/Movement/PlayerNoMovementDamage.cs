using TSCore.Time;
using UnityEngine;
using UnityEngine.Serialization;

namespace FrikinCore.Player.Movement
{
    [RequireComponent(typeof(PlayerDataModel))]
    public class PlayerNoMovementDamage : MonoBehaviour
    {
        PlayerDataModel _model;

        [FormerlySerializedAs("damTime")] [SerializeField]
        int _damageMaxTick = 30;

        int _damageTick;

        void Awake()
        {
            _model = GetComponent<PlayerDataModel>();
        }

        void Start()
        {
            TickTimeTimer.OnTick += DamageTick;
        }

        void OnDestroy()
        {
            TickTimeTimer.OnTick -= DamageTick;
        }

        // Update is called once per frame
        void Update()
        {
            if (_model.CanTakeAmbientDamage && PauseManager.IsPaused() == false)
            {
                if (_damageTick >= _damageMaxTick)
                {
                    _model.MoveReminder.SetActive(true);
                    PlayerManager.instance.HealthController.RemoveHealth(1);
                    PlayerManager.instance.HealthController.PlayDamageSoundNoMove();
                    PlayerManager.instance.HealthController.ResetInvulnerability();
                    _damageTick = 0;
                }
            }
            else
            {
                _model.MoveReminder.SetActive(false);
                _damageTick = 0;
            }
        }

        void DamageTick(object sender, TickTimeTimer.OnTickEventArgs args)
        {
            _damageTick += 1;
        }
    }
}