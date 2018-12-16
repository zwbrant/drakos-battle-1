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
        CircleUpdate[] circles = new CircleUpdate[6];
        for (int i = 0; i < 6; i++)
        {
            circles[i] = CreateEmptyCircleUpdate(i);
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

    private void ApplyStateUpdate(PlayerOrdinal player, PlayerStateUpdate update)
    {
        var playerSetup = (player == PlayerOrdinal.Player1) ? Game.Player1Setup : Game.Player2Setup;

        playerSetup.Circles = UpdateCircles(playerSetup.Circles, update.CircleChanges);
    }


    #region Static game managment functions
    public static CircleUpdate CreateEmptyCircleUpdate(int index)
    {
        CircleUpdate update = new CircleUpdate();

        Array values = Enum.GetValues(typeof(CircleColor));
        System.Random random = new System.Random();
        CircleColor randColor = (CircleColor)values.GetValue(random.Next(values.Length));

        update.NewColor1 = (byte)randColor;
        update.CircleIndex = (byte)index;
        update.NewCard = null;
        update.CardPowerChange = null;

        return update;
    }

    public static Circle[] UpdateCircles(Circle[] circles, CircleUpdate[] updates)
    {
        for (int i = 0; i < circles.Length; i++)
        {
            for (int x = 0; x < updates.Length; x++)
            {
                if (circles[i].CircleIndex == updates[x].CircleIndex)
                {
                    circles[i] = UpdateCircle(circles[i], updates[x]);
                }
            }
        }
        return circles;
    }

    public static Circle UpdateCircle(Circle circle, CircleUpdate update)
    {
        if (update.NewColor1 != null)
            { circle.Color1 = (byte)update.NewColor1; }
        if (update.NewCard != null)
            { circle.Card = update.NewCard; }
        else if (update.CardPowerChange != null)
            { circle.Card.Power = (byte)((int)circle.Card.Power + (int)update.CardPowerChange); }
        return circle;
    }
    #endregion
}

