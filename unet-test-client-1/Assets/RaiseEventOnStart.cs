using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseEventOnStart : MonoBehaviour {

    public ClientGameState GameState;
    public GameStateEvent Event;

	// Use this for initialization
	void Start () {
        Event.Raise(GameState);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
