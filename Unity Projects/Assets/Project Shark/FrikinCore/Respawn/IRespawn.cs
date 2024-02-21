namespace FrikinCore.Respawn
{
    public interface IRespawn
    {
        void PlayerDeath();
        void RespawnFunctionality();
        void RespawnToLocation();
        void LoadToScene();
        void FindOnSceneChange();
    }
}