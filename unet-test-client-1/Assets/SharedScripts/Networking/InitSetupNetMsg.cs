using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class InitSetupNetMsg : NetMsg
{
    public PlayerOrdinal PlayerNumber { get; set; }
    public PlayerStateUpdate PlayerSetup { get; set; }

    public InitSetupNetMsg()
    {
        OP = NetOP.InitSetup;
    }
}

public class SetPlayerNumberNetMsg : NetMsg
{
    public PlayerOrdinal PlayerNumber { get; set; }

    public SetPlayerNumberNetMsg()
    {
        OP = NetOP.SetPlayerNumber;
    }
}

