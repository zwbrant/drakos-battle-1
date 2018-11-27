using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Server : MonoBehaviour {

    private const int PORT = 12345;
    private const int MSG_BYTE_SIZE = 1024;

    private int _hostId;
    private byte _reliableChannel;
    private byte _error;

    public ServerClient Player1 { get; private set; }
    public ServerClient Player2 { get; private set; }
    public bool IsOnline { get; private set; }


    // Use this for initialization
    void Start () {
        IsOnline = false;
	}
	
	// Update is called once per frame
	void Update () {
        UpdateMessagePump();
	}

    public void Init()
    {
        DontDestroyOnLoad(gameObject);

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

    private void OnDisconnect(int connectionId, int channelId, int recHostId)
    {
        if (Player1 != null && Player1.ConnectionId == connectionId)
        {
            Player1 = null;
        }

        if (Player2 != null && Player2.ConnectionId == connectionId)
        {
            Player2 = null;
        }
    }

    private bool AddNewPlayer(int cnnId)
    {
        if (Player1 != null && Player2 != null)
        {
            Debug.LogError("Can't add another player, there are already two connected");
            return false;
        }

        if (Player1 == null) {
            Player1 = new ServerClient(cnnId);
        } else { 
            Player2 = new ServerClient(cnnId);
        }
        return true;
    }

    public void OnConnect(int cnnId, int channelId, int hostId)
    {
        Debug.Log(string.Format("User {0} has connected", cnnId));

        // succesfully added new player and both players are connected
        if (AddNewPlayer(cnnId) && Player1 != null && Player2 != null)
        {
            PlayersConnectedMsg connectedMsg = new PlayersConnectedMsg();
            SendToClient(Player1.ConnectionId, connectedMsg);
            SendToClient(Player2.ConnectionId, connectedMsg);

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


}
