using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TSCore.Utils.GameEvent
{
    [CreateAssetMenu(menuName = "TSCore/Game Event")]
    public class GameEvent : UnityEngine.ScriptableObject
    {
        List<GameEventListener> listeners = new List<GameEventListener>();

        [Button]
        public void Raise()
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventRaised();
            }
        }

        public void RegisterListener(GameEventListener listener)
        {
            listeners.Add(listener);
        }

        public void UnregisterListener(GameEventListener listener)
        {
            listeners.Remove(listener);
            listeners.Sort();
        }

        [Button("Remove All Listeners")]
        public void UnregisterAllListeners()
        {
            listeners.Clear();
        }
    }
}
