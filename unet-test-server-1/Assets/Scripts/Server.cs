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
    private const int MSG_BYTE_SIZE = 1024;

    private int _hostId;
    private byte _reliableChannel;
    private byte _error;

    public PlayerManager Player1;
    public PlayerManager Player2;

    public ServerGameInstanceManager GameInstance { get; private set; }

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
        _reliableChannel = cc.AddChannel(QosType.Reliable);

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
        byte[] recBuffer = new byte[MSG_BYTE_SIZE];
        int bufferSize = 1024;
        int dataSize;

        NetworkEventType networkEventType = NetworkTransport.Receive(out recHostId, out connectionId, out channelId, recBuffer, bufferSize, out dataSize, out _error);
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
                NetMsg netMsg = MsgSerializer.DeserializeNetMsg(recBuffer);
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

        // succesfully added new player and both players are connected
        if (AddNewPlayer(cnnId) && Player1.IsConnected && Player2.IsConnected)
        {
            PlayersConnectedMsg playersReadyMsg = new PlayersConnectedMsg();
            SendToClient((int)Player1.ConnectionId, playersReadyMsg);
            SendToClient((int)Player2.ConnectionId, playersReadyMsg);

            GameInstance.ResetGameInstance();
            GameInstance.Deck = new Deck(CardCache.Instance, 20, true, true);

            InitGameSetupMsg initSetupMsg = new InitGameSetupMsg();
            initSetupMsg.InitSetup = GameInstance.CreateInitSetup();
            SendToClient((int)Player1.ConnectionId, initSetupMsg);
            SendToClient((int)Player2.ConnectionId, initSetupMsg);
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
        {
            Player1.SetConnected(cnnId, PlayerOrdinal.Player1);
        }
        else
        {
            Player2.SetConnected(cnnId, PlayerOrdinal.Player2);
        }
        return true;
    }

    #endregion

    #region OnData

    public void OnData(int cnnId, int channelId, int hostId, NetMsg netMsg)
    {
        switch(netMsg.OP)
        {
            case NetOP.CardDealt:
                Debug.Log("sda");
                break;
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

        player.PlayerInfo = netMsg.PlayerInfo;
        Debug.Log(string.Format("Player {0} equipped dragon {1}", player.PlayerNumber, player.PlayerInfo.EquippedDragonId));

    }

    #endregion

    public void SendToClient(int cnnId, NetMsg msg)
    {
        byte[] buffer = MsgSerializer.SerializeNetMsg(msg, MSG_BYTE_SIZE);

        NetworkTransport.Send(_hostId, cnnId, _reliableChannel, buffer, MSG_BYTE_SIZE, out _error);
    }

    public PlayerManager GetPlayer(int cnnId)
    {
        if (Player1.ConnectionId == cnnId)
            return Player1;
        if (Player2.ConnectionId == cnnId)
            return Player2;

        return null;
    }

}
