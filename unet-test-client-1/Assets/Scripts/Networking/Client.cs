using RoboRyanTron.Unite2017.Variables;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Client : MonoBehaviour {
    public StringVariable IPAddress;
    public StringVariable Port;

    public UnityStringEvent NewCardDrawnEvent;
    public UnityEvent RandomCardDrawnEvent;

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
        NetworkTransport.Connect(_hostId, IPAddress.Value, int.Parse(Port.Value), 0, out error);

        if ((NetworkError)error != NetworkError.Ok)
        {
            //Output this message in the console with the Network Error
            Debug.Log("There was this error : " + (NetworkError)error);
        }
        //Otherwise if no errors occur, output this message to the console
        else
        {
            Debug.Log("Connected : " + (NetworkError)error);
            OnConnectedToServer();
        }



    }

    public void OnConnectedToServer()
    {
        SceneManager.LoadScene(1);
    }

    public void OnNewCardDrawn(string cardGuid)
    {
        Debug.Log("Drawing card " + cardGuid);
        NewCardDrawnEvent.Invoke(cardGuid);
    }

    public void OnNewRandomCardDrawn()
    {
        Debug.Log("Drawing random card");
        RandomCardDrawnEvent.Invoke();
    }
}
