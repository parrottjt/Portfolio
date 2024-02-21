using System.Collections;
using System.Collections.Generic;

using TSCore.Utils;
using UnityEngine;

namespace TSCore.Time
{
    public enum TimeLayers
    {
        Default,
        Player,
        Enemy,
        Hazard,
        UI
    }

    public static class TimeManager
    {
        static float _globalSpeed = 1f;
        static readonly DeltaObject _delta = new DeltaObject();
        static readonly FixedDeltaObject _fixedDelta = new FixedDeltaObject();

        static readonly Dictionary<TimeLayers, float> _timeLayers = new Dictionary<TimeLayers, float>();

        public static DeltaObject Delta => _delta;
        public static FixedDeltaObject FixedDelta => _fixedDelta;

        public static float GlobalDelta => UnityEngine.Time.deltaTime * GlobalSpeed;

        public static float GlobalSpeed
        {
            get { return _globalSpeed; }
            set { _globalSpeed = Mathf.Clamp(value, 0, 1f); }
        }

        public static float GetLayerSpeed(TimeLayers timeLayer) => _timeLayers[timeLayer] * _globalSpeed;
        public static float DeltaLayer(TimeLayers timeLayer) => _timeLayers[timeLayer] * GlobalDelta;

        static TimeManager()
        {
            foreach (var timeLayer in Enums.GetValues<TimeLayers>())
            {
                _timeLayers.Add(timeLayer, 1f);
            }
        }

        public static void SetLayerSpeed(TimeLayers timeLayer, float value)
        {
            _timeLayers[timeLayer] = value;
        }

        public static void ResetTimeLayers() => SetAll(1f);

        public static void SetAll(float value)
        {
            GlobalSpeed = value;
            foreach (var timeLayer in Enums.GetValues<TimeLayers>())
            {
                SetLayerSpeed(timeLayer, value);
            }
        }

        public static Coroutine WaitForSeconds(MonoBehaviour m, float time)
        {
            return m.StartCoroutine(WaitForSeconds_Custom(time, TimeLayers.Default));
        }

        public static Coroutine WaitForSeconds(MonoBehaviour m, float time, TimeLayers timeLayer)
        {
            return m.StartCoroutine(WaitForSeconds_Custom(time, timeLayer));
        }

        static IEnumerator WaitForSeconds_Custom(float time, TimeLayers timeLayer)
        {
            yield return new WaitForSeconds(time * (1 / GetLayerSpeed(timeLayer)));
        }

        public class DeltaObject
        {
            float this[TimeLayers timeLayer] => UnityEngine.Time.deltaTime * GetLayerSpeed(timeLayer) * GlobalSpeed;
            public static implicit operator float(DeltaObject d) => d[TimeLayers.Default] * GlobalSpeed;
        }

        public class FixedDeltaObject
        {
            float this[TimeLayers timeLayer] => UnityEngine.Time.fixedDeltaTime * GetLayerSpeed(timeLayer) * GlobalSpeed;
            public static implicit operator float(FixedDeltaObject d) => d[TimeLayers.Default] * GlobalSpeed;
        }
    }
}