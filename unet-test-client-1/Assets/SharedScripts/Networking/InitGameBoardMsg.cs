using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InitGameSetupMsg : NetMsg {

    public InitGameSetup InitSetup { get; set; }

    public InitGameSetupMsg()
    {
        OP = NetOP.GameInit;
    } 
}
