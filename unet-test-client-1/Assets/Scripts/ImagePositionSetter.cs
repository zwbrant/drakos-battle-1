using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImagePositionSetter : MonoBehaviour {
    public bool InvisibleBeforeSet = true;
    private Image _image;

	// Use this for initialization
	void Awake () {
        _image = GetComponent<Image>();

        if (InvisibleBeforeSet)
            _image.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetPosition(Vector3 targetPosition)
    {
        _image.enabled = (targetPosition != null);
        gameObject.SetActive(targetPosition != null);

        this.transform.position = targetPosition;
    }


    public void SetPosition(GameObject targetObject)
    {
        _image.enabled = (targetObject != null);
        gameObject.SetActive(targetObject != null);

        this.transform.position = targetObject.transform.position;
    }
}
