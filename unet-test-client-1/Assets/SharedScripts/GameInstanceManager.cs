using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldGameInstanceManager : ManagedBehaviour<OldGameInstanceManager> {

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

public class GameInstanceManager : MonoBehaviour
{
    public GameInstance Game { get; private set; }

    public Deck Deck { get; private set; }

    public Stack<Turn> P1Turns { get; private set; }
    public Stack<Turn> P2Turns { get; private set; }
    public InitGameSetup InitSetup { get; private set; }

    public InitGameSetup InitializeGame(PlayerInfo p1, PlayerInfo p2, Deck deck)
    {
        Deck = deck;
        InitSetup = new InitGameSetup
        {
            P1Setup = CreateInitPlayerStateUpdate(p1),
            P2Setup = CreateInitPlayerStateUpdate(p2)
        };

        return InitSetup;
    }

    public void InitializePlayer(PlayerOrdinal playerNumber, PlayerStateUpdate setup)
    {
        if (InitSetup == null)
            InitSetup = new InitGameSetup { P1Setup = p1Setup, P2Setup = p2Setup };
        ApplyStateUpdate(PlayerOrdinal.Player1, p1Setup);
        ApplyStateUpdate(PlayerOrdinal.Player2, p2Setup);
        Debug.Log("Player1 Dragon: " + Game.Player1Setup.Dragon.DragonId);
        Debug.Log("Player2 Dragon: " + Game.Player2Setup.Dragon.DragonId);

    }

    private PlayerStateUpdate CreateInitPlayerStateUpdate(PlayerInfo playerInfo)
    {
        PlayerStateUpdate playerState = new PlayerStateUpdate();

        playerState.NewDragonEquip = playerInfo.EquippedDragonId;

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

        playerState.DragonHpChange = null;
        playerState.DragonEnergyChange = null;
        playerState.CircleRotation = null;
        playerState.CircleChanges = circles;
        playerState.DrawnCards = drawnCards;
        playerState.DiscardedCards = null;

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

    private void ApplyStateUpdate(PlayerOrdinal player, PlayerStateUpdate update)
    {
        var playerSetup = (player == PlayerOrdinal.Player1) ? Game.Player1Setup : Game.Player2Setup;

        playerSetup.Circles = UpdateCircles(playerSetup.Circles, update.CircleChanges);
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
        if (update.NewColor != null)
            { circle.Color = (CircleColor)update.NewColor; }
        if (update.NewCard != null)
            { circle.Card = update.NewCard; }
        else if (update.CardHpChange != null)
            { circle.Card.HP += (int)update.CardHpChange; }
        return circle;
    }
}



public class OldServerGameInstanceManager : OldGameInstanceManager
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
