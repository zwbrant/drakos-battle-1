using System;
using System.Collections;
using System.Collections.Generic;

#region Game Level

[Serializable]
public class GameInstance
{
    public PlayerGameInstance Player1Setup { get; set; }
    public PlayerGameInstance Player2Setup { get; set; }
}

[Serializable]
public class PlayerInfo
{
    public string EquippedDragonId { get; set; }

    // (other stuff: username, etc...)
}

[Serializable]
public class PlayerGameInstance
{
    public string[] HandCards { get; set; }
    public Circle[] Circles { get; set; }
    public PlacedDragon Dragon { get; set; }
}

[Serializable]
public class PlayerStateUpdate
{
    public string NewDragonEquip { get; set; }
    public int? DragonHpChange { get; set; }
    public int? DragonEnergyChange { get; set; }

    public CircleUpdate[] CircleChanges { get; set; }
    public int? CircleRotation { get; set; }

    public string[] DrawnCards { get; set; }
    public string[] DiscardedCards { get; set; }
}

[Serializable]
public class Turn
{
    public int TurnNumber { get; set; }
    public PlayerOrdinal PlayerNumber { get; set; }  
    public PlayerStateUpdate StateUpdate { get; set; }
}

[Serializable]
public class InitGameSetup
{
    public PlayerStateUpdate P1Setup { get; set; }
    public PlayerStateUpdate P2Setup { get; set; }
}

public enum PlayerOrdinal : byte
{
    Player1,
    Player2
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
    public int CircleIndex { get; set; }
    public CircleColor Color { get; set; }
    public PlacedCard Card { get; set; }
}

[Serializable]
public class CircleUpdate
{
    public int CircleIndex { get; set; }
    public CircleColor? NewColor { get; set; }
    public PlacedCard NewCard { get; set; }
    public int? CardHpChange { get; set; }

}

[Serializable]
public class PlacedDragon
{
    public string DragonId { get; set; }
    public int HP { get; set; }
    public int Energy { get; set; }
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