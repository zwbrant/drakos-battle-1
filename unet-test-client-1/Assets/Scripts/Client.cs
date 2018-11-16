using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Client : MonoBehaviour {

    private const int PORT = 12345;
    private const string SERVER_IP = "127.0.0.1";

    private int _hostId;
    private byte _reliableChannel;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Connnect()
    {
        DontDestroyOnLoad(gameObject);

        NetworkTransport.Init();

        ConnectionConfig cc = new ConnectionConfig();
        _reliableChannel = cc.AddChannel(QosType.Reliable);

        HostTopology hostTopology = new HostTopology(cc, 100);

        _hostId = NetworkTransport.AddHost(hostTopology, 0);

        byte error;
        NetworkTransport.Connect(_hostId, SERVER_IP, PORT, 0, out error);

        if ((NetworkError)error != NetworkError.Ok)
        {
            //Output this message in the console with the Network Error
            Debug.Log("There was this error : " + (NetworkError)error);
        }
        //Otherwise if no errors occur, output this message to the console
        else Debug.Log("Connected : " + (NetworkError)error);


        int i = 55555555;
        byte b = (byte)i;
        print(b);
    }

    public void OnConnectedToServer()
    {
        print("Connected to Server");
    }
}
