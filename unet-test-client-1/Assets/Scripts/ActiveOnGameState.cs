using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveOnGameState : MonoBehaviour {
    public GameState ActiveState;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void SetActive(GameState state)
    {
        if (state == ActiveState)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }
}
