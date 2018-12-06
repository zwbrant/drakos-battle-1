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
    public GameEventDispatcher EventDispatcher;
    public GameStateEvent GameStateEvent;

    public PlayerInstance PlayerSetup { get; private set; }
    public bool IsOnline { get; private set; }

    public const int MSG_BYTE_SIZE = 1024;

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
    }

    public void Connnect()
    {
        DontDestroyOnLoad(gameObject);

        NetworkTransport.Init();

        ConnectionConfig cc = new ConnectionConfig();
        _reliableChannel = cc.AddChannel(QosType.Reliable);

        HostTopology hostTopology = new HostTopology(cc, 100);

        _hostId = NetworkTransport.AddHost(hostTopology, 0);

        _connectionId = NetworkTransport.Connect(_hostId, IPAddress.Value, int.Parse(Port.Value), 0, out _error);

        if ((NetworkError)_error != NetworkError.Ok)
        {
            //Output this message in the console with the Network Error
            Debug.Log("There was this error : " + (NetworkError)_error);
        }
        //Otherwise if no errors occur, output this message to the console
        else
        {
            Debug.Log("Connected : " + (NetworkError)_error);
            IsOnline = true;

            OnConnectedToServer();
        }
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
                Debug.Log(string.Format("Connected to game server", connectionId));
                SceneManager.LoadScene("Battle");
                break;
            case NetworkEventType.DisconnectEvent:
                Debug.Log(string.Format("Disconnected from game server", connectionId));
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

    #region OnData
    public void OnData(int cnnId, int channelId, int hostId, NetMsg netMsg)
    {
        switch (netMsg.OP)
        {
            case NetOP.CardDealt:
                Debug.Log("OnData: CardDealt");
                break;
            case NetOP.PlayersConnected:
                Debug.Log("OnData: PlayersConnected");
                OnPlayersConnected(cnnId, channelId, hostId, netMsg);
                break;
            case NetOP.TotalStateUpdate:
                Debug.Log("DOG CAT");
                break;
            default:
                break;

        }
    }

    public void OnPlayersConnected(int cnnId, int channelId, int hostId, NetMsg netMsg)
    {
        GameStateEvent.Raise(ClientGameState.CardSetup);
        PlayerSetup = new PlayerInstance();
    }



    #endregion


    public void OnConnectedToServer()
    {
        //SceneManager.LoadScene(1);
    }

    private void OnLevelWasLoaded(int level)
    {
        string levelName = SceneManager.GetSceneByBuildIndex(level).name;
        switch (levelName)
        {
            case "Battle":
                GameStateEvent.Raise(OnConnectedState);
                break;
            default:
                break;
        }
    }

    public void SendToServer(NetMsg msg)
    {
        byte[] buffer = new byte[MSG_BYTE_SIZE];

        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream memStream = new MemoryStream(buffer);
        formatter.Serialize(memStream, msg);

        NetworkTransport.Send(_hostId, _connectionId, _reliableChannel, buffer, MSG_BYTE_SIZE, out _error);
    }

    public void SendCardDealtMsg()
    {
        SendToServer(new CardDealtMsg() { CardName = "Salmon Baby" });
    }


}
