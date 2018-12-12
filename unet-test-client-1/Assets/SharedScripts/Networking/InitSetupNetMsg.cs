using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class InitSetupNetMsg : NetMsg
{
    public PlayerOrdinal PlayerNumber { get; set; }
    public PlayerOrdinal PlayerNumber2 { get; set; }

    public PlayerStateUpdate PlayerSetup { get; set; }

    public InitSetupNetMsg()
    {
        OP = NetOP.InitSetup;
    }
}

[Serializable]
public class SetPlayerNumberNetMsg : NetMsg
{
    public PlayerOrdinal PlayerNumber { get; set; }

    public SetPlayerNumberNetMsg()
    {
        OP = NetOP.SetPlayerNumber;
    }
}

