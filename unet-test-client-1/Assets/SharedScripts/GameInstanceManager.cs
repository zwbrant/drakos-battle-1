using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstanceManager : MonoBehaviour
{
    public GameInstance Game { get; private set; }

    public Deck Deck { get; private set; }

    public Stack<Turn> P1Turns { get; private set; }
    public Stack<Turn> P2Turns { get; private set; }

    public Turn P1InitialSetup { get; private set; }
    public Turn P2InitialSetup { get; private set; }
    public Turn P1ArrangeTurn { get; private set; }
    public Turn P2ArrangeTurn { get; private set; }



    public Turn CurrPlayerTurn;
    public Turn CurrOponentTurn;

    public void ResetGame()
    {
        Deck = null;
        Game = new GameInstance();
        P1Turns = new Stack<Turn>();
        P2Turns = new Stack<Turn>();
    }

    public void InitializeGame(PlayerInfo p1, PlayerInfo p2, Deck deck)
    {
        ResetGame();

        Deck = deck;

        P1InitialSetup = CreateInitTurn(p1, PlayerOrdinal.Player1);
        P2InitialSetup = CreateInitTurn(p2, PlayerOrdinal.Player2);
    }

    private Turn CreateInitTurn(PlayerInfo playerInfo, PlayerOrdinal playerNumber)
    {
        Turn initTurn = new Turn()
        {
            PlayerNumber = playerNumber,
            DragonUpdate = new DragonStateUpdate(),
            CirclesUpdate = new CirclesStateUpdate(),
            CardsUpdate = new CardsStateUpdate()
        };

        // create empty circles

        Array values = Enum.GetValues(typeof(CircleColor));
        System.Random random = new System.Random();

        CircleUpdate[] circles = new CircleUpdate[6];
        for (int i = 0; i < 6; i++)
        {

            circles[i] = CreateEmptyCircleUpdate(i,
                (CircleColor)values.GetValue(random.Next(values.Length)),
                (CircleColor)values.GetValue(random.Next(values.Length)));
        }

        // draw initial cards
        string[] drawnCards = new string[5];
        for (int i = 0; i < 5; i++)
        {
            drawnCards[i] = Deck.DrawCard().id;
        }

        initTurn.DragonUpdate.NewDragonEquip = playerInfo.EquippedDragonId;
        initTurn.CirclesUpdate.CircleChanges = circles;
        initTurn.CardsUpdate.DrawnCards = drawnCards;

        return initTurn;
    }


    #region Static game managment functions
    public static CircleUpdate CreateEmptyCircleUpdate(int index, CircleColor rightColor, CircleColor leftColor)
    {
        CircleUpdate update = new CircleUpdate();

        update.RightColor = (byte)rightColor;
        update.LeftColor = (byte)leftColor;
        update.CircleIndex = (byte)index;
        update.NewCard = null;
        update.CardPowerChange = null;

        return update;
    }

    #endregion
}

