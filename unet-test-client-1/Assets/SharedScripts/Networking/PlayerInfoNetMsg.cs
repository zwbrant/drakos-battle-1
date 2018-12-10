using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[System.Serializable]
class PlayerInfoNetMsg : NetMsg
{
    public PlayerInfo PlayerInfo { get; set; }



    public PlayerInfoNetMsg()
    {
        OP = NetOP.PlayerInfo;
    }
}

