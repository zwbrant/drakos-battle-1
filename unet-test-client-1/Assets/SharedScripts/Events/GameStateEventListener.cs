using RoboRyanTron.Unite2017.Events;
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

    public virtual void OnEventRaised(ClientGameState state)
    {
        DynamicResponse.Invoke(state);

        switch (state)
        {
            case ClientGameState.AwaitingOpponentJoin:
                AwaitingPlayerResponse.Invoke();
                break;
            case ClientGameState.CardSetup:
                SetupCardsResponse.Invoke();
                break;
            case ClientGameState.PlayerSetupReady:
                UserSetupReadyResponse.Invoke();
                break;
            case ClientGameState.PlayerTurn:
                UserTurnResponse.Invoke();
                break;
            case ClientGameState.PlayerAttack:
                UserAttackResponse.Invoke();
                break;
            case ClientGameState.OpponentTurn:
                OpponentTurnResponse.Invoke();
                break;
            case ClientGameState.OpponentAttack:
                OpponentAttackResponse.Invoke();
                break;
        }
    }

}
