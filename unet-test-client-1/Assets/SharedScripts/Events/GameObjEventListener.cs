using System;
using UnityEngine;
using UnityEngine.Events;

public class GameObjEventListener : MonoBehaviour
{
    [Tooltip("Event to register with.")]
    public GameObjEvent Event;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityGameObjEvent Response;

    private void OnEnable()
    {
        if (Event != null)
            Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        if (Event != null)
            Event.UnregisterListener(this);
    }

    public virtual void OnEventRaised(GameObject gameObj)
    {
        Response.Invoke(gameObj);
    }
}