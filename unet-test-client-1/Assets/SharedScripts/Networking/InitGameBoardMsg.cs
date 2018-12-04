using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InitGameBoardMsg : NetMsg {

    public string[] DealtCards { get; set; }
    public Dragon OpponentDragon { get; set; }
    public CircleColor[] CircleColors { get; set; }


    public InitGameBoardMsg()
    {
        OP = NetOP.InitGameBoard;
    } 
}
