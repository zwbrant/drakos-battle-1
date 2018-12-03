using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Clickable : MonoBehaviour, IPointerDownHandler {
    public UnityGameObjEvent ClickedEvent;

    public void OnPointerDown(PointerEventData eventData)
    {
        ClickedEvent.Invoke(this.gameObject);       
    }

}