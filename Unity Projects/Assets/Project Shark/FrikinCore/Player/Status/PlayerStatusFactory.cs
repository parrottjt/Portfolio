using FrikinCore.Player.Status.States;

namespace FrikinCore.Player.Status
{
    public class PlayerStatusFactory
    {
        readonly PlayerStatusContext _context;

        public PlayerStatusFactory(PlayerStatusContext context)
        {
            _context = context;
        }

        public PlayerStatusBaseState NoStatus() => new NoStatusPlayer(_context, this);
        public PlayerStatusBaseState Stunned() => new StunnedPlayer(_context, this);
        public PlayerStatusBaseState Freeze() => new FreezePlayerState(_context, this);
        public PlayerStatusBaseState Death() => new PlayerDeath(_context, this);
    }
}
