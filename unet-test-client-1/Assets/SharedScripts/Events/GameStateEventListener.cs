using RoboRyanTron.Unite2017.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStateEventListener : MonoBehaviour
{
    public GameStateEvent GameStateEvent;

    [Tooltip("The state TargetStateResponse will be invoked by.")]
    public ClientGameState TargetState;
    public UnityGameStateEvent TargetStateResponse;

    [Tooltip("All other states will invoke this response")]
    public UnityGameStateEvent OtherStatesResponse;


    private void OnEnable()
    {
        if (GameStateEvent != null)
            GameStateEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        if (GameStateEvent != null)
            GameStateEvent.UnregisterListener(this);
    }

    public virtual void OnEventRaised(ClientGameState state)
    {
        if (state == TargetState)
            TargetStateResponse.Invoke(state);
        else
            OtherStatesResponse.Invoke(state);
    }

}
