using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Server : MonoBehaviour {

    private const int PORT = 12345;
    private const int MSG_SIZE = 1024;

    private int _hostId;
    private byte _reliableChannel;
    private byte _error;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
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

    }

    private void ReceiveMessage()
    {
        int recHostId;
        int connectionId;
        int channelId;
        byte[] recBuffer = new byte[MSG_SIZE];
        int bufferSize = 1024;
        int dataSize;

        NetworkEventType recData = NetworkTransport.Receive(out recHostId, out connectionId, out channelId, recBuffer, bufferSize, out dataSize, out _error);
        switch (recData)
        {
            case NetworkEventType.Nothing: break;
            case NetworkEventType.ConnectEvent: break;
            case NetworkEventType.DataEvent: break;
            case NetworkEventType.DisconnectEvent: break;

            case NetworkEventType.BroadcastEvent:

                break;
        }
    }
}
