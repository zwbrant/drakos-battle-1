using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayersConnectedMsg : NetMsg {

    public PlayersConnectedMsg()
    {
        OP = NetOP.PlayersConnected;
    }
}
