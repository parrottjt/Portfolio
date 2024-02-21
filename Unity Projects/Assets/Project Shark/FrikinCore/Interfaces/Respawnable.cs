namespace FrikinCore.Interfaces
{
    public class Respawnable
    {
        bool _canRespawn = true;
        public bool CanRespawn => _canRespawn;

        void SetRespawn(bool canRespawn)
        {
            _canRespawn = canRespawn;
        }
    }
}
