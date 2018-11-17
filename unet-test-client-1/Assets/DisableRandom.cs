using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableRandom : MonoBehaviour {

    public RectTransSet Set;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Disable()
    {
        int index = Random.Range(0, Set.Items.Count);

        Set.Items[index].gameObject.SetActive(false);
    }
}
