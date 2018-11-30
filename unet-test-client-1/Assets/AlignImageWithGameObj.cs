using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class AlignImageWithGameObj : MonoBehaviour {
    public bool ImageDisabledOnStart = true;
    private Image _image;

	// Use this for initialization
	void Start () {
        _image = GetComponent<Image>();

        if (ImageDisabledOnStart)
            _image.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Align(GameObject gameObj)
    {
        _image.enabled = (gameObj != null);

        this.transform.position = gameObj.transform.position;
    }
}
