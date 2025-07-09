using TSCore.Time;
using TSCore.Utils;
using UnityEngine;
using UnityEngine.Events;

public static class PauseManager
{
    public static event UnityAction OnPause, OnUnpause;

    enum State
    {
        Unpause,
        Pause
    }

    static State _state = State.Unpause;
    static float oldGlobalSpeed;

    public static bool IsPaused() => _state == State.Pause;

    public static void Pause()
    {
        if (_state == State.Pause) return;

        OnPause?.Invoke();
        
        _state = State.Pause;
        oldGlobalSpeed = TimeManager.GlobalSpeed;
        DebugScript.Log(nameof(PauseManager), "Pause is turned on!");
        TimeManager.GlobalSpeed = 0;
        //This is dangers to other things
        Time.timeScale = 0;
        //If things break with time scale its here
    }

    public static void Unpause()
    {
        if(_state == State.Unpause) return;
        DebugScript.Log(nameof(PauseManager), "Pause is turned off!");
        
        OnUnpause?.Invoke();
        
        _state = State.Unpause;
        TimeManager.GlobalSpeed = oldGlobalSpeed;
        Time.timeScale = 1;
    }
}