using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Server : ManagedBehaviour<Server> {

    private const int PORT = 12345;
    private const int MSG_BYTE_SIZE = 1400;

    private int _hostId;
    private byte _reliableChannel;
    private byte _error;

    public GameInstanceManager GameInstance;
    public PlayerManager Player1;
    public PlayerManager Player2;

    public bool IsOnline { get; private set; }

    // Update is called once per frame
    void Update ()
    {
        UpdateMessagePump();
	}

    public override void Init()
    {
        NetworkTransport.Init();

        ConnectionConfig cc = new ConnectionConfig();
        _reliableChannel = cc.AddChannel(QosType.Unreliable);

        HostTopology hostTopology = new HostTopology(cc, 100);

        _hostId = NetworkTransport.AddHost(hostTopology, PORT, null);

        Debug.Log(string.Format("Opening port {0} on localhost", PORT.ToString()));

        IsOnline = true;
    }

    private void UpdateMessagePump()
    {
        if (!IsOnline)
            return;

        int recHostId, connectionId, channelId;
        byte[] dataBuffer = new byte[MSG_BYTE_SIZE];
        int dataSize;

        NetworkEventType networkEventType = 
            NetworkTransport.Receive(out recHostId, out connectionId, out channelId, dataBuffer, 
            dataBuffer.Length, out dataSize, out _error);
        switch (networkEventType)
        {
            case NetworkEventType.Nothing:
                break;
            case NetworkEventType.ConnectEvent:
                OnConnect(connectionId, channelId, recHostId);
                break;
            case NetworkEventType.DisconnectEvent:
                Debug.Log(string.Format("User {0} has disconnected", connectionId));
                OnDisconnect(connectionId, channelId, recHostId);
                //ServerClients.Remove();
                break;
            case NetworkEventType.DataEvent:
                NetMsg netMsg = MsgSerializer.DeserializeNetMsg(dataBuffer);
                OnData(connectionId, channelId, recHostId, netMsg);
                break;
            case NetworkEventType.BroadcastEvent:
                Debug.Log("Unexpected Network Event");
                break;
        }
    }

    #region Connect & Disconnect functions

    public void OnConnect(int cnnId, int channelId, int hostId)
    {
        Debug.Log(string.Format("User {0} has connected", cnnId));

        // succesfully added new player, tell them their player number
        if (AddNewPlayer(cnnId))
        {
            var msg = new SetPlayerNumberNetMsg() { PlayerNumber = GetPlayer(cnnId).PlayerNumber  };
            SendToClient(cnnId, msg);
        }
    }

    private void OnDisconnect(int cnnId, int channelId, int recHostId)
    {
        if (!Player1.IsConnected && !Player2.IsConnected)
            return;

        var player = GetPlayer(cnnId);
        if (player == null)
            return;
        else
            player.SetDisconnected();
    }

    private bool AddNewPlayer(int cnnId)
    {
        if (Player1.IsConnected && Player2.IsConnected)
        {
            Debug.LogError("Can't add another player, there are already two connected");
            return false;
        }

        if (!Player1.IsConnected)
            Player1.SetConnected(cnnId, PlayerOrdinal.Player1);
        else
            Player2.SetConnected(cnnId, PlayerOrdinal.Player2);
        return true;
    }

    #endregion

    #region OnData

    public void OnData(int cnnId, int channelId, int hostId, NetMsg netMsg)
    {
        switch(netMsg.OP)
        {
            case NetOP.PlayerInfo:
                OnPlayerInfo(cnnId, channelId, hostId, (PlayerInfoNetMsg)netMsg);
                break;
            default:
                break;
        }
    }

    private void OnPlayerInfo(int cnnId, int channelId, int hostId, PlayerInfoNetMsg netMsg)
    {
        var player = GetPlayer(cnnId);
        if (player == null)
        {
            Debug.LogError("Can't set player info, because no player is associated with this connection");
            return;
        }

        // Save player's information
        player.PlayerInfo = netMsg.PlayerInfo;
        Debug.Log(string.Format("{0} equipped dragon {1}", player.PlayerNumber, player.PlayerInfo.EquippedDragonId));

        if (Player1.IsReadyToBattle() && Player2.IsReadyToBattle())
        {
            StartGame();
        }
    }

    #endregion

    public void StartGame()
    {
        // let em' know we're starting the game
        SendToClient((int)Player1.ConnectionId, new NetMsg(NetOP.GameInit));
        SendToClient((int)Player2.ConnectionId, new NetMsg(NetOP.GameInit));

        GameInstance.InitializeGame(Player1.PlayerInfo, Player2.PlayerInfo, new Deck(CardCache.Instance, 20, true, true));

        // send the initial (setup) turns

        Debug.Log("Sending Initial UPDATES");

        SendTurnToPlayer(GameInstance.P1InitialSetup, Player1);
        SendTurnToPlayer(GameInstance.P2InitialSetup, Player1);
        SendTurnToPlayer(GameInstance.P1InitialSetup, Player2);
        SendTurnToPlayer(GameInstance.P2InitialSetup, Player2);
    }

    // take a turn object, turn it into 3 seperate update net messages, then send them off
    public void SendTurnToPlayer(Turn turn, PlayerManager recipient)
    {
        var dragonMsg = new DragonUpdateNetMsg()
        {
            PlayerNumber = turn.PlayerNumber,
            DragonUpdate = turn.DragonUpdate
        };
        var circlesMsg = new CirclesUpdateNetMsg()
        {
            PlayerNumber = turn.PlayerNumber,
            CirclesUpdate = turn.CirclesUpdate
        };
        var cardsMsg = new CardsUpdateNetMsg()
        {
            PlayerNumber = turn.PlayerNumber,
            CardsUpdate = turn.CardsUpdate
        };

        SendToClient((int)recipient.ConnectionId, dragonMsg);
        SendToClient((int)recipient.ConnectionId, circlesMsg);
        SendToClient((int)recipient.ConnectionId, cardsMsg);

    }

    public void SendToClient(int cnnId, NetMsg msg)
    {
        byte[] buffer = MsgSerializer.SerializeObject(msg, MSG_BYTE_SIZE);

        NetworkTransport.Send(_hostId, cnnId, _reliableChannel, buffer, MSG_BYTE_SIZE, out _error);

        if (_error != 0)
            Debug.Log((NetworkError)_error);
    }

    public PlayerManager GetPlayer(int cnnId)
    {
        if (Player1.ConnectionId == cnnId)
            return Player1;
        if (Player2.ConnectionId == cnnId)
            return Player2;

        return null;
    }

    public void TestSerialize()
    {

        DragonUpdateNetMsg m = new DragonUpdateNetMsg();

        SendToClient((int)Player1.ConnectionId, m);
        

    }

}
