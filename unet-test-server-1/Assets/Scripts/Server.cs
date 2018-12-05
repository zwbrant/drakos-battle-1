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

    public GameInstance CurrentGame { get; private set; }
    public Deck CurrentDeck { get; private set; }

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

        int recHostId;
        int connectionId;
        int channelId;
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

    private void OnDisconnect(int cnnId, int channelId, int recHostId)
    {
        if (CurrentGame.Player1 != null && CurrentGame.Player1.ConnectionId == cnnId)
        {
            CurrentGame.Player1 = null;
        }

        if (CurrentGame.Player2 != null && CurrentGame.Player2.ConnectionId == cnnId)
        {
            CurrentGame.Player2 = null;
        }
    }

    private bool AddNewPlayer(int cnnId)
    {
        if (CurrentGame.Player1 != null && CurrentGame.Player2 != null)
        {
            Debug.LogError("Can't add another player, there are already two connected");
            return false;
        }

        if (CurrentGame.Player1 == null) {
            CurrentGame.Player1 = new Player() { ConnectionId = cnnId };
        } else {
            CurrentGame.Player2 = new Player() { ConnectionId = cnnId };
        }
        return true;
    }

    public void OnConnect(int cnnId, int channelId, int hostId)
    {
        Debug.Log(string.Format("User {0} has connected", cnnId));

        // succesfully added new player and both players are connected
        if (AddNewPlayer(cnnId) && CurrentGame.Player1 != null && CurrentGame.Player2 != null)
        {
            PlayersConnectedMsg playersReadyMsg = new PlayersConnectedMsg();
            SendToClient(CurrentGame.Player1.ConnectionId, playersReadyMsg);
            SendToClient(CurrentGame.Player2.ConnectionId, playersReadyMsg);
            CurrentGame = new GameInstance();
            CurrentDeck = new Deck(CardCache.Instance, 20, true);
        }

    }

    public void OnData(int cnnId, int channelId, int hostId, NetMsg netMsg)
    {
        switch(netMsg.OP)
        {
            case NetOP.CardDealt:
                Debug.Log("sda");
                break;
            default:
                break;

        }
    }

    public void SendToClient(int cnnId, NetMsg msg)
    {
        byte[] buffer = MsgSerializer.SerializeNetMsg(msg, MSG_BYTE_SIZE);

        NetworkTransport.Send(_hostId, cnnId, _reliableChannel, buffer, MSG_BYTE_SIZE, out _error);
    }


    public void SendTotalUpdate()
    {
        SendToClient(CurrentGame.Player1.ConnectionId, new TotalStateUpdateMsg());
    }

}
