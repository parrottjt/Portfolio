using FrikinCore.Player.Status.States;
using TSCore.Time;
using TSCore.Utils.GameEvent;
using UnityEngine;

namespace FrikinCore.Player.Status
{
    public class PlayerStatusContext : MonoBehaviour
    {
        [SerializeField] GameEvent _cameraShake;
        PlayerStatusFactory _states;

        public PlayerDataModel Model { get; private set; }
        public PlayerStatusBaseState Current { get; set; }
        public void ShakeCamera() => _cameraShake.Raise();

        void Awake()
        {
            Model = GetComponent<PlayerDataModel>();

            _states = new PlayerStatusFactory(this);
            Current = _states.NoStatus();
            Current.EnterState();
        }

        void Start() => TickTimeTimer.OnTick += OnTick;

        void Update()
        {
            Current.CheckSwitchStates();
            Current.ExecuteState();
        }

        void OnTick(object sender, TickTimeTimer.OnTickEventArgs e) => Current.OnTick();
    }
}
