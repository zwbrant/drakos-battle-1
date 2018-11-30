using RoboRyanTron.Unite2017.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjEventListener : MonoBehaviour {
    [Tooltip("Event to register with.")]
    public GameObjEvent Event;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityGameObjEvent Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public virtual void OnEventRaised(GameObject gameObj)
    {
        Response.Invoke(gameObj);
    }
}
