using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEnabler : MonoBehaviour {
    public HandCardSet EnabledHandCards;
    public HandCardSet DisabledHandCards;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnGameStateChange(ClientGameState state)
    {
        Debug.Log("State changed: " + state.ToString());
    } 
}
