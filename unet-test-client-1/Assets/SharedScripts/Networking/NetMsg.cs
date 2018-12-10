﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NetOP : byte
{
    None,
    CardDealt,
    PlayersJoined,
    GameInit,
    PlayerInfo,
    TotalStateUpdate
}

[System.Serializable]
public abstract class NetMsg
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
