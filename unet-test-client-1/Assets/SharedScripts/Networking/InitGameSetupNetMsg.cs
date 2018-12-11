using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InitGameSetupNetMsg : NetMsg {

    public PlayerOrdinal ClientPlayerNumber { get; set; }
    public InitGameSetup InitSetup { get; set; }

    public InitGameSetupNetMsg()
    {
        OP = NetOP.GameInit;
    } 
}
