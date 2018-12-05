using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[System.Serializable]
public class TotalStateUpdateMsg : NetMsg
{
    public PlayerStateUpdate Player1StateUpdate;
    public PlayerStateUpdate Player2StateUpdate;

    public TotalStateUpdateMsg()
    {
        OP = NetOP.TotalStateUpdate;
    }

}

