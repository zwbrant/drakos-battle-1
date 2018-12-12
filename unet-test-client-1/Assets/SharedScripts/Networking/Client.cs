using RoboRyanTron.Unite2017.Variables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Client : ManagedBehaviour<Client> {
    public StringVariable IPAddress;
    public StringVariable Port;

    public ClientGameState OnConnectedState;

    public GameInstanceManager GameInstance;
    public PlayerManager PlayerManager;

    public PlayerGameInstance PlayerSetup { get; private set; }
    public bool IsOnline { get; private set; }

    public const int MSG_BYTE_SIZE = 1400;

    private int _hostId;
    private int _connectionId;
    private byte _reliableChannel;
    private byte _error;

	// Update is called once per frame
	void Update ()
    {
        UpdateMessagePump();
    }

    public override void Init()
    {
        IsOnline = false;
        NetworkTransport.Init();

        ConnectionConfig cc = new ConnectionConfig();
        _reliableChannel = cc.AddChannel(QosType.Unreliable);

        HostTopology hostTopology = new HostTopology(cc, 100);

        _hostId = NetworkTransport.AddHost(hostTopology, 0);


    }

    public void Connnect()
    {
        // only ensuring dragon is equipped on the client-side for now
        if (PlayerManager.PlayerInfo.EquippedDragonId == null)
        {
            Debug.LogError("Cannot connect to game until a dragon is equipped");
            return;
        }

        _connectionId = NetworkTransport.Connect(_hostId, IPAddress.Value, int.Parse(Port.Value), 0, out _error);
        if ((NetworkError)_error != NetworkError.Ok)
        {
            //Output this message in the console with the Network Error
            Debug.Log("There was this error : " + (NetworkError)_error);
        }
        //Otherwise if no errors occur, output this message to the console
        else
        {
            IsOnline = true;

        }
    }

    private void UpdateMessagePump()
    {
        if (!IsOnline)
            return;

        int recHostId, cnnId, channelId;
        byte[] dataBuffer = new byte[5000];
        int dataSize;

        NetworkEventType networkEventType = 
            NetworkTransport.Receive(out recHostId, out cnnId, out channelId, 
            dataBuffer, dataBuffer.Length, out dataSize, out _error);
        switch (networkEventType)
        {
            case NetworkEventType.Nothing:
                break;
            case NetworkEventType.ConnectEvent:
                OnConnect(cnnId, channelId, recHostId, _error);
                break;
            case NetworkEventType.DisconnectEvent:
                Debug.Log(string.Format("Disconnected from game server", cnnId));
                break;
            case NetworkEventType.DataEvent:
                NetMsg netMsg = MsgSerializer.DeserializeNetMsg(dataBuffer);
                OnData(cnnId, channelId, recHostId, netMsg);
                break;
            case NetworkEventType.BroadcastEvent:
                Debug.Log("Unexpected Network Event");
                break;
        }
    }

    #region Connect & Disconnect

    private void OnConnect(int cnnId, int channelId, int hostId, int error)
    {
        Debug.Log("Connected to Server");
        PlayerInfoNetMsg msg = new PlayerInfoNetMsg();
        msg.PlayerInfo = PlayerManager.PlayerInfo;

        SendToServer(msg);
    }

    private void OnDisconnect(int cnnId, int channelId, int hostId, int error)
    {
        Debug.Log("Disconnected from Server");
        PlayerManager.SetDisconnected();
        IsOnline = false;
    }

    #endregion

    #region OnData
    public void OnData(int cnnId, int channelId, int hostId, NetMsg netMsg)
    {
        switch (netMsg.OP)
        {
            case NetOP.CardDealt:
                Debug.Log("OnData: CardDealt");
                break;
            case NetOP.PlayersJoined:
                Debug.Log("OnData: PlayersJoined");
                OnPlayersJoined(cnnId, channelId, hostId, netMsg);
                break;
            case NetOP.InitSetup:
                OnGameInit(cnnId, channelId, hostId, (InitSetupNetMsg)netMsg);
                break;
            case NetOP.TotalStateUpdate:
                Debug.Log("DOG CAT");
                break;
            case NetOP.SetPlayerNumber:
                PlayerManager.SetConnected(cnnId, ((SetPlayerNumberNetMsg)netMsg).PlayerNumber);
                break;

            default:
                break;

        }
    }

    public void OnPlayersJoined(int cnnId, int channelId, int hostId, NetMsg netMsg)
    {

    }

    public void OnGameInit(int cnnId, int channelId, int hostId, InitSetupNetMsg initSetupMsg)
    {
        Debug.Log("Eager baby dog");
        //InitGameBoardMsg msg = (InitGameBoardMsg)netMsg;

        GameInstance.SetPlayerSetup(initSetupMsg.PlayerNumber, initSetupMsg.PlayerSetup);
    }

    #endregion

    public void SendToServer(NetMsg msg)
    {
        byte[] buffer = MsgSerializer.SerializeNetMsg(msg, MSG_BYTE_SIZE);

        //Debug.Log("Buffer Length: " + buffer.Length);

        NetworkTransport.Send(_hostId, _connectionId, _reliableChannel, buffer, MSG_BYTE_SIZE, out _error);

        //Debug.LogError((NetworkError)_error);
    }

}
