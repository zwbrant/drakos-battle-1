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

    public List<ServerClient> ServerClients { get; private set; }
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
                Debug.Log(string.Format("User {0} has connected", connectionId));
                ServerClients.Add(new ServerClient(connectionId));
                break;
            case NetworkEventType.DisconnectEvent:
                Debug.Log(string.Format("User {0} has disconnected", connectionId));
                //ServerClients.Remove();
                break;
            case NetworkEventType.DataEvent:

                NetMsg netMsg = DeserializeNetMsg(recBuffer);
                OnNetMsgReceived(connectionId, channelId, recHostId, netMsg);

                break;
            case NetworkEventType.BroadcastEvent:
                Debug.Log("Unexpected Network Event");
                break;
        }
    }

    public void OnNetMsgReceived(int cnnId, int channelId, int hostId, NetMsg netMsg)
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

    public void SendToClient(int recHost, int cnnId, NetMsg msg)
    {
        byte[] buffer = SerializeNetMsg(msg);

        NetworkTransport.Send(_hostId, cnnId, _reliableChannel, buffer, MSG_BYTE_SIZE, out _error);
    }

    public byte[] SerializeNetMsg(NetMsg netMsg)
    {
        byte[] buffer = new byte[MSG_BYTE_SIZE];

        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream memStream = new MemoryStream(buffer);
        formatter.Serialize(memStream, netMsg);

        return buffer;
    }

    public NetMsg DeserializeNetMsg(byte[] netMsgBuffer)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream memStream = new MemoryStream(netMsgBuffer);
        return (NetMsg)formatter.Deserialize(memStream);
    }
}
