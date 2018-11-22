using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerClient : MonoBehaviour {
    public int ConnectionId { get; private set; }

    public ServerClient(int connectionId)
    {
        ConnectionId = connectionId;
    }
}
