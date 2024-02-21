using System;
using System.Collections;
using SharkCore.Utils.Utils;
using TSCore.Utils;
using UnityEngine;

namespace TSCore.Time
{
    /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    /// Possible updates:
    /// -Get the tick to start for each object when they subscribe, as currently if anything joins in the middle of
    /// a tick the time isn't consistent. Possible solution is to reset the tick on a scene change to give a bigger window  
    /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public class TickTimeTimer : MonoBehaviour
    {
        //Creating the event args for the the event
        public class OnTickEventArgs : EventArgs
        {
            public int tick;
        }

        //This allows for a check of the current number of event subscriptions
        public static int GetOnTickSubCount()
        {
            return OnTick.GetInvocationList().Length;
        }

        //Creating the Event
        public static event EventHandler<OnTickEventArgs> OnTick, OnTick_OneSec;

        public static float TickTimerMax = .1f; // 100 milliseconds, 1/10 second

        static int totalTick;
        float tickTimer;

        public static int GetTotalTick() => totalTick;

        bool CanRun => true;
        
        void Awake()
        {
            totalTick = 0;
            StartCoroutine(Tick());
        }

        IEnumerator Tick()
        {
            //todo: Add GameManager in
            while (true)
            {
                if (TimeManager.GlobalSpeed > 0)
                {
                    yield return TimeManager.WaitForSeconds(this, TickTimerMax);
                
                    totalTick += 1;
                    //print($"<color=yellow>Tick</color>");
                    OnTick?.Invoke(this, new OnTickEventArgs { tick = totalTick });
                    if (totalTick % 10 == 0)
                    {
                        OnTick_OneSec?.Invoke(this, new OnTickEventArgs { tick = totalTick });
                        //print($"<color=pink>Tick_OneSec</color>");
                    }
                }

                yield return null;
            }

            DebugScript.LogError(typeof(TickTimeTimer), "GameManager was destroyed, TICK HAS STOPPED");
        }
    }
}