using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventDispatcher : MonoBehaviour
{
    public PlayerOrdinal LocalPlayerNumber { get; set; }

    public UnityGameStateEvent GameStateEvent;

    public UnityEvent PlayerTurnReceivedEvent;
    public UnityEvent OpponentTurnReceivedEvent;

    public Turn CurrPlayerTurn;
    public Turn CurrOpponentTurn;

    public void Init(PlayerOrdinal localPlayerNumber)
    {
        ResetTurn(ref CurrOpponentTurn);
        ResetTurn(ref CurrPlayerTurn);

        CurrPlayerTurn.PlayerNumber = localPlayerNumber;
        CurrOpponentTurn.PlayerNumber =
            (localPlayerNumber == PlayerOrdinal.Player1) ? PlayerOrdinal.Player2 : PlayerOrdinal.Player2;

    }

    public void ProcessDragonUpdate(DragonStateUpdate update, bool isOpponentTurn)
    {
        Turn turn = (isOpponentTurn) ? CurrOpponentTurn : CurrPlayerTurn;

        turn.DragonUpdate = update;
        turn.HasBeenConsumed = false;

        TryRaiseTurnEvent(isOpponentTurn); 
    }

    public void ProcessCirclesUpdate(CirclesStateUpdate update, bool isOpponentTurn)
    {
        Turn turn = (isOpponentTurn) ? CurrOpponentTurn : CurrPlayerTurn;

        turn.CirclesUpdate = update;
        turn.HasBeenConsumed = false;

        TryRaiseTurnEvent(isOpponentTurn);
    }

    public void ProcessCardsUpdate(CardsStateUpdate update, bool isOpponentTurn)
    {
        Turn turn = (isOpponentTurn) ? CurrOpponentTurn : CurrPlayerTurn;

        turn.CardsUpdate = update;
        turn.HasBeenConsumed = false;

        TryRaiseTurnEvent(isOpponentTurn);
    }

    private void TryRaiseTurnEvent(bool isOpponentTurn)
    {
        Turn turn = (isOpponentTurn) ? CurrOpponentTurn : CurrPlayerTurn;

        if (!turn.HasBeenConsumed && 
            turn.DragonUpdate != null && 
            turn.CirclesUpdate != null &&
            turn.CardsUpdate != null)
        {
            if (isOpponentTurn)
                OpponentTurnReceivedEvent.Invoke();
            else
                PlayerTurnReceivedEvent.Invoke();

            turn.HasBeenConsumed = true;
        }

        ResetTurn(ref turn);
    }

    public void OnGameInit()
    {
        GameStateEvent.Invoke(ClientGameState.SetupPhase);
    }

    private void ResetTurn(ref Turn turn)
    {
        turn.HasBeenConsumed = true;
        turn.TurnNumber = (byte)0;
        turn.DragonUpdate = null;
        turn.CirclesUpdate = null;
        turn.CardsUpdate = null;
    }


}
