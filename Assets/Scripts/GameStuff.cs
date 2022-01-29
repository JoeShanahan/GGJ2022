using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStuff : ScriptableObject
{
    public enum CurrentState { PlayMode, BuildMode, DestroyMode, MenuUp };
    public enum DayState { Day, Night };

    public CurrentState currentState;
    public DayState timeOfDay;

    public void OnEnable()
    {
        currentState = CurrentState.PlayMode;
        timeOfDay = DayState.Night;
    }

    public bool IsNotPlayMode => currentState != CurrentState.PlayMode;
}
