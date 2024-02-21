using UnityEngine;
using UnityEngine.Events;

namespace TSCore.Utils.GameEvent
{
    public class GameEventListener : MonoBehaviour
    {
        public GameEvent Event;
        public UnityEvent Response;

        void OnEnable()
        {
            if (Event != null) Event.RegisterListener(this);
            //Debug.Log($"{Event.name} had a listener added to there List");   
        }

        void OnDisable()
        {
            if (Event != null) Event.UnregisterListener(this);
            //Debug.Log($"{Event.name} had a listener removed from there List");
        }

        public void OnEventRaised() => Response.Invoke();
    }
}
