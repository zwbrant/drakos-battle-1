using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InitGameBoardMsg : NetMsg {

    public PlayerStateUpdate P1Update;
    public PlayerStateUpdate P2Update;


    public InitGameBoardMsg()
    {
        OP = NetOP.GameInit;
    } 
}
