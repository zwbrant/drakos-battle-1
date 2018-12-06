﻿using System;
using System.Collections;
using System.Collections.Generic;

#region Game Level

[Serializable]
public class GameInstance
{
    public ClientGameState GameState;

    public Player Player1 { get; set; }
    public Player Player2 { get; set; }

    public PlayerInstance Player1Setup { get; set; }
    public PlayerInstance Player2Setup { get; set; }
}

[Serializable]
public class PlayerInstance
{
    public string[] HandCards { get; set; }
    public Circle[] Circles { get; set; }
    public string Dragon { get; set; }
}

[Serializable]
public class PlayerStateUpdate
{
    public Circle[] CircleChanges;
    public int? CircleRotation;

    public string[] DrawnCards;
    public string[] DiscardedCards;

    public string NewDragonEquip;
    public int? DragonDamage;
    public int? DragonEnergyChange;
}

#endregion


#region Unit Level

[Serializable]
public class Dragon
{
    public string id { get; set; }
    public string name { get; set; }
    public int level { get; set; }
    public int health { get; set; }
    public int attack { get; set; }
    public int attack_cost { get; set; }
    public int energy { get; set; }
    public string age { get; set; }
    public string color { get; set; }
    public string spriteFile { get; set; }

    public List<Ability> abilities { get; set; }
}

[Serializable]
public class Ability
{
    public string slug { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public int cost { get; set; }
    public int cooldown { get; set; }
    public int chance { get; set; }
    public string target { get; set; }
    public string player { get; set; }
    public string type { get; set; }

    public Action action { get; set; }
}

[Serializable]
public class Action
{
    public string type { get; set; }
    public string mod { get; set; }
    public string trigger { get; set; }
    public float value { get; set; }
}

[Serializable]
public class Card
{
    public string id { get; set; }
    public string slug { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public string type { get; set; }
    public string subtype { get; set; }
    public int cost { get; set; }
    public string color { get; set; }
    public int power { get; set; }

    public Ability ability { get; set; }
}

[Serializable]
public class Circle
{
    public int CircleID { get; set; }
    public CircleColor Color { get; set; }
    public PlacedCard Card { get; set; }
}

[Serializable]
public class PlacedCard
{
    public string CardId { get; set; }
    public int HP { get; set; }
}


[Serializable]
public enum CircleColor : byte
{
    Green,
    Red,
    Yellow,
    Blue
}

#endregion