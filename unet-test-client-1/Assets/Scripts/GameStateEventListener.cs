﻿using RoboRyanTron.Unite2017.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStateEventListener : MonoBehaviour
{
    [Tooltip("Event to register with.")]
    public GameStateEvent Event;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityGameStateEvent DynamicResponse;
    public UnityEvent AwaitingPlayerResponse;
    public UnityEvent SetupCardsResponse;
    public UnityEvent UserSetupReadyResponse;
    public UnityEvent UserTurnResponse;
    public UnityEvent UserAttackResponse;
    public UnityEvent OpponentTurnResponse;
    public UnityEvent OpponentAttackResponse;


    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public virtual void OnEventRaised(GameState state)
    {
        DynamicResponse.Invoke(state);

        switch (state)
        {
            case GameState.AwaitingPlayerJoin:
                AwaitingPlayerResponse.Invoke();
                break;
            case GameState.SetupCards:
                SetupCardsResponse.Invoke();
                break;
            case GameState.UserSetupReady:
                UserSetupReadyResponse.Invoke();
                break;
            case GameState.UserTurn:
                UserTurnResponse.Invoke();
                break;
            case GameState.UserAttack:
                UserAttackResponse.Invoke();
                break;
            case GameState.OpponentTurn:
                OpponentTurnResponse.Invoke();
                break;
            case GameState.OpponentAttack:
                OpponentAttackResponse.Invoke();
                break;
        }
    }

}
