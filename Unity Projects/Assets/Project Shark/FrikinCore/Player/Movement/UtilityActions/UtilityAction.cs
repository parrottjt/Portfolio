using FrikinCore.Input;
using TSCore.Time;

namespace FrikinCore.Player.Movement
{
    //todo: possible add state machine based on new character Utility actions
    public abstract class UtilityAction : AbstractPlayerBehavior
    {
        void Start()
        {
            TickTimeTimer.OnTick += OnTick;
        }

        void OnDestroy()
        {
            TickTimeTimer.OnTick -= OnTick;
        }
        
        void Update()
        {
            var primary = InputManager.instance.UtilityInput();
            var secondary = InputManager.instance.UtilityHoldInput();

            AdditionalUpdateFunctionality(primary, secondary);
            if (primary) PrimaryUtilityFunctionality();
            if (secondary) SecondaryUtilityFunctionality();
        }

        protected abstract void AdditionalUpdateFunctionality(bool primary, bool secondary);
        protected abstract void PrimaryUtilityFunctionality();
        protected abstract void SecondaryUtilityFunctionality();
        protected abstract void OnTick(object sender, TickTimeTimer.OnTickEventArgs e);
    }
}