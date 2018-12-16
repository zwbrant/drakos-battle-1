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

    public Event OnConnectedEvent;
    public Event OnDisconnectedEvent;

    public GameInstanceManager GameInstance;
    public GameEventDispatcher EventDispatcher;
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
        if (_error != 0)
            Debug.Log((NetworkError)_error);
        else
        {
            IsOnline = true;
            OnConnectedEvent.Raise();
        }
    
    }

    public void Disconnect()
    {
        NetworkTransport.Disconnect(_hostId, _connectionId, out _error);
        if (_error != 0)
            Debug.Log((NetworkError)_error);

    }

    private void UpdateMessagePump()
    {
        if (!IsOnline)
            return;

        int recHostId, cnnId, channelId;
        byte[] dataBuffer = new byte[MSG_BYTE_SIZE];
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
                IsOnline = false;
                OnDisconnectedEvent.Raise();
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
        SceneManager.LoadScene("Battle");

        Debug.Log("Connected to Server");
        PlayerInfoNetMsg msg = new PlayerInfoNetMsg();
        msg.PlayerInfo = PlayerManager.PlayerInfo;

        SendToServer(msg);
    }

    private void OnDisconnect(int cnnId, int channelId, int hostId, int error)
    {
        SceneManager.LoadScene("Battle");

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
            case NetOP.GameInit:
                OnGameInit(cnnId, channelId, hostId);
                break;
            case NetOP.SetPlayerNumber:
                PlayerManager.SetConnected(cnnId, ((SetPlayerNumberNetMsg)netMsg).PlayerNumber);
                EventDispatcher.Init(((SetPlayerNumberNetMsg)netMsg).PlayerNumber);
                break;
            case NetOP.DragonUpdate:
                OnGameUpdate((GameUpdateNetMsg)netMsg);
                break;
            case NetOP.CirclesUpdate:
                OnGameUpdate((GameUpdateNetMsg)netMsg);
                break;
            case NetOP.CardsUpdate:
                OnGameUpdate((GameUpdateNetMsg)netMsg);
                break;
            default:
                break;

        }
    }

    private void OnGameUpdate (GameUpdateNetMsg updateMsg)
    {
        bool isOpponentTurn = (updateMsg.PlayerNumber != PlayerManager.PlayerNumber);
        // consume data
        switch (updateMsg.OP)
        {
            case NetOP.DragonUpdate:
                EventDispatcher.ProcessDragonUpdate(((DragonUpdateNetMsg)updateMsg).DragonUpdate, isOpponentTurn);
                break;
            case NetOP.CirclesUpdate:
                EventDispatcher.ProcessCirclesUpdate(((CirclesUpdateNetMsg)updateMsg).CirclesUpdate, isOpponentTurn);
                break;
            case NetOP.CardsUpdate:
                EventDispatcher.ProcessCardsUpdate(((CardsUpdateNetMsg)updateMsg).CardsUpdate, isOpponentTurn);
                break;
            default:
                return;
        }
    }


    public void OnGameInit(int cnnId, int channelId, int hostId)
    {
        Debug.Log("Initializing Game");
        EventDispatcher.OnGameInit();
    }

    #endregion

    public void SendToServer(NetMsg msg)
    {
        byte[] buffer = MsgSerializer.SerializeObject(msg, MSG_BYTE_SIZE);

        NetworkTransport.Send(_hostId, _connectionId, _reliableChannel, buffer, MSG_BYTE_SIZE, out _error);

        if (_error != 0)
            Debug.Log((NetworkError)_error);
    }

}
