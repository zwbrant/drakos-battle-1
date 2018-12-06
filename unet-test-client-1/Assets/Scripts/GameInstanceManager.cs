using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstanceManager : ManagedBehaviour<GameInstanceManager> {

    public GameInstance GameInstance { get; private set; }

    public override void Init()
    {
        GameInstance = new GameInstance();
    }


    public bool CanEquipDragon(string dragonId)
    {
        return DragonCache.TryGetDragonByID(dragonId);
    }

    public void P1_SetEquippedDragon(string dragonId)
    {
        if (CanEquipDragon(dragonId))
            GameInstance.Player1Setup.Dragon = dragonId;
    }
    public void P2_SetEquippedDragon(string dragonId)
    {
        if (CanEquipDragon(dragonId))
            GameInstance.Player2Setup.Dragon = dragonId;
    }

    public void P1_SetHandCards(string[] cards)
    {
        GameInstance.Player1Setup.HandCards = cards;
    }
    public void P2_SetHandCards(string[] cards)
    {
        GameInstance.Player2Setup.HandCards = cards;
    }

    public void P1_SetCircles(Circle[] circles)
    {
        GameInstance.Player1Setup.Circles = circles;
    }
    public void P2_SetCircles(Circle[] circles)
    {
        GameInstance.Player1Setup.Circles = circles;
    }
}
