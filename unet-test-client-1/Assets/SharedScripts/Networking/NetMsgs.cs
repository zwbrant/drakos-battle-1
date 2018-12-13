using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NetOP : byte
{
    None,
    PlayersReady,
    GameInit,
    PlayerInfo,
    SetPlayerNumber,
    InitSetup,
    DragonUpdate,
    CirclesUpdate,
    CardsUpdate
}

[Serializable]
public class NetMsg
{
    public NetOP OP { get; set; }

    public NetMsg()
    {
        OP = NetOP.None;
    }

    public NetMsg(NetOP op)
    {
        OP = op;
    }
}

[Serializable]
public class InitSetupNetMsg : NetMsg
{
    public PlayerOrdinal PlayerNumber { get; set; }
    public PlayerOrdinal PlayerNumber2 { get; set; }

    public PlayerStateUpdate PlayerSetup { get; set; }

    public InitSetupNetMsg()
    {
        OP = NetOP.InitSetup;
    }
}

[Serializable]
public class SetPlayerNumberNetMsg : NetMsg
{
    public PlayerOrdinal PlayerNumber { get; set; }

    public SetPlayerNumberNetMsg()
    {
        OP = NetOP.SetPlayerNumber;
    }
}

[Serializable]
class PlayerInfoNetMsg : NetMsg
{
    public PlayerInfo PlayerInfo { get; set; }

    public PlayerInfoNetMsg()
    {
        OP = NetOP.PlayerInfo;
    }
}

[Serializable]
class GameUpdateNetMsg : NetMsg
{
    public PlayerOrdinal PlayerNumber { get; set; }
}

[Serializable]
class DragonUpdateNetMsg : GameUpdateNetMsg
{
    public DragonStateUpdate DragonUpdate { get; set; }

    public DragonUpdateNetMsg()
    {
        OP = NetOP.DragonUpdate;
    }
}

[Serializable]
class CirclesUpdateNetMsg : GameUpdateNetMsg
{
    public CirclesStateUpdate CirclesUpdate { get; set; }

    public CirclesUpdateNetMsg()
    {
        OP = NetOP.CirclesUpdate;
    }
}

[Serializable]
class CardsUpdateNetMsg : GameUpdateNetMsg
{
    public CardsStateUpdate CardsUpdate { get; set; }

    public CardsUpdateNetMsg()
    {
        OP = NetOP.CardsUpdate;
    }
}


