using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class NetMsg {

    public NetOP OP { get; set; }

    public enum NetOP : byte
    {
        None,
        DealtCard
    }

    public NetMsg()
    {
        OP = NetOP.None;
    }

    public NetMsg(NetOP op)
    {
        OP = op;
    }

   

}
