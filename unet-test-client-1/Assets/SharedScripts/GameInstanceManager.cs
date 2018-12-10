using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstanceManager : ManagedBehaviour<GameInstanceManager> {

    public GameInstance Game { get; private set; }

    public override void Init()
    {
        ResetGameInstance();
    }

    public virtual void ResetGameInstance()
    {
        Game = new GameInstance
        {
            Player1Setup = new PlayerGameInstance(),
            Player2Setup = new PlayerGameInstance()
        };
    }



    #region Setters

    public void P1_SetHandCards(string[] cards)
    {
        Game.Player1Setup.HandCards = cards;
    }
    public void P2_SetHandCards(string[] cards)
    {
        Game.Player2Setup.HandCards = cards;
    }

    public void P1_SetCircles(Circle[] circles)
    {
        Game.Player1Setup.Circles = circles;
    }
    public void P2_SetCircles(Circle[] circles)
    {
        Game.Player1Setup.Circles = circles;
    }
    #endregion
}

public class ServerGameInstanceManager : GameInstanceManager
{
    public Deck Deck { get; set; }
    // the initial update sent to both player to setup the game board
    public InitGameSetup InitSetup { get; private set; }

    public InitGameSetup CreateInitSetup()
    {
        InitSetup = new InitGameSetup
        {
            P1Setup = CreateInitPlayerStateUpdate(),
            P2Setup = CreateInitPlayerStateUpdate()
        };

        return InitSetup;
    }

    private PlayerStateUpdate CreateInitPlayerStateUpdate()
    {
        PlayerStateUpdate playerState = new PlayerStateUpdate();
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

        playerState.CircleChanges = circles;
        playerState.DrawnCards = drawnCards;

        return playerState;
    }

    public CircleUpdate CreateEmptyCircleUpdate(int index)
    {
        CircleUpdate update = new CircleUpdate();

        Array values = Enum.GetValues(typeof(CircleColor));
        System.Random random = new System.Random();
        CircleColor randColor = (CircleColor)values.GetValue(random.Next(values.Length));

        update.NewColor = randColor;
        update.CircleIndex = index;
        update.NewCard = null;
        update.CardHpChange = null;

        return update;
    }

     


}
