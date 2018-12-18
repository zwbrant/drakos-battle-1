using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventDispatcher : MonoBehaviour
{
    public PlayerOrdinal LocalPlayerNumber { get; set; }

    public GameStateEvent GameStateEvent;

    public Event PlayerTurnReceivedEvent;
    public Event EnemyTurnReceivedEvent;

    public Turn CurrPlayerTurn;
    public Turn CurrEnemyTurn;

    public void Start()
    {
        ResetTurn(ref CurrEnemyTurn);
        ResetTurn(ref CurrPlayerTurn);
    }

    public void Init(PlayerOrdinal localPlayerNumber)
    {
        ResetTurn(ref CurrEnemyTurn);
        ResetTurn(ref CurrPlayerTurn);

        CurrPlayerTurn.PlayerNumber = localPlayerNumber;
        CurrEnemyTurn.PlayerNumber =
            (localPlayerNumber == PlayerOrdinal.Player1) ? PlayerOrdinal.Player2 : PlayerOrdinal.Player2;

    }

    public void ProcessDragonUpdate(DragonStateUpdate update, bool isEnemyTurn)
    {
        Turn turn = (isEnemyTurn) ? CurrEnemyTurn : CurrPlayerTurn;
        if (turn.HasBeenConsumed)
            ResetTurn(ref turn);

        turn.DragonUpdate = update;
        turn.HasBeenConsumed = false;

        TryRaiseTurnEvent(isEnemyTurn); 
    }

    public void ProcessCirclesUpdate(CirclesStateUpdate update, bool isEnemyTurn)
    {
        Turn turn = (isEnemyTurn) ? CurrEnemyTurn : CurrPlayerTurn;
        if (turn.HasBeenConsumed)
            ResetTurn(ref turn);

        turn.CirclesUpdate = update;
        turn.HasBeenConsumed = false;

        TryRaiseTurnEvent(isEnemyTurn);
    }

    public void ProcessCardsUpdate(CardsStateUpdate update, bool isEnemyTurn)
    {
        Turn turn = (isEnemyTurn) ? CurrEnemyTurn : CurrPlayerTurn;
        if (turn.HasBeenConsumed)
            ResetTurn(ref turn);

        turn.CardsUpdate = update;
        turn.HasBeenConsumed = false;

        TryRaiseTurnEvent(isEnemyTurn);
    }

    private void TryRaiseTurnEvent(bool isEnemyTurn)
    {
        Turn turn = (isEnemyTurn) ? CurrEnemyTurn : CurrPlayerTurn;

        if (!turn.HasBeenConsumed && 
            turn.DragonUpdate != null && 
            turn.CirclesUpdate != null &&
            turn.CardsUpdate != null)
        {
            if (isEnemyTurn)
                EnemyTurnReceivedEvent.Raise();
            else
                PlayerTurnReceivedEvent.Raise();

            turn.HasBeenConsumed = true;
        }

    }

    public void OnGameInit()
    {
        GameStateEvent.Raise(ClientGameState.SetupPhase);
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
