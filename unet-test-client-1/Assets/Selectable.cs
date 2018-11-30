using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selectable : MonoBehaviour, IPointerDownHandler {
    public UnityGameObjEvent SelectionEvent;


    public void OnPointerDown(PointerEventData eventData)
    {

        SelectionEvent.Invoke(this.gameObject);
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
