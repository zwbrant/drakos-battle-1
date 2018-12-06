using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstanceManager : ManagedBehaviour<GameInstanceManager> {

    public GameInstance Game { get; private set; }
    public PlayerStateUpdate TurnInstance { get; private set; }


    public virtual void CreateNewGameInstance()
    {
        Game = new GameInstance();
    }

    public virtual void DestroyGameInstance()
    {
        Game = null;
    }

    public virtual void CreateNewTurnInstance()
    {
        TurnInstance = new PlayerStateUpdate();
    }


    public bool CanEquipDragon(string dragonId)
    {
        return DragonCache.TryGetDragonByID(dragonId);
    }

    public void P1_SetEquippedDragon(string dragonId)
    {
        if (CanEquipDragon(dragonId))
            Game.Player1Setup.Dragon = dragonId;
    }
    public void P2_SetEquippedDragon(string dragonId)
    {
        if (CanEquipDragon(dragonId))
            Game.Player2Setup.Dragon = dragonId;
    }

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
}

public class ServerGameInstanceManager : GameInstanceManager
{
    public Deck Deck { get; private set; }

    public void CreateNewGameInstance(Deck deck)
    {
        base.CreateNewGameInstance();
        Deck = deck;
    }
}
