using TSCore.Time;
using UnityEngine;

namespace FrikinCore.Player
{
    public class PlayerParticleSystem
    {
        ParticleSystem _particleSystem;
        float _timer;

        public PlayerParticleSystem(ParticleSystem system)
        {
            _particleSystem = system;
        }

        public bool IsPlaying => _particleSystem.isPlaying;

        public void Run()
        {
            if (_particleSystem.isPlaying == false)
                _particleSystem.Play();
        }

        public void Run(float time)
        {
            Run();
            _timer += TimeManager.Delta;
            if (_timer >= time)
            {
                _particleSystem.Stop();
            }
        }

        public void Run(bool whileTrue)
        {
            if (whileTrue) Run();
            else Stop();
        }

        public void Stop()
        {
            _particleSystem.Stop();
        }
    }
}
